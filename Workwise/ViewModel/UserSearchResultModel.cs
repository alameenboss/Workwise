using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ViewModel
{
    public class UserSearchViewModel
    {
        public UserProfileViewModel UserInfo { get; set; }
        public string FriendRequestStatus { get; set; }
        public bool IsRequestReceived { get; set; }
    }
}
