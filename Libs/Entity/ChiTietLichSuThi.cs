namespace Libs.Entity
{
    public class ChiTietLichSuThi
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid LichSuThiId { get; set; } 
        public Guid CauHoiId { get; set; }    

        public char? CauTraLoi { get; set; }
        public bool? DungSai { get; set; }

        public LichSuThi LichSuThi { get; set; }
        public CauHoi CauHoi { get; set; }
    }
}
