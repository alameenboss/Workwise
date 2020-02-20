﻿using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data.Interface;
using Workwise.Helper;
using Workwise.ViewModel;
namespace Workwise.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(IUserService UserProfileRepo)
        {
            this._userService = UserProfileRepo;
        }
        public ActionResult Chat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getusers()
        {

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            var objmodel = DefaultsHelper.GetUserModel(User.Identity.GetUserId());
            return View(objmodel);
        }
        [HttpPost]
        public ActionResult EditProfile(UserViewModel model)
        {
            var entity = _userService.GetUserById(User.Identity.GetUserId());
            entity.FirstName = model.Name;
            entity.Gender = model.Gender;
            entity.DOB = Convert.ToDateTime(model.DOB);
            entity.Bio = model.Bio;
            entity.UpdatedOn = System.DateTime.Now;
            _userService.SaveProfile(entity);
            return RedirectToAction("Profile");
        }

        public ActionResult SerachUser(string search)
        {
            var userList = _userService.SerachUser(search);
            return View(@"~\Views\Profiles\Index.cshtml",userList);
        }

        public ActionResult _UserSearchResult(string name)
        {
            var userList = _userService.SearchUsers(name, User.Identity.GetUserId());
            var objmodel = userList.Select(m => DefaultsHelper.GetUserModel(m.UserInfo.UserId, m.UserInfo, m.FriendRequestStatus, m.IsRequestReceived)).ToList();
            return PartialView(objmodel);
        }
        public ActionResult _OnlineFriends()
        {
            var onlineFriends = _userService.GetOnlineFriends(User.Identity.GetUserId());
            var objmodel = onlineFriends.Select(m => new UserViewModel()
            {
                UserId = m.UserId,
                Name = m.Name,
                ProfilePicture = DefaultsHelper.GetProfilePicture(m.ProfilePicture, m.Gender)
            }).ToList();
            return Json(objmodel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _UserNotifications()
        {
            var notifications = _userService.GetUserNotifications(User.Identity.GetUserId());
            var objmodel = notifications.Select(m => new UserNotificationViewModel()
            {
                NotificationId = m.NotificationId,
                NotificationType = m.NotificationType,
                User = DefaultsHelper.GetUserModel("", m.User),
                NotificationStatus = m.NotificationStatus,
                CreatedOn = m.CreatedOn,
                TotalNotifications = m.TotalNotifications
            }).ToList();
            return PartialView(objmodel);
        }
        public ActionResult _RecentChats()
        {
            var recentChats = _userService.GetRecentChats(User.Identity.GetUserId());
            var objmodel = recentChats.Select(m => new UserViewModel()
            {
                UserId = m.UserId,
                Name = m.Name,
                ProfilePicture = DefaultsHelper.GetProfilePicture(m.ProfilePicture, m.Gender),
                IsOnline = m.IsOnline,
                UnReadMessages = m.UnReadMessageCount > 0 ? Convert.ToString(m.UnReadMessageCount) : ""
            }).ToList();
            return PartialView(objmodel);
        }
        [HttpPost]
        public ActionResult UpdateProfilePicture(HttpPostedFileBase profilePicture, string UserId)
        {
            try
            {
                string filePath = string.Empty;
                if (profilePicture != null)
                {
                    string folderpath = Server.MapPath("~/") + "Content/Images";
                    if (!System.IO.Directory.Exists(folderpath))
                    {
                        System.IO.Directory.CreateDirectory(folderpath);
                    }
                    string path = Server.MapPath("~/Content/Images/ProfilePictures");
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    Random r = new Random();
                    int randomNo = r.Next();
                    filePath = "/Content/Images/ProfilePictures/" + randomNo + "_" + profilePicture.FileName;
                    profilePicture.SaveAs(Server.MapPath("~/Content/Images/ProfilePictures/" + randomNo + "_" + profilePicture.FileName));
                    _userService.UpdateUserProfilePicture(UserId, filePath);
                    SessionHelper.UserImage = filePath;
                    return Json(new { success = true, filePath = filePath }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, filePath = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                return Json(new { success = false, filePath = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Friends()
        {
            var friendUsers = _userService.GetFriends(User.Identity.GetUserId());
            var objmodel = friendUsers.Select(m => new UserViewModel()
            {
                UserId = m.UserId,
                Name = m.Name,
                ProfilePicture = DefaultsHelper.GetProfilePicture(m.ProfilePicture, m.Gender),
                IsOnline = m.IsOnline,
            }).ToList();
            return View(objmodel);
        }
    }

}