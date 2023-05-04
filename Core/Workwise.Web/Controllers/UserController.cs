using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServiceAgent _userServiceAgent;
        private readonly IDefaultsHelper _defaultHelper;
        public UserController(
            IUserServiceAgent userServiceAgent,
            IDefaultsHelper defaultHelper)
        {
            this._userServiceAgent = userServiceAgent;
            _defaultHelper = defaultHelper;
        }

        public ActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUsers()
        {
            return Ok(true);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            var objmodel = _defaultHelper.GetUserModel("alameen02111988");
            return View(objmodel);
        }

        [HttpPost]
        public ActionResult EditProfile(UserViewModel model)
        {
            var entity = _userServiceAgent.GetUserById("alameen02111988");
            entity.FirstName = model.Name;
            entity.Gender = model.Gender;
            entity.DOB = Convert.ToDateTime(model.DOB);
            entity.Bio = model.Bio;
            entity.UpdatedOn = DateTime.Now;
            _userServiceAgent.SaveProfile(entity);
            return RedirectToAction("Profile");
        }

        public ActionResult SerachUser(string search)
        {
            var userList = _userServiceAgent.SerachUser(search);
            return View(@"~\Views\Profiles\Index.cshtml",userList);
        }

        public ActionResult _UserSearchResult(string name)
        {
            var userList = _userServiceAgent.SearchUsers(name, "alameen02111988");
            var objmodel = userList.Select(m => _defaultHelper.GetUserModel(m.UserInfo.UserId, m.UserInfo, m.FriendRequestStatus, m.IsRequestReceived)).ToList();
            return PartialView(objmodel);
        }
        public ActionResult _OnlineFriends()
        {
            var onlineFriends = _userServiceAgent.GetOnlineFriends("alameen02111988");
            var objmodel = onlineFriends.Select(m =>  new UserViewModel()
            {
                UserId = m.UserId,
                Name = m.Name,
                ProfilePicture = _defaultHelper.GetProfilePicture(m.ProfilePicture, m.Gender)
            }).ToList();
            return Ok(objmodel);
        }
        public ActionResult _UserNotifications()
        {
            var notifications = _userServiceAgent.GetUserNotifications("alameen02111988");
            var objmodel = notifications.Select(m => new UserNotificationViewModel()
            {
                NotificationId = m.NotificationId,
                NotificationType = m.NotificationType,
                User = _defaultHelper.GetUserModel("", m.User),
                NotificationStatus = m.NotificationStatus,
                CreatedOn = m.CreatedOn,
                TotalNotifications = m.TotalNotifications
            }).ToList();
            return PartialView(objmodel);
        }
        public ActionResult _RecentChats()
        {
            var recentChats = _userServiceAgent.GetRecentChats("alameen02111988");
            var objmodel = recentChats.Select(m => new UserViewModel()
            {
                UserId = m.UserId,
                Name = m.Name,
                ProfilePicture = _defaultHelper.GetProfilePicture(m.ProfilePicture, m.Gender),
                IsOnline = m.IsOnline,
                UnReadMessages = m.UnReadMessageCount > 0 ? Convert.ToString(m.UnReadMessageCount) : ""
            }).ToList();
            return PartialView(objmodel);
        }
        [HttpPost]
        public ActionResult UpdateProfilePicture(IFormFile profilePicture, string UserId)
        {
            try
            {
                if (profilePicture != null)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        profilePicture.CopyToAsync(stream);
                    }

                    //Random r = new Random();
                    //int randomNo = r.Next();
                    //filePath = "/Content/Images/ProfilePictures/" + randomNo + "_" + profilePicture.FileName;
                    //profilePicture.SaveAs(Server.MapPath("~/Content/Images/ProfilePictures/" + randomNo + "_" + profilePicture.FileName));
                    _userServiceAgent.UpdateUserProfilePicture(UserId, filePath);
                    SessionHelper.UserImage = filePath;
                    return Ok(new { success = true, filePath = filePath });
                }
                return Ok(new { success = false, filePath = "" });
            }
            catch (Exception )
            {
                return Ok(new { success = false, filePath = "" });
            }
        }
        public ActionResult Friends()
        {
            var friendUsers = _userServiceAgent.GetFriends("alameen02111988");
            var objmodel = friendUsers.Select(m => new UserViewModel()
            {
                UserId = m.UserId,
                Name = m.Name,
                ProfilePicture = _defaultHelper.GetProfilePicture(m.ProfilePicture, m.Gender),
                IsOnline = m.IsOnline,
            }).ToList();
            return View(objmodel);
        }
    }

}