using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.ResultModel
{
    public class OnlineUserDetailViewModel
    {
        public string UserId { get; set; }
        public List<string> ConnectionId { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public bool IsOnline { get; set; }
        public int UnReadMessageCount { get; set; }
        public DateTime LastUpdationTime { get; set; }
    }
}
