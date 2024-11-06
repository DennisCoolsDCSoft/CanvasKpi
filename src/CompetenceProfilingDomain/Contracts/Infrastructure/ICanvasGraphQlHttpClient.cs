namespace CompetenceProfilingDomain.Contracts.Infrastructure;

public interface ICanvasGraphQlHttpClient
{
    HttpClient HttpClient { get; }
}