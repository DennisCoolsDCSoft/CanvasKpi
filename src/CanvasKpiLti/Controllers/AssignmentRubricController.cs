using System;
using System.Collections.Generic;
using System.Linq;
using CanvasIdentity.Extensions;
using CanvasKpiLti.Models;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingDomain.Domain;
using CompetenceProfilingDomain.DomainCp;
using Microsoft.AspNetCore.Mvc;
using AssignmentGroups = CompetenceProfilingDomain.Domain.AssignmentGroups;

namespace CanvasKpiLti.Controllers;

public class AssignmentRubricController : Controller
{
    private readonly CardCollection _cardCollection;
    private readonly UserCollection _userCollection;
    private readonly AssignmentGroups _assignmentGroups;

    private readonly Matrix _matrix;
    //private readonly OutcomeResultCollection _outcomeResultCollection;


    public AssignmentRubricController(CardCollection cardCollection, UserCollection userCollection,
        AssignmentGroups assignmentGroups, Matrix matrix)
    {
        _cardCollection = cardCollection;
        _userCollection = userCollection;
        _assignmentGroups = assignmentGroups;
        _matrix = matrix;
        //_outcomeResultCollection = outcomeResultCollection;
    }

    [HttpPost]
    public IActionResult SubmissionCriteriaRating(string session,
        AssignmentRubricCriteriaRatingViewModel criteriaRatingViewModel)
    {
        try
        {
            if (User.CanvasClaims().CanvasUserId != criteriaRatingViewModel.UserId
                && User.CanvasClaims().IsCanvasInstructor() == false)
                return BadRequest("you can't change userId if you are an student");

            _cardCollection.UpdateCards(
                User.CanvasClaims().CanvasCourseId,
                criteriaRatingViewModel.UserId,
                criteriaRatingViewModel.AssignmentId,
                criteriaRatingViewModel.Points,
                User.CanvasClaims().IsCanvasInstructor());
        }
        catch (Exception e)
        {
            return Problem($"Error: {e.Message}");
        }

        return Ok();
    }


    public IActionResult Index(string session, int userId = 0)
    {
        var studentsInCourse = new List<UserCanvasDto>();
        string studentName;
        if (User.CanvasClaims().IsCanvasInstructor())
        {
            studentsInCourse = _userCollection.GetStudentsInCourse(User.CanvasClaims().CanvasCourseId);

            if (userId == 0 && studentsInCourse.Count != 0)
                userId = studentsInCourse.First().Id;

            studentName = studentsInCourse.FirstOrDefault(f => f.Id == userId)?.Name ?? "";
        }
        else
        {
            userId = User.CanvasClaims().CanvasUserId;
            studentName = User.CanvasClaims().LisPersonNameFull;
        }

        var assignmentId = _assignmentGroups.GetAssignmentIdInWeightGroup(User.CanvasClaims().CanvasCourseId);


        var outcomeResults =
            _matrix.FillMatrix(User.CanvasClaims().CanvasCourseId, assignmentId, userId);

        var outcomeResultsViewModels = new List<OutcomeResultViewModel>();
        foreach (var architecture in _matrix.Architectures)
        {
            foreach (var competenceCell in architecture.CompetenceCells)
            {
                foreach (var outcomeGroup in competenceCell.OutcomeGroups)
                {
                    foreach (var outcomeResult in outcomeGroup.OutcomeResults)
                    {
                        outcomeResultsViewModels.Add(new OutcomeResultViewModel
                        {
                            ArchitectureHbo = architecture.ArchitectureHbo,
                            CompetenceHbo = competenceCell.CompetenceHbo,
                            Level = outcomeGroup.Level,

                            Points = outcomeResult.Points,
                            Description = 
                                outcomeResult.Description.IndexOf('-') < 3 ? 
                                    outcomeResult.Description : outcomeResult.Description.Split('-')[1],
                            CourseHistory = outcomeResult.CourseHistory,
                            CriteriaId = outcomeResult.CriteriaId,
                            LongDescription = outcomeResult.LongDescription,
                            OutcomeId = outcomeResult.OutcomeId,
                            LevelDivisorNumber = outcomeResult.LevelDivisorNumber,
                        });
                    }
                }
            }
        }


        return View(
            new AssignmentRubricViewModel(outcomeResultsViewModels, studentsInCourse, userId, studentName, assignmentId)
        );
    }

    // private string FormatDescription(string outcomeResultDescription)
    // {
    //     var index = outcomeResultDescription.IndexOf('-');
    //     return outcomeResultDescription.IndexOf('-') < 3 ? outcomeResultDescription : outcomeResultDescription.Split('-')[1];
    // }


    // public IActionResult Index(string session, int userId = 0)
    // {
    //     var studentsInCourse = new List<UserCanvasDto>();
    //     string studentName;
    //     if (User.CanvasClaims().IsCanvasInstructor())
    //     {
    //         studentsInCourse = _userCollection.GetStudentsInCourse(User.CanvasClaims().CanvasCourseId);
    //         
    //         if (userId == 0 && studentsInCourse.Count != 0)
    //             userId = studentsInCourse.First().Id;
    //         
    //         studentName = studentsInCourse.FirstOrDefault(f => f.Id == userId)?.Name ?? "";
    //     }
    //     else
    //     {
    //         userId = User.CanvasClaims().CanvasUserId;
    //         studentName = User.CanvasClaims().LisPersonNameFull;
    //     }
    //     
    //     var assignmentId = _assignmentGroups.GetAssignmentIdInWeightGroup(User.CanvasClaims().CanvasCourseId);
    //    var outcomeResults =
    //        _outcomeResultCollection.GetAllCards(User.CanvasClaims().CanvasCourseId, assignmentId, userId);
    //     return View(
    //         new AssignmentRubricViewModel(outcomeResults, studentsInCourse, userId, studentName,assignmentId)
    //     );
    // }
}