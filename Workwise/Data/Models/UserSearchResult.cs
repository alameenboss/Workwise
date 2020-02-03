using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.Data.Models
{
    public class UserSearchResult
    {
        public User UserInfo { get; set; }
        public string FriendRequestStatus { get; set; }
        public bool IsRequestReceived { get; set; }
    }
}
