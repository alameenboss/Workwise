﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;
using Workwise.Data.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfilesController : BaseController
    {
        private readonly IUserService _userService;
        public ProfilesController()
        {
        }
        public ProfilesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ProfilesController(IUserService userProfileRepo)
        {
            _userService = userProfileRepo;
        }
        public ActionResult Index()
        {
            var model = _userService.GetAllUsers(100, User.Identity.GetUserId());
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? User.Identity.GetUserId() : id;
            var model = _userService.FollowingList(userid, User.Identity.GetUserId());
            var userModel = _userService.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text = userModel.FirstName + " is following " + model.Count + " person.";
           
            return View("Index",model);
        }
        public ActionResult Followers(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? User.Identity.GetUserId() : id;
            var model = _userService.FollowersList(userid, User.Identity.GetUserId());
            var userModel = _userService.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text =  userModel.FirstName + " has " + model.Count + " followers.";
            return View("Index", model);
        }
        public ActionResult Randomuser(int id)
        {
            var model =  RandomUserGenerator.GetManyDummyUser(1, id);
            return View(model);
        }

        
    }
}