﻿using System.Web.Mvc;
using Workwise.Data.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfilesController : BaseController
    {
        private readonly IUserRepository _userProfileRepo;
       
        public ProfilesController(IUserRepository userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;
        }


        public ActionResult Index()
        {
            var model = _userProfileRepo.GetAllUsers();
            return View(model);
        }
    }
}