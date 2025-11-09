namespace Libs.Models
{
    public class ShareReply
    {
        public Guid id { get; set; }
        public Guid ShareId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string? UserName { get; set; }

        public Guid? ParentReplyId { get; set; }
        public Share Share { get; set; }
    }
}
