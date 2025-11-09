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
    public class CauHoiCache
    {
        private readonly IDistributedCache _Cache;
        private readonly CauHoiService _cauHoiService;
        private DistributedCacheEntryOptions _cacheOption;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public CauHoiCache(IDistributedCache distributedCache, CauHoiService cauHoiService)
        {
            _Cache = distributedCache;
            _cauHoiService = cauHoiService;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
        }
        public async Task<CauHoi> GetCauHoiByIdAsync(Guid Id)
        {
            var cacheKey = $"CauHoiById-{Id}";
            var cachedData = await _Cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                var cauHoiData = await _cauHoiService.GetCauHoiByIdAsync(Id);
                var serializedData = JsonSerializer.Serialize(cauHoiData, _serializerOptions);
                await _Cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return cauHoiData;
            }
            else
            {
                var cauHoiData = JsonSerializer.Deserialize<CauHoi>(cachedData, _serializerOptions);
                return cauHoiData;
            }
        }
        public async Task AddAsync(CauHoi cauHoi)
        {
            try
            {
                await _cauHoiService.CreateCauHoiAsync(cauHoi);
                var cacheKey = $"CauHoiById-{cauHoi.Id}";
                var serializedData = JsonSerializer.Serialize(cauHoi, _serializerOptions);
                await _Cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding CauHoi to cache", ex);
            }
        }
        public async Task UpdateAsync(CauHoi cauHoi)
        {
            try
            {
                await _cauHoiService.UpdateCauHoiAsync(cauHoi);
                var cacheKey = $"CauHoiById-{cauHoi.Id}";
                var serializedData = JsonSerializer.Serialize(cauHoi, _serializerOptions);
                await _Cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating CauHoi in cache", ex);
            }
        }
        public async Task RemoveAsync(Guid Id)
        {
            try
            {
                var cacheKey = $"CauHoiById-{Id}";
                await _Cache.RemoveAsync(cacheKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing CauHoi from cache", ex);
            }
        }
    }
}
