using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Workwise.Model;
using Workwise.ResultModel;
using Workwise.Service.Interface;

namespace Workwise.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService = null;
        //private readonly IRandomUserService _randomUserService = null;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IHttpActionResult SaveUserOnlineStatus(OnlineUser objentity)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _userService.SaveUserOnlineStatus(objentity);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetUserConnectionId(string userId)
        {
            try
            {
                var myConnectionIdList = _userService.GetUserConnectionId(userId);
                if (myConnectionIdList == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(myConnectionIdList);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetUsersConnectionId(string userIds)
        {
            try
            {
                var myConnectionIdList = _userService.GetUserConnectionId(userIds.Split(','));
                if (myConnectionIdList == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(myConnectionIdList);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<UserSearchResultModel>))]
        public IHttpActionResult GetAllUsers(int count, string userId)
        {
            try
            {
                var userList = _userService.GetAllUsers(count, userId);
                if (userList == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(userList);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }
        

        [HttpGet]
        [ResponseType(typeof(List<UserProfile>))]
        public IHttpActionResult MyFriendsList(string userId)
        {
            try
            {
                var myFriendsList = _userService.MyFriendsList(userId);
                if (myFriendsList == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(myFriendsList);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<UserSearchResultModel>))]
        public IHttpActionResult FollowersList(string userId, string currentUserId)
        {
            try
            {
                var followers = _userService.FollowersList(userId, currentUserId);
                if (followers == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(followers);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [ResponseType(typeof(List<UserSearchResultModel>))]
        public IHttpActionResult FollowingList(string userId, string currentUserId)
        {
            try
            {
                var following = _userService.FollowingList(userId, currentUserId);
                if (following == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(following);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }
        
        [HttpGet]
        [ResponseType(typeof(List<OnlineUserDetailViewModel>))]
        public IHttpActionResult GetOnlineFriends(string userId)
        {
            try
            {
                var onlineFriends = _userService.GetOnlineFriends(userId);
                if (onlineFriends == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(onlineFriends);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }


        [HttpGet]
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult GetUserById(string userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                if (user == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(user);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [ResponseType(typeof(string[]))]
        public IHttpActionResult GetFriendUserIds(string userId)
        {
            try
            {
                var friendUserIds = _userService.GetFriendUserIds(userId);
                if (friendUserIds == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(friendUserIds);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [ResponseType(typeof(List<FriendRequestResultModel>))]
        public IHttpActionResult GetSentFriendRequests(string userId)
        {
            try
            {
                var sentRequests = _userService.GetSentFriendRequests(userId);
                if (sentRequests == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(sentRequests);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }


        [HttpGet]
        [ResponseType(typeof(List<FriendRequestResultModel>))]
        public IHttpActionResult GetReceivedFriendRequests(string userId)
        {
            try
            {
                var receivedRequest = _userService.GetReceivedFriendRequests(userId);
                if (receivedRequest == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(receivedRequest);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }



        [HttpGet]
        [ResponseType(typeof(List<FriendRequestResultModel>))]
        public IHttpActionResult GetAllSentFriendRequests()
        {
            try
            {
                var sentRequests = _userService.GetAllSentFriendRequests();
                if (sentRequests == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(sentRequests);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [ResponseType(typeof(List<UserSearchResultModel>))]
        public IHttpActionResult SearchUsers(UserProfile model)
        {
            try
            {
                var result = _userService.SearchUsers(model.FirstName, model.UserId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public IHttpActionResult SendFriendRequest(FriendRequestResultModel model )
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _userService.SendFriendRequest(model.EndUserId, model.UserId);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            return Ok();
        }
       
        
        [HttpPost]
        [ResponseType(typeof(UserNotification))]
        public IHttpActionResult SaveUserNotification(UserNotification model)
        {
            var result = new UserNotification();
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                result = _userService.SaveUserNotification(model.NotificationType, model.FromUserId, model.ToUserId);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(FriendMapping))]
        public IHttpActionResult GetFriendRequestStatus(string userId)
        {
            try
            {
                var result = _userService.GetFriendRequestStatus(userId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }

        }



        [HttpGet]
        [ResponseType(typeof(int))]
        public IHttpActionResult ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            try
            {
                var result = _userService.ResponseToFriendRequest(requestorId, requestResponse, endUserId);
                if (result == 0)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            } 
        }
        [HttpGet]
        [ResponseType(typeof(List<UserNotificationListResultModel>))]
        public IHttpActionResult GetUserNotifications(string toUserId)
        {
            try
            {
                var result = _userService.GetUserNotifications(toUserId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetUserNotificationCounts(string toUserId)
        {
            try
            {
                var result = _userService.GetUserNotificationCounts(toUserId);
                if (result == 0)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult ChangeNotificationStatus(int[] notificationIds)
        {
            
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _userService.ChangeNotificationStatus(notificationIds);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(FriendMapping))] 
        public IHttpActionResult RemoveFriendMapping(int friendMappingId)
        {
            FriendMapping result = null;
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                result = _userService.RemoveFriendMapping(friendMappingId);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<OnlineUserDetailViewModel>))]
        public IHttpActionResult GetRecentChats(string currentUserId)
        {
            try
            {
                var result = _userService.GetRecentChats(currentUserId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [ResponseType(typeof(OnlineUserDetailViewModel))]
        public IHttpActionResult GetUserOnlineStatus(string userId)
        {
            try
            {
                var result = _userService.GetUserOnlineStatus(userId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult SaveProfileImage(UserProfile model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _userService.SaveProfileImage(model.UserId, model.ImageUrl);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult SaveUserImage(UserImage model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _userService.SaveUserImage(model.UserId, model.ImagePath, false);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(List<OnlineUserDetailViewModel>))]
        public IHttpActionResult GetFriends(string userId)
        {
            try
            {
                var result = _userService.GetFriends(userId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
        }

        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult GetByUserId(string UserId)
        {
            try
            {
                var userprofile = _userService.GetByUserId(UserId);
                if (userprofile == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(userprofile);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            
        }

        [HttpPost]
        public IHttpActionResult SaveProfile(UserProfile profile)
        {
           
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _userService.SaveProfile(profile);
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateUserProfile(UserProfile model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                await _userService.CreateUserProfileAsync(model.UserId, model.FirstName); 
            }
            catch (HttpResponseException ex)
            {

                throw ex;
            }
            return Ok();
            
        }
        public List<UserSearchResultModel> SerachUser(string userName)
        {
            return _userService.SerachUser(userName);
        }

        public List<UserProfile> GetManyDummyUser(int pageNo, int pageSize)
        {
            //return _randomUserService.GetManyDummyUser(pageNo, pageSize);
            return new List<UserProfile>();
        }
    }
}
