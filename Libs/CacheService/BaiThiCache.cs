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
    public class BaiThiCache
    {
        private readonly IDistributedCache _cache;
        private readonly BaiThiService _baiThiService;
        private DistributedCacheEntryOptions _cacheOption;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public BaiThiCache(IDistributedCache cache, BaiThiService baiThiService)
        {
            _cache = cache;
            _baiThiService = baiThiService;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)// thoi gian het han cache
            };
        }
        // Cache theo id
        public async Task<BaiThi> GetBaiThiByIdAsync(Guid Id)
        {
            var cacheKey = $"BaiThiById-{Id}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                var baiThiData = await _baiThiService.GetBaiThiWithDetails(Id);
                var serializedData = JsonSerializer.Serialize(baiThiData, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return baiThiData;
            }
            else
            {
                var baiThiData = JsonSerializer.Deserialize<BaiThi>(cachedData, _serializerOptions);
                return baiThiData;
            }
        }
        //Xoa cach khi update hoac delete
        public async Task AddAsync(BaiThi baiThi)
        {
            try {

                await _baiThiService.AddAsync(baiThi);
                var cacheKey = $"BaiThiById-{baiThi.Id}";
                var serializedData = JsonSerializer.Serialize(baiThi, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
            }
            catch(Exception ex)
            {
                throw new Exception("Lỗi khi thêm bài thi: " + ex.Message);
            }
        }
        public async Task<BaiThi> DeleteAsync(BaiThi baiThi)
        {
            try
            {
                await _baiThiService.Delete(baiThi);
                var cacheKey = $"BaiThiById-{baiThi.Id}";
                var cacheData = await _cache.GetStringAsync(cacheKey);
                if(cacheData != null)
                {
                    await _cache.RemoveAsync(cacheKey);
                }
                var serializedData = JsonSerializer.Serialize(baiThi, _serializerOptions);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOption);
                return baiThi;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật bài thi: " + ex.Message);
            }
        }
    }
}
