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
    public class MoPhongcache
    {
        private readonly IDistributedCache _cache;
        private readonly MoPhongService _moPhongService;
        private DistributedCacheEntryOptions _cacheOption;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public MoPhongcache(IDistributedCache cache, MoPhongService moPhongService)
        {
            _cache = cache;
            _moPhongService = moPhongService;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
        }
        public async Task<MoPhong> GetMoPhongByIdAsync(Guid Id)
        {
            var cacheKey = $"MoPhongById-{Id}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                var moPhongData = await _moPhongService.GetMoPhongByIdAsync(Id);
                var serializedData = JsonSerializer.Serialize(moPhongData, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return moPhongData;
            }
            else
            {
                var moPhongData = JsonSerializer.Deserialize<MoPhong>(cachedData, _serializerOptions);
                return moPhongData;
            }
        }
        public async Task AddAsync(MoPhong moPhong)
        {
            try
            {
                await _moPhongService.CreateMoPhongAsync(moPhong);
                var cacheKey = $"MoPhongById-{moPhong.Id}";
                var serializedData = JsonSerializer.Serialize(moPhong, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding MoPhong to cache", ex);
            }
        }
        public async Task UpdateAsync(MoPhong moPhong)
        {
            try
            {
                await _moPhongService.UpdateMoPhongAsync(moPhong);
                var cacheKey = $"MoPhongById-{moPhong.Id}";
                var serializedData = JsonSerializer.Serialize(moPhong, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating MoPhong in cache", ex);
            }
        }
        public async Task RemoveAsync(Guid Id)
        {
            try
            {
                var cacheKey = $"MoPhongById-{Id}";
                await _cache.RemoveAsync(cacheKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing MoPhong from cache", ex);
            }
        }

    }
}
