using System.Collections.Generic;

namespace Workwise.ViewModel
{
    public class MessageRecordViewModel
    {
        public MessageRecordViewModel()
        {
            Messages =new  List<ChatMessageViewModel>();
        }
        public IEnumerable<ChatMessageViewModel> Messages { get; set; }
        public int TotalMessages { get; set; }
        public int LastChatMessageId { get; set; }
    }
}
