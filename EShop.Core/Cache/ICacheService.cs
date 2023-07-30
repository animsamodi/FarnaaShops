using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Cache
{
    public interface ICacheService
    {
        Task<List<T>> TolistAsync<T>(string key);

        Task<bool> AddAsync<T>(string key, T data, TimeSpan? expireTime = null, bool isAbsoluteExpire = true);

        Task<bool> SetAsync<T>(string key, T model, TimeSpan? expireTime = null, bool isAbsoluteExpire = true);

        Task<T> GetAsync<T>(string key);

        Task<bool> RemoveCacheAsync(string key);
    }
}