using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;

namespace Workwise.ResultModel
{
    public class UserSearchResultModel
    {
        public UserProfile UserInfo { get; set; }
        public string FriendRequestStatus { get; set; }
        public bool IsRequestReceived { get; set; }
    }
}
