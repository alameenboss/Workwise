using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;

namespace Workwise.ResultModel
{
    public class FriendRequestResultModel
    {
        public UserProfile UserInfo { get; set; }
        public string RequestStatus { get; set; }
        public string UserId { get; set; }
        public string EndUserId { get; set; }
    }
}
