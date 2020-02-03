using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.Data.Models;

namespace Workwise.Data.Interface
{
    public interface IUserProfileRepository
    {
        UserProfile GetByUserId(string UserId);
        void SaveUserImage(string userid, string imgPath);
        void SaveProfile(UserProfile profile);
        List<UserProfile> GetAllUsers();
        Task CreateUserProfileAsync(string userId, string userName);
    }
}