using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ViewModel
{
    public class FriendRequestViewModel
    {
        public UserProfileViewModel UserInfo { get; set; }
        public string RequestStatus { get; set; }
        public string UserId { get; set; }
        public string EndUserId { get; set; }
    }
}
