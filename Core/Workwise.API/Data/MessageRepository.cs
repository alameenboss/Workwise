using Workwise.API.Data.EFCore;
using Workwise.API.Data.Interface;
using Workwise.API.Model.ResultModel;
using Workwise.API.Models;

namespace Workwise.API.Data
{
    public class MessageRepository : IMessageRepository
    {
        //GetChatMessagesByUserId

        private readonly WorkwiseDbContext workwiseDbContext;

        public MessageRepository(WorkwiseDbContext workwiseDbContext)
        {
            this.workwiseDbContext = workwiseDbContext;
        }
        public ChatMessage SaveChatMessage(ChatMessage objentity)
        {
            _ = workwiseDbContext.ChatMessages.Add(objentity);
            _ = workwiseDbContext.SaveChanges();
            return objentity;
        }
        public Task<MessageRecordResultModel> GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            MessageRecordResultModel result = new();
            IOrderedQueryable<ChatMessage> messages = workwiseDbContext.ChatMessages.Where(m => m.IsActive == true && (m.ToUserId == toUserId || m.FromUserId == toUserId) && (m.ToUserId == currentUserId || m.FromUserId == currentUserId)).OrderByDescending(m => m.CreatedOn);
            result.Messages = lastMessageId > 0
                ? messages.Where(m => m.ChatMessageId < lastMessageId).OrderBy(m => m.CreatedOn).Take(20).ToList()
                : messages.OrderBy(m => m.CreatedOn).Take(20).ToList();
            result.LastChatMessageId = result.Messages.OrderBy(m => m.ChatMessageId).Select(m => m.ChatMessageId).FirstOrDefault();
            return Task.FromResult(result);

            //var parameter = new DynamicParameters();
            //parameter.AddDynamicParams(new
            //{
            //    UserId = currentUserId,
            //    ToUserId = toUserId,
            //    LastMessageId = lastMessageId
            //});
            //var messages = await QueryData<IEnumerable<ChatMessage>>("GetChatMessagesByUserId", parameter);
            //result.Messages = messages.ToList();
            //result.LastChatMessageId = result.Messages.OrderBy(m => m.ChatMessageId).Select(m => m.ChatMessageId).LastOrDefault();
            //return result;
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            List<ChatMessage> unreadMessages = workwiseDbContext.ChatMessages.Where(m => m.Status == "Sent" && m.ToUserId == currentUserId && m.FromUserId == fromUserId && m.IsActive == true).ToList();
            unreadMessages.ForEach(m =>
            {
                m.Status = "Viewed";
                m.ViewedOn = DateTime.Now;
            });
            _ = workwiseDbContext.SaveChanges();
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
            ChatMessage? unreadMessages = workwiseDbContext.ChatMessages.Where(m => m.ChatMessageId == messageId).FirstOrDefault();
            if (unreadMessages != null)
            {
                unreadMessages.Status = "Viewed";
                unreadMessages.ViewedOn = DateTime.Now;
                _ = workwiseDbContext.SaveChanges();
            }
        }
    }

}