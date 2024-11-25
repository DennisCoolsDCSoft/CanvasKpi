using CompetenceProfilingDomain.Definitions;
using CompetenceProfilingDomain.DomainCp;

namespace CompetenceProfilingDomainTests;

public class OutcomeGroupUnitTest
{
   
    // [Fact]
    // public void OutcomeGroup_IfTwoOfThreeAreTrueThenMastered_Succeed()
    // {
    //     var list = new List<OutcomeResult>
    //     {
    //         new ("criteriaId01", "outcomeId01", "description01", "longDescription01",
    //                 ArchitectureHboEnum.NotFound, CompetencesHboEnum.NotFound, LevelsEnum.NotFound, 1) 
    //             { Points = null },
    //         new ("criteriaId02", "outcomeId02", "description02", "longDescription02",
    //                 ArchitectureHboEnum.NotFound, CompetencesHboEnum.NotFound, LevelsEnum.NotFound, 2)
    //             { Points = PointScale.Mastered },
    //         new ("criteriaId03", "outcomeId03", "description03", "longDescription03",
    //                 ArchitectureHboEnum.NotFound, CompetencesHboEnum.NotFound, LevelsEnum.NotFound, 3)
    //             { Points = PointScale.Mastered }
    //     };
    //     
    //     var outcomegroup = new OutcomeGroup(LevelsEnum.L1, list);
    //     Assert.True(outcomegroup.MasteredOutcomeGroup);
    // }
    //
    // [Fact]
    // public void OutcomeGroup_IfTwoOfThreeAreTrueThenMastered_Failed()
    // {
    //     var list = new List<OutcomeResult>
    //     {
    //         new ("criteriaId01", "outcomeId01", "description01", "longDescription01",
    //                 ArchitectureHboEnum.NotFound, CompetencesHboEnum.NotFound, LevelsEnum.NotFound, 1) 
    //             { Points = null },
    //         new ("criteriaId02", "outcomeId02", "description02", "longDescription02",
    //                 ArchitectureHboEnum.NotFound, CompetencesHboEnum.NotFound, LevelsEnum.NotFound, 2)
    //             { Points = PointScale.StudentAdvice },
    //         new ("criteriaId03", "outcomeId03", "description03", "longDescription03",
    //                 ArchitectureHboEnum.NotFound, CompetencesHboEnum.NotFound, LevelsEnum.NotFound, 3)
    //             { Points = PointScale.Mastered }
    //     };
    //     
    //     var outcomegroup = new OutcomeGroup(LevelsEnum.L1, list);
    //     Assert.False(outcomegroup.MasteredOutcomeGroup);
    // }
}