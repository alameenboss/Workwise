using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Workwise.Data.Interface;
using System.Web;
using Workwise.Data.Models;

namespace Workwise.Data
{
    public class MessageRepository : IMessage
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ChatMessage SaveChatMessage(ChatMessage objentity)
        {
            _context.ChatMessages.Add(objentity);
            _context.SaveChanges();
            return objentity;
        }
        public MessageRecords GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            MessageRecords obj = new MessageRecords();
            var messages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserId == toUserId || m.FromUserId == toUserId) && (m.ToUserId == currentUserId || m.FromUserId == currentUserId)).OrderByDescending(m => m.CreatedOn);
            if (lastMessageId > 0)
            {
                obj.Messages = messages.Where(m => m.ChatMessageId < lastMessageId).Take(20).ToList().OrderBy(m => m.CreatedOn).ToList();
            }
            else
            {
                obj.Messages = messages.Take(20).ToList().OrderBy(m => m.CreatedOn).ToList();
            }
            obj.LastChatMessageId = obj.Messages.OrderBy(m => m.ChatMessageId).Select(m => m.ChatMessageId).FirstOrDefault();
            return obj;
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            var unreadMessages = _context.ChatMessages.Where(m => m.Status == "Sent" && m.ToUserId == currentUserId && m.FromUserId == fromUserId && m.IsActive == true).ToList();
            unreadMessages.ForEach(m =>
            {
                m.Status = "Viewed";
                m.ViewedOn = System.DateTime.Now;
            });
            _context.SaveChanges();
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
            var unreadMessages = _context.ChatMessages.Where(m => m.ChatMessageId == messageId).FirstOrDefault();
            if (unreadMessages != null)
            {
                unreadMessages.Status = "Viewed";
                unreadMessages.ViewedOn = System.DateTime.Now;
                _context.SaveChanges();
            }
        }
    }

}