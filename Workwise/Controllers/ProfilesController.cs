﻿using System.Linq;
using System.Web.Mvc;
using Workwise.Data;
using Workwise.Data.Interface;
using Workwise.Data.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Workwise.Helper;
using Workwise.Data.Models;
using Workwise.Data.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfilesController : BaseController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationUserManager _userManager;
        private readonly IUserRepository _userProfileRepo;
        public ProfilesController()
        {
        }
        public ProfilesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ProfilesController(IUserRepository userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;
        }
        public ActionResult Index()
        {
            var model = _userProfileRepo.GetAllUsers(100, User.Identity.GetUserId());
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? User.Identity.GetUserId() : id;
            var model = _userProfileRepo.FollowingList(userid);
            var userModel = _userProfileRepo.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text = userModel.FirstName + " is following " + model.Count + " person.";
           
            return View("Index",model);
        }
        public ActionResult Followers(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? User.Identity.GetUserId() : id;
            var model = _userProfileRepo.FollowersList(userid);
            var userModel = _userProfileRepo.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text =  userModel.FirstName + " has " + model.Count + " followers.";
            return View("Index", model);
        }
        public ActionResult Randomuser(int id)
        {
            var model =  RandomUserGenerator.GetManyDummyUser(id);
            return View(model);
        }

        
    }
}