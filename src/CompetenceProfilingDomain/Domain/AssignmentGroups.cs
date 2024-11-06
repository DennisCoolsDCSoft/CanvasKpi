using System.Text;
using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;

namespace CompetenceProfilingDomain.Domain;

public class AssignmentGroups
{

    private readonly IAssignmentGroupsDao _assignmentGroupsDao;
    private readonly IDistributedCache _distributedCache;

    public AssignmentGroups(IAssignmentGroupsDao assignmentGroupsDao, IDistributedCache distributedCache)
    {
        _assignmentGroupsDao = assignmentGroupsDao;
        _distributedCache = distributedCache;
    }
    public int GetAssignmentIdInWeightGroup(int courseId)
    {
        var buff = _distributedCache.Get("AssignmentIdInWeightGroup" + courseId);
        if (buff != null)
        {
            return int.Parse(Encoding.UTF8.GetString(buff));
        }
        
        var groupsByCourseId = _assignmentGroupsDao.GetAssignmentGroupsByCourseId(courseId);
        var availableGroups = groupsByCourseId.Edges.Where(w => w.NodeCanvasDto.State == "available" && w.NodeCanvasDto.GroupWeight == "100.0").ToList();
        if (!availableGroups.Any())
            throw new Exception("Assignment Weight Group not found or Weight is not 100%");
        //
        
        var assignId = "";
        foreach (var group in availableGroups)
        {
            if (group.NodeCanvasDto.Name.ToLower() == "canvaskpi" && group.NodeCanvasDto.AssignmentsConnectionCanvasDto.Nodes.Count ==1 )
            {
                assignId = group.NodeCanvasDto.AssignmentsConnectionCanvasDto.Nodes.First().Id ?? "";
            }
        }

        if (assignId == "")
        {
            // en met twee zoek de gene met de naam en gebruik die
            if (availableGroups.First().NodeCanvasDto.AssignmentsConnectionCanvasDto.Nodes.Count > 1)
                throw new Exception("More then one assignments in weight group!");
            assignId = availableGroups.First().NodeCanvasDto.AssignmentsConnectionCanvasDto.Nodes.First().Id;

        }
        
        //
        var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(15));
        _distributedCache.Set("AssignmentIdInWeightGroup" + courseId, Encoding.UTF8.GetBytes(assignId), options);
        
        
        return int.Parse(assignId);
    }
}