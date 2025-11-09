namespace Libs.Models
{
    public class SubmitBaiThiRequest
    {
        public Guid BaiThiId { get; set; }

        public Dictionary<Guid, string> Answers { get; set; }  // key là Id của ChiTietBaiThi

    }
}
