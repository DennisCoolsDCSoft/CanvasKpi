using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.Infrastructure;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingDomain.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace CompetenceProfilingDomain.Domain;

public class UserCollection
{
    private readonly IStudentDao _studentDao;
    private readonly IDistributedCache _distributedCache;

    public UserCollection(IStudentDao studentDao, IDistributedCache distributedCache)
    {
        _studentDao = studentDao;
        _distributedCache = distributedCache;
    }

    public List<UserCanvasDto> GetStudentsInCourse(int courseId)
    {
        List<UserCanvasDto>? buff = _distributedCache.GetDistributedCache<List<UserCanvasDto>>("StudentList" + courseId);
        if (buff != null)
        {
            return buff;
        }

        var users = _studentDao.GetStudentsInCourse(courseId).OrderBy(s => s.Name).ToList();
        _distributedCache.SetDistributedCache("StudentList" + courseId, users, 15);
        return users;
    }
}