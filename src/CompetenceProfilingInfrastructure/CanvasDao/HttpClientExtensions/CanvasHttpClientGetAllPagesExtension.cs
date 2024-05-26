using System.Net;

namespace CompetenceProfilingInfrastructure.CanvasDao.HttpClientExtensions;

public static class CanvasHttpClientGetAllPagesExtension
{
    public class HttpResult
    {
        public string Result { get; set; } = "";
        public HttpStatusCode StatusCode { get; set; }
    }
    
    public static async Task<HttpResult> GetAllPagesAsync(this HttpClient httpClient, string commandUrl, int pageSize = 100)
    {
        var ret = new HttpResult();
        commandUrl = commandUrl + $"&page=1&per_page={pageSize}";
        
        var resFirstOne = await httpClient.InternalGet(commandUrl);

        if (resFirstOne.IsSuccessStatusCode)
        {
            ret.Result = await resFirstOne.Content.ReadAsStringAsync();
            ret.StatusCode = resFirstOne.StatusCode;

            ret = await InternalGetAllPage(httpClient,resFirstOne, ret);
        }
        else
        {
            throw new CanvasHttpException(resFirstOne.StatusCode, await resFirstOne.Content.ReadAsStringAsync());
        }
        
        return ret;
    }

    private class LinkNode
    {
        public LinkNode(string link, HttpClient httpClient)
        {
            Url = link.Split(';')
                .First()
                .TrimStart('<')
                .TrimEnd('>');

            Command = Url.Replace("" + httpClient.BaseAddress, "");

            Rel = link.Split(';')
                .Last()
                .TrimStart()
                .Replace("rel=\"","")
                .TrimEnd('"');
        }
        public string Url { get; set; }
        public string Command { get; set; }
        public string Rel { get; set; }
    }
    
    private static async Task<HttpResult> InternalGetAllPage(this HttpClient httpClient,
        HttpResponseMessage previousResponse, HttpResult ret)
    {
        if (!previousResponse.Headers.TryGetValues("Link", out var link)) return ret;
        var linkNodes = link.First().Split(',').Select(s => new LinkNode(s,httpClient)).ToList();

        var currentPageUrl = linkNodes.FirstOrDefault(w => w.Rel == "current");
        var nextPage = linkNodes.FirstOrDefault(w => w.Rel == "next");
        var lastPage = linkNodes.FirstOrDefault(w => w.Rel == "last");
        if (lastPage != null && currentPageUrl !=null)
        {
            if (currentPageUrl.Command == lastPage.Command) return ret;
        }
        
        var next = nextPage?.Command ?? "";

        var nextResult = httpClient.InternalGet(next).Result;
        if (!nextResult.IsSuccessStatusCode) throw new Exception($"http getAll pages error {nextResult.StatusCode}");

        ret.StatusCode = nextResult.StatusCode;
        ret.Result = ret.Result.TrimEnd(']');
        ret.Result += ",";
        ret.Result += nextResult.Content.ReadAsStringAsync().Result.TrimStart('[');

        return await httpClient.InternalGetAllPage(nextResult, ret);
    }

    private static async Task<HttpResponseMessage> InternalGet(this HttpClient httpClient,string url, int retry=4)
    {
        var ret = await httpClient.GetAsync(url);
        
        if (retry == 0)
        {
            return ret;
        }
         
        if (!ret.IsSuccessStatusCode)
        {
            retry--;
            return await InternalGet(httpClient,url, retry);
        }
        return ret;
    }
    
     public class CanvasHttpException:Exception
     {
         public CanvasHttpException(HttpStatusCode statusCode,string message) : base(message: message)
         {
             StatusCode = statusCode;
         }
         public HttpStatusCode StatusCode { get;}
     }
}