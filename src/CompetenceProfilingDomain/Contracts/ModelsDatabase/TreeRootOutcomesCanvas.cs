namespace CompetenceProfilingDomain.Contracts.ModelsDatabase;

public class TreeRootOutcomesCanvas
{
    public string LmsId { get; set; }

    public string Id { get; set; }

    public OutcomesCanvasDto OutcomeCanvas { get; set; }
    public TreeRootCanvasDto TreeRootCanvas { get; set; }
}