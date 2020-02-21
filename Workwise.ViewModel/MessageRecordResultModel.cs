using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ViewModel
{
    public class MessageRecordViewModel
    {
        public List<ChatMessageViewModel> Messages { get; set; }
        public int TotalMessages { get; set; }
        public int LastChatMessageId { get; set; }
    }
}
