using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class ChatBoxService
    {
        private readonly IChatClient _chatClient;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _cacheOption;
        private readonly List<ChatMessage> _conversation = [];
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        private Guid sessionId;
        private readonly static string SYSTEM_DECRIPTION =
            """
            Bạn là một trợ lý am hiểu luật giao thông Việt Nam.
            Nếu người dùng gõ sai chính tả hoặc có lỗi đánh máy, hãy cố gắng suy luận ý nghĩa câu hỏi và trả lời chính xác nhất theo ngữ cảnh.
            Từ chối trả lời câu hỏi nếu câu hỏi hoàn toàn không liên quan đến giao thông.
            """;
        public ChatBoxService(IChatClient chatClient, IDistributedCache cache)
        {
            _chatClient = chatClient;
            _cache = cache;
            _cacheOption = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)// thoi gian het han cache
            };
            _conversation.Add(new ChatMessage(ChatRole.System, SYSTEM_DECRIPTION));
        }
        public async Task<List<ChatMessage>>? GetConversationAsync(Guid sessionId)
        {
            this.sessionId = sessionId;
            var cacheKey = $"ChatConversation-{sessionId}";
            var cachedConversation = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedConversation))
            {
                var conversation = JsonSerializer.Deserialize<List<ChatMessage>>(cachedConversation, _serializerOptions);
                _conversation.AddRange(conversation!.Skip(1));
                return _conversation;
            }
            return _conversation;
        }
        public async Task<string> GetAIResponseAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return string.Empty;
            _conversation.Add(new ChatMessage(ChatRole.User, prompt));
            var response = await _chatClient.GetResponseAsync(_conversation);
            if(sessionId == Guid.Empty)
                this.sessionId = Guid.NewGuid();
            var cacheKey = $"ChatConversation-{this.sessionId}";
            var cacheConverstion = JsonSerializer.Serialize<List<ChatMessage>>(_conversation, _serializerOptions);
            await _cache.SetStringAsync(cacheKey, cacheConverstion, _cacheOption);
            return response.Text;
        }
    }
}
