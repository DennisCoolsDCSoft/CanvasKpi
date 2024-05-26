using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Extensions
{
    public static class DistributedCaching
    {
        public static void SetDistributedCache<T>(this IDistributedCache distributedCache, string key, T value, int timeSpanMinutes)
        {
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(timeSpanMinutes));
            distributedCache.SetDistributedCache(key,value,options);
        }
        private static void SetDistributedCache<T>(this IDistributedCache distributedCache, string key, T value,
            DistributedCacheEntryOptions options)
        {
            if (value != null)
                distributedCache.Set(key, value.Serialize<T>(), options);
        }

        public static T? GetDistributedCache<T>(this IDistributedCache distributedCache, string key) where T : class
        {
            var result = distributedCache.Get(key);
            return result != null ? result.Deserialize<T>() : null;
        }
    }
    
    public static class JsonSerializer
    {
        public static T? Deserialize<T>(this byte[] arrayToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(arrayToDeserialize));
        }

        public static byte[] Serialize<T>(this T objectToSerialize)
        {
            return Encoding.Default.GetBytes(JsonConvert.SerializeObject(objectToSerialize));
        }
    }
}
