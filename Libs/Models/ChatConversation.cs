using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Models
{
    public class ChatConversation
    {
        public readonly Guid Id = Guid.NewGuid();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
    public class Message
    {
        public string Role { get; set; } = "user";
        public string Content { get; set; } = string.Empty;
    }
}
