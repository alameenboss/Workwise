using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;

namespace Workwise.ResultModel
{
    public class MessageRecordResultModel
    {
        public List<ChatMessage> Messages { get; set; }
        public int TotalMessages { get; set; }
        public int LastChatMessageId { get; set; }
    }
}
