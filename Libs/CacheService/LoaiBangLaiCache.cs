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
    public class LoaiBangLaiCache
    {
        private readonly IDistributedCache _cache;
        private readonly LoaiBangLaiService _loaiBangLaiService;
        private DistributedCacheEntryOptions _cacheOption;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public LoaiBangLaiCache(IDistributedCache cache, LoaiBangLaiService loaiBangLaiService)
        {
            _cache = cache;
            _loaiBangLaiService = loaiBangLaiService;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
        }
        public async Task<LoaiBangLai> GetLoaiBangLaiByIdAsync(Guid Id)
        {
            var cacheKey = $"LoaiBangLaiById-{Id}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                var loaiBangLaiData = await _loaiBangLaiService.GetByIdAsync(Id);
                var serializedData = JsonSerializer.Serialize(loaiBangLaiData, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return loaiBangLaiData;
            }
            else
            {
                var loaiBangLaiData = JsonSerializer.Deserialize<LoaiBangLai>(cachedData, _serializerOptions);
                return loaiBangLaiData;
            }
        }
        public async Task AddAsync(LoaiBangLai loaiBangLai)
        {
            try
            {
                await _loaiBangLaiService.CreateLoaiBangLaiAsync(loaiBangLai);
                var cacheKey = $"LoaiBangLaiById-{loaiBangLai.Id}";
                var serializedData = JsonSerializer.Serialize(loaiBangLai, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding LoaiBangLai to cache", ex);
            }
        }
        public async Task UpdateAsync(LoaiBangLai loaiBangLai)
        {
            try
            {
                var cacheKey = $"LoaiBangLaiById-{loaiBangLai.Id}";
                var serializedData = JsonSerializer.Serialize(loaiBangLai, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating LoaiBangLai in cache", ex);
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            await _loaiBangLaiService.DeleteLoaiBangLaiAsync(id);
            await _cache.RemoveAsync($"LoaiBangLaiById-{id}");
            await _cache.RemoveAsync("AllLoaiBangLai");
        }
        public async Task<List<LoaiBangLai>> GetAllLoaiBangLaiAsync()
        {
            var cacheKey = "AllLoaiBangLai";
            var cachedData = await _cache.GetStringAsync(cacheKey);

            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<List<LoaiBangLai>>(cachedData, _serializerOptions);
            }

            var list = await _loaiBangLaiService.GetDanhSachLoaiBangLaiAsync();
            var serializedData = JsonSerializer.Serialize(list, _serializerOptions);
            await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);

            return list;
        }

    }
}
