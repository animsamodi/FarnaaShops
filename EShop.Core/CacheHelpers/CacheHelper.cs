using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheHelpers
{
    public static class CacheHelper
    {
        public static readonly TimeSpan LongCacheDuration = TimeSpan.FromMinutes(60);
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan LowCacheDuration = TimeSpan.FromMinutes(10);
 
 
        public static string GenerateProductCategoryCacheKey(long? catId,long? brandId,long? seriId)
        {
            var key = $"PCKey";
            if (catId != null)
                key += $"-C-{catId}";
            if (brandId != null)
                key += $"-B-{brandId}";
            if (seriId != null)
                key += $"-S-{seriId}";
            return key;
        }
        public static string GenerateProductCacheKey(long productId)
        {
            return $"ProductKey-{productId}";
        }
        public static string GenerateCategoryCacheKey(string title)
        {
            return $"CategoryKey-{title}";
        }
        public static string GenerateHomePageCacheKey()
        {
            return $"HomePage";
        }
      


    }
}
