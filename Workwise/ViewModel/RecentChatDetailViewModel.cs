using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.ViewModel
{
    public class RecentChatDetailViewModel
    {
        public List<OnlineUserDetailViewModel> Users { get; set; }
        public int LastUserId { get; set; }
    }
}
