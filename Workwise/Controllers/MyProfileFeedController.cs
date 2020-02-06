﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data.Interface;
using Workwise.Data.Models;

namespace Workwise.Controllers
{

    [Authorize]
    public class MyProfileFeedController : BaseController
    {
        private readonly IUserRepository _userProfileRepo;
        private readonly IPostRepository _postrepository;


        public MyProfileFeedController(ApplicationUserManager userManager)
        {
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public MyProfileFeedController(IUserRepository userProfileRepo, IPostRepository postrepository)
        {
            _userProfileRepo = userProfileRepo;
            _postrepository = postrepository;
        }

        public ActionResult Index(string id)
        {
            var model = new List<Post>();
            if (string.IsNullOrEmpty(id)) id = User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            if (user != null)
            {
                model = _postrepository.GetLatestPostByUser(user.Id).ToList();
                ViewData["username"] = user.UserName;
                var userprofile = _userProfileRepo;
                var _user = userprofile.GetByUserId(user.Id);
                ViewData["userimage"] = string.IsNullOrEmpty(_user?.ImageUrl) ? @"\images\DefaultPhoto.png" : _user?.ImageUrl;
                ViewData["firstname"] = string.IsNullOrEmpty(_user?.FirstName) ? "FirstName" : _user?.FirstName;
            }
            
            
            
            return View(model);
        }

        public ActionResult SaveUserInfo(UserProfile profile)
        {
            profile.UserId = User.Identity.GetUserId();
            _userProfileRepo.SaveProfile(profile);

            return RedirectToAction("Index");
        }

    }
}