namespace Libs.Models
{
    public class ShareReport
    {
        public Guid Id { get; set; }

        // Cho phép báo cáo Share gốc
        public Guid? ShareId { get; set; }
        public Share? Share { get; set; }

        // Cho phép báo cáo Reply
        public Guid? ShareReplyId { get; set; }
        public ShareReply? ShareReply { get; set; }

        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
