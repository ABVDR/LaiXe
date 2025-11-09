using Libs.Entity;
using System;
using System.Collections.Generic;

namespace Libs.Models
{
    public class LichSuThiDetailModel
    {
        public LichSuThi LichSuThi { get; set; }
        public List<ChiTietLichSuThi> ChiTietList { get; set; }
        public BaiThi BaiThi { get; set; }
    }

    public class LichSuThiStatModel
    {
        public int TongSoBaiThi { get; set; }
        public int SoBaiThiDat { get; set; }
        public int SoBaiThiKhongDat { get; set; }
        public double DiemTrungBinh { get; set; }
        public double TyLeDung { get; set; }
        public LichSuThi BaiThiGanNhat { get; set; }
    }

    public class CauHoiSaiFrequencyModel
    {
        public Guid CauHoiId { get; set; }
        public CauHoi CauHoi { get; set; }
        public int SoLanSai { get; set; }
        public DateTime NgaySaiGanNhat { get; set; }
    }
}