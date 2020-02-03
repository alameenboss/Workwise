using System.Collections.Generic;
using Workwise.Models;

namespace Workwise.Data.Interface
{
    public interface IUserProfileRepository
    {
        UserProfile GetByUserId(string UserId);
        void SaveUserImage(string userid, string imgPath);
        void SaveProfile(UserProfile profile);
        List<UserProfile> GetAllUsers();
    }
}