using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace EShop.Core.Cache
{
  public class CacheService : ICacheService
{
    #region Constructor
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }
    #endregion

    public async Task<List<T>> TolistAsync<T>(string key)
    {
        try
        {
            string row = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(row))
                return null;
            return JsonSerializer.Deserialize<List<T>>(row);
        }
        catch (Exception ex)
        {
            // TODO:
            //logger.LogWarning("Not found any item in redis. EX:" + ex.Message);
        }
        return default(List<T>);
    }

    public async Task<bool> AddAsync<T>(string key, T data, TimeSpan? expireTime = null, bool isAbsoluteExpire = true)
    {
        try
        {
            var list = await TolistAsync<T>(key);
            if (list == null)
                return false;
            list.Add(data);
            var result = await SetAsync(key, list, expireTime, isAbsoluteExpire);
            return result;
        }
        catch (Exception ex)
        {
            //logger.LogWarning(ex.Message);
            return false;
        }
    }

    public async Task<bool> SetAsync<T>(string key, T model, TimeSpan? expireTime = null, bool isAbsoluteExpire = true)
    {
        try
        {
            string serializedObj = JsonSerializer.Serialize(model);

            if (expireTime != null)
                if (isAbsoluteExpire)
                    await _cache.SetStringAsync(key, serializedObj,
                        new DistributedCacheEntryOptions().SetAbsoluteExpiration(expireTime.Value));
                else
                    await _cache.SetStringAsync(key, serializedObj,
                        new DistributedCacheEntryOptions().SetSlidingExpiration(expireTime.Value));
            else
                await _cache.SetStringAsync(key, serializedObj);

            return true;
        }
        catch (Exception ex)
        {
            // TODO:
            //logger.LogError($"{key} key storage failed.");
            return false;
        }
    }

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            string row = await _cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(row))
                return JsonSerializer.Deserialize<T>(row);
        }
        catch (Exception ex)
        {
            // TODO:
            //logger.LogError(ex.Message);
        }
        return default(T);
    }

    public async Task<bool> RemoveCacheAsync(string key)
    {
        try
        {
            await _cache.RemoveAsync(key);
            return true;
        }
        catch
        {
            // TODO:
            // save a log here.
            return false;
        }
    }
}
}