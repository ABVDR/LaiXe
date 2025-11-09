using Libs.Entity;
using Libs.Repositories;
using Libs.Service;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Libs.CacheService
{
    public class ChuDeCache
    {
        private readonly IDistributedCache _cache;
        private readonly ChuDeService _chuDeService;
        private DistributedCacheEntryOptions _cacheOption;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public ChuDeCache(IDistributedCache cache, ChuDeService chuDeService)
        {
            _cache = cache;
            _chuDeService = chuDeService;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)// thoi gian het han cache
            };
        }
        // Cache theo id
        public async Task<ChuDe?> GetChuDeByIdAsync(Guid Id)
        {
            var cacheKey = $"ChuDeById-{Id}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                var chuDeData = await _chuDeService.GetByIdAsync(Id);
                var serializedData = JsonSerializer.Serialize(chuDeData, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return chuDeData;
            }
            else
            {
                var chuDeData = JsonSerializer.Deserialize<ChuDe>(cachedData, _serializerOptions);
                return chuDeData;
            }
        }
        public async Task AddAsync(ChuDe chuDe)
        {
            try {
                await _chuDeService.AddAsync(chuDe);
                var cacheKey = $"ChuDeById-{chuDe.Id}";
                var serializedData = JsonSerializer.Serialize(chuDe, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ChuDeCache.AddAsync: {ex.Message}");
            }
        }
        public async Task UpdateAsync(ChuDe chuDe)
        {
            try {
                await _chuDeService.UpdateAsync(chuDe);
                var cacheKey = $"ChuDeById-{chuDe.Id}";
                var serializedData = JsonSerializer.Serialize(chuDe, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ChuDeCache.UpdateAsync: {ex.Message}");
            }
        }
    }
}
