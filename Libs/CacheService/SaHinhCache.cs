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
    public class SaHinhCache
    {
        private readonly IDistributedCache _cache;
        private readonly SaHinhService _saHinhService;
        private DistributedCacheEntryOptions _cacheOption;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public SaHinhCache(IDistributedCache cache, SaHinhService saHinhService)
        {
            _cache = cache;
            _saHinhService = saHinhService;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
        }
        public async Task<BaiSaHinh> GetSaHinhByIdAsync(Guid Id)
        {
            var cacheKey = $"SaHinhById-{Id}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                var saHinhData = await _saHinhService.GetBaiSaHinhByIdAsync(Id);
                var serializedData = JsonSerializer.Serialize(saHinhData, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return saHinhData;
            }
            else
            {
                var saHinhData = JsonSerializer.Deserialize<BaiSaHinh>(cachedData, _serializerOptions);
                return saHinhData;
            }
        }
        public async Task AddAsync(BaiSaHinh saHinh)
        {
            try
            {
                await _saHinhService.CreateBaiSaHinhAsync(saHinh);
                var cacheKey = $"SaHinhById-{saHinh.Id}";
                var serializedData = JsonSerializer.Serialize(saHinh, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding SaHinh to cache", ex);
            }
        }
        public async Task UpdateAsync(BaiSaHinh saHinh)
        {
            try
            {
                await _saHinhService.UpdateBaiSaHinhAsync(saHinh);
                var cacheKey = $"SaHinhById-{saHinh.Id}";
                var serializedData = JsonSerializer.Serialize(saHinh, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating SaHinh in cache", ex);
            }
        }
        public async Task RemoveAsync(Guid id)
        {
            try
            {
                var cacheKey = $"SaHinhById-{id}";
                await _cache.RemoveAsync(cacheKey);

            }
            catch (Exception ex)
            {
                throw new Exception("Error removing SaHinh from cache", ex);
            }
        }
    }
}
