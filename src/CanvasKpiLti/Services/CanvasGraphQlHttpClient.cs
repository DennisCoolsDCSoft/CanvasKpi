using System.Net.Http;
using System.Net.Http.Headers;
using CompetenceProfilingDomain.Contracts.Infrastructure;

namespace CanvasKpiLti.Services;

public class CanvasGraphQlHttpClient : ICanvasGraphQlHttpClient
{
    private readonly HttpClient _httpClient;

    public CanvasGraphQlHttpClient(HttpClient httpClient, TokenStore tokenStore)
    {
        _httpClient = httpClient;
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenStore.Token);
    }

    public HttpClient HttpClient => _httpClient;
}