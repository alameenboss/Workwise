using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.Data.Models
{
    public class FriendRequests
    {
        public UserProfile UserInfo { get; set; }
        public string RequestStatus { get; set; }
        public string RequestorUserId { get; set; }
        public string EndUserId { get; set; }
    }
}
