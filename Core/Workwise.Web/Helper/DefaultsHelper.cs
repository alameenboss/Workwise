using System;
using System.Threading.Tasks;
using Workwise.ServiceAgent;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Helper
{

    public class DefaultsHelper : IDefaultsHelper
    {
        private readonly IUserServiceAgent _userServiceAgent = null;

        public DefaultsHelper(IUserServiceAgent userServiceAgent)
        {
            _userServiceAgent = userServiceAgent;
        }
        public string GetProfilePicture(string profilePicture, string gender)
        {
            string profilePicturePath = "";
            if (string.IsNullOrEmpty(profilePicture))
            {
                if (gender == "Female")
                {
                    profilePicturePath = "/Content/Images/female-default-pic.jpg";
                }
                else
                {
                    profilePicturePath = "/Content/Images/male-default-pic.jpg";
                }
            }
            else
            {
                profilePicturePath = profilePicture;
            }
            return profilePicturePath;
        }
        public UserViewModel GetUserModel(string id, UserProfileViewModel objentity = null, string friendRequestStatus = "", bool isRequestReceived = false)
        {
            var user = new UserProfileViewModel();
            if (objentity != null)
            {
                user = objentity;
            }
            else
            {
                user = _userServiceAgent.GetUserById(id);
            }
            UserViewModel objmodel = new UserViewModel();
            if (user != null)
            {
                objmodel.IsRequestReceived = isRequestReceived;
                objmodel.FriendRequestStatus = friendRequestStatus;
                objmodel.UserId = user.UserId;
                objmodel.Name = user.FirstName;
                objmodel.ProfilePicture = GetProfilePicture(user.ImageUrl, user.Gender);
                objmodel.Gender = user.Gender;
                objmodel.DOB = user.DOB?.ToShortDateString();
                if (user.DOB != null)
                {
                    objmodel.Age = Convert.ToString(Math.Floor(DateTime.Now.Subtract(Convert.ToDateTime(user.DOB)).TotalDays / 365.0)) + " Years";
                }
                else
                {
                    objmodel.Age = "NaN";
                }
                objmodel.Bio = user.Bio;
            }
            return objmodel;
        }

        public MessageViewModel  GetMessageModel(ChatMessageViewModel objentity)
        {
            MessageViewModel objmodel = new MessageViewModel();
            if (objentity != null)
            {
                objmodel.ChatMessageId = objentity.ChatMessageId;
                objmodel.FromUserId = objentity.FromUserId;
                objmodel.ToUserId = objentity.ToUserId;
                objmodel.Message = objentity.Message;
                objmodel.Status = objentity.Status;
                objmodel.CreatedOn = Convert.ToString(objentity.CreatedOn);
                objmodel.UpdatedOn = Convert.ToString(objentity.UpdatedOn);
                objmodel.ViewedOn = Convert.ToString(objentity.ViewedOn);
                objmodel.IsActive = objentity.IsActive;
            }
            return objmodel;
        }

        public UserProfileViewModel GetUser(string userid)
        {

            if (SessionHelper.Get<UserProfileViewModel>(userid) == null)
            {
                var model = _userServiceAgent.GetByUserId(userid);
                if (model == null)
                {
                    model = new UserProfileViewModel()
                    {
                        FirstName = userid,
                        ImageUrl = @"\images\DefaultPhoto.png"
                    };
                }
                SessionHelper.Set<UserProfileViewModel>(userid, model);
                SessionHelper.UserImage = model.ImageUrl;
                SessionHelper.UserName = model.FirstName;
                SessionHelper.UserId = userid;
            }

            return SessionHelper.Get<UserProfileViewModel>(userid);

        }
    }
}