namespace CompetenceProfilingDomain.Contracts.ModelsDatabase;

public class TreeRootCanvasDto
{
    //[Key]
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";

    public virtual ICollection<OutcomesCanvasDto> OutcomesCanvas { get; set; }
}