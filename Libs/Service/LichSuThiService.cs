using Libs.Entity;
using Libs.Models;
using Libs.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class LichSuThiService
    {
        private readonly ILichSuThiRepository _lichSuThiRepository;

        public LichSuThiService(ILichSuThiRepository lichSuThiRepository)
        {
            _lichSuThiRepository = lichSuThiRepository;
        }

        public async Task<PageList<LichSuThi>> GetLichSuThiByUser(string userId, int pageNumber = 1, int pageSize = 10)
        {
            return await _lichSuThiRepository.GetLichSuThiByUserAsync(userId, pageNumber, pageSize);
        }

        public async Task<LichSuThiDetailModel> GetLichSuThiDetail(Guid lichSuThiId)
        {
            return await _lichSuThiRepository.GetLichSuThiDetailAsync(lichSuThiId);
        }

        public async Task<LichSuThiStatModel> GetLichSuThiStats(string userId)
        {
            return await _lichSuThiRepository.GetLichSuThiStatsAsync(userId);
        }

        public async Task<List<CauHoiSaiFrequencyModel>> GetFrequentWrongQuestions(string userId, int limit = 10)
        {
            return await _lichSuThiRepository.GetFrequentWrongQuestionsAsync(userId, limit);
        }

        public async Task<bool> DeleteLichSuThi(Guid lichSuThiId, string userId)
        {
            try
            {
                return await _lichSuThiRepository.DeleteLichSuThiAsync(lichSuThiId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LichSuThiService.DeleteLichSuThi: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveLichSuThi(LichSuThi lichSuThi, List<ChiTietLichSuThi> chiTietList)
        {
            try
            {
                if (lichSuThi == null)
                    throw new ArgumentNullException(nameof(lichSuThi));

                if (chiTietList == null || !chiTietList.Any())
                    throw new ArgumentException("Chi tiết lịch sử thi không được rỗng");

                return await _lichSuThiRepository.SaveLichSuThiAsync(lichSuThi, chiTietList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LichSuThiService.SaveLichSuThi: {ex.Message}");
                return false;
            }
        }

        //-----------//
        public async Task<List<object>> GetCauHoiLuyenLaiAsync(string userId)
        {
            var cauHois = await _lichSuThiRepository.GetCauHoiSaiByUserAsync(userId);
            var result = cauHois.Select(ch => new
            {
                ch.Id,
                ch.NoiDung,
                ch.LuaChonA,
                ch.LuaChonB,
                ch.LuaChonC,
                ch.LuaChonD,
                ch.DapAnDung,
                chuDe = ch.ChuDe == null ? null : new { ch.ChuDe.Id, ch.ChuDe.TenChuDe },
                mediaUrl = ch.MediaUrl
            }).ToList<object>();

            return result;
        }


        public async Task<List<object>> LuuKetQuaLuyenLaiAsync(string userId, Dictionary<Guid, string> cauHoiAnswers)
        {
            var results = new List<object>();

            foreach (var item in cauHoiAnswers)
            {
                Guid cauHoiId = item.Key;
                string dapAn = item.Value;

                var cauHoi = await _lichSuThiRepository.GetCauHoiByIdAsync(cauHoiId);
                if (cauHoi == null) continue;

                bool isCorrect = (dapAn == cauHoi.DapAnDung.ToString());
                var cauHoiSaiList = await _lichSuThiRepository.GetCauHoiSaiListAsync(userId, cauHoiId);

                if (isCorrect)
                {
                    if (cauHoiSaiList.Any())
                        await _lichSuThiRepository.XoaCauHoiSaisAsync(cauHoiSaiList);
                }
                else
                {
                    if (cauHoiSaiList.Any())
                    {
                        foreach (var itemSai in cauHoiSaiList)
                        {
                            itemSai.NgaySai = DateTime.Now;
                        }
                    }
                    else
                    {
                        await _lichSuThiRepository.AddCauHoiSaiAsync(new CauHoiSai
                        {
                            UserId = userId,
                            CauHoiId = cauHoiId,
                            NgaySai = DateTime.Now
                        });
                    }
                }

                results.Add(new
                {
                    CauHoiId = cauHoi.Id,
                    cauHoi.NoiDung,
                    cauHoi.LuaChonA,
                    cauHoi.LuaChonB,
                    cauHoi.LuaChonC,
                    cauHoi.LuaChonD,
                    DapAnDung = cauHoi.DapAnDung.ToString(),
                    DapAnNguoiDung = dapAn,
                    IsCorrect = isCorrect
                });
            }

            await _lichSuThiRepository.SaveChangesAsync();
            return results;
        }

    }
}