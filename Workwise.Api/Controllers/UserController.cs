using System;
using System.Collections.Generic;
using System.Web.Http;
using Workwise.Model;
using Workwise.Service.Interface;

namespace Workwise.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService = null;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        public UserProfile GetByUserId(string userId)
        {
            return _userService.GetByUserId(userId);
        }
        
    }
}
