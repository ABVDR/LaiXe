using Libs.Entity;

public class KetQuaBaiThi
{
    public Guid BaiThiId { get; set; }       // ✅ từ int → Guid
    public Guid CauHoiId { get; set; }       // ✅ từ int → Guid
    public CauHoi CauHoi { get; set; }
    public char? CauTraLoi { get; set; }
    public char DapAnDung { get; set; }
    public bool DungSai { get; set; }
}
