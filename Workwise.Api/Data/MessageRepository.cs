﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Workwise.Data.Interface;
using System.Web;
using Workwise.Model;
using Workwise.ResultModel;
using Dapper;
using System.Xml.XPath;
using System.Threading.Tasks;

namespace Workwise.Data
{
    public class MessageRepository : BaseRepository,IMessageRepository
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ChatMessage SaveChatMessage(ChatMessage objentity)
        {
            _context.ChatMessages.Add(objentity);
            _context.SaveChanges();
            return objentity;
        }
        public async Task<MessageRecordResultModel> GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            var result = new MessageRecordResultModel();
            //var messages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserId == toUserId || m.FromUserId == toUserId) && (m.ToUserId == currentUserId || m.FromUserId == currentUserId)).OrderByDescending(m => m.CreatedOn);
            //if (lastMessageId > 0)
            //{
            //    obj.Messages = messages.Where(m => m.ChatMessageId < lastMessageId).OrderBy(m => m.CreatedOn).Take(20).ToList();
            //}
            //else
            //{
            //    obj.Messages = messages.OrderBy(m => m.CreatedOn).Take(20).ToList();
            //}
            //obj.LastChatMessageId = obj.Messages.OrderBy(m => m.ChatMessageId).Select(m => m.ChatMessageId).FirstOrDefault();
            //return obj;

            var parameter = new DynamicParameters();
            parameter.AddDynamicParams(new
            {
                UserId = currentUserId,
                ToUserId = toUserId,
                LastMessageId = lastMessageId
            });
            var messages = await QueryData<IEnumerable<ChatMessage>>("GetChatMessagesByUserId", parameter);
            result.Messages = messages.ToList();
            result.LastChatMessageId = result.Messages.OrderBy(m => m.ChatMessageId).Select(m => m.ChatMessageId).LastOrDefault();
            return result;
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