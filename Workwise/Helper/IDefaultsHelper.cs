using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.Helper
{
    public interface IDefaultsHelper
    {
        MessageViewModel GetMessageModel(ChatMessageViewModel objentity);
        string GetProfilePicture(string profilePicture, string gender);
        UserViewModel GetUserModel(string id, UserProfileViewModel objentity = null, string friendRequestStatus = "", bool isRequestReceived = false);
        UserProfileViewModel GetUser(string userid);
    }
}