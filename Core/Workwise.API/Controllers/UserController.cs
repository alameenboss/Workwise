using Microsoft.AspNetCore.Mvc;
using Workwise.API.Model.ResultModel;
using Workwise.API.Models;
using Workwise.API.Service.Interface;

namespace Workwise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        //private readonly IRandomUserService _randomUserService = null;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("SaveUserOnlineStatus")]
        public IActionResult SaveUserOnlineStatus(OnlineUser objentity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            userService.SaveUserOnlineStatus(objentity);

            return Ok();
        }

        [HttpGet("GetUserConnectionId")]
        public IActionResult GetUserConnectionId(string userId)
        {
            List<string> myConnectionIdList = userService.GetUserConnectionId(userId);

            return myConnectionIdList == null ? NotFound() : Ok(myConnectionIdList);
        }

        [HttpGet("GetUsersConnectionId")]
        public IActionResult GetUsersConnectionIds(string userIds)
        {
            List<string> myConnectionIdList = userService.GetUsersConnectionId(userIds.Split(','));

            return myConnectionIdList == null ? NotFound() : Ok(myConnectionIdList);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers(int count, string userId)
        {
            List<UserSearchResultModel> userList = userService.GetAllUsers(count, userId);

            return userList == null ? NotFound() : Ok(userList);
        }


        [HttpGet("MyFriendsList")]
        public IActionResult MyFriendsList(string userId)
        {
            List<UserProfile> myFriendsList = userService.MyFriendsList(userId);

            return myFriendsList == null ? NotFound() : Ok(myFriendsList);
        }

        [HttpGet("FollowersList")]
        public IActionResult FollowersList(string userId, string currentUserId)
        {
            List<UserSearchResultModel> followers = userService.FollowersList(userId, currentUserId);

            return followers == null ? NotFound() : Ok(followers);
        }

        [HttpGet("FollowingList")]
        public IActionResult FollowingList(string userId, string currentUserId)
        {
            List<UserSearchResultModel> following = userService.FollowingList(userId, currentUserId);

            return following == null ? NotFound() : Ok(following);
        }

        [HttpGet("GetOnlineFriends")]
        public IActionResult GetOnlineFriends(string userId)
        {
            List<OnlineUserDetailViewModel> onlineFriends = userService.GetOnlineFriends(userId);

            return onlineFriends == null ? NotFound() : Ok(onlineFriends);
        }


        [HttpGet("GetUserById")]
        public IActionResult GetUserById(string userId)
        {
            UserProfile user = userService.GetUserById(userId);

            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("GetFriendUserIds")]
        public IActionResult GetFriendUserIds(string userId)
        {
            string[] friendUserIds = userService.GetFriendUserIds(userId);

            return friendUserIds == null ? NotFound() : Ok(friendUserIds);
        }

        [HttpGet("GetSentFriendRequests")]
        public IActionResult GetSentFriendRequests(string userId)
        {
            List<FriendRequestResultModel> sentRequests = userService.GetSentFriendRequests(userId);

            return sentRequests == null ? NotFound() : Ok(sentRequests);
        }


        [HttpGet("GetReceivedFriendRequests")]
        public IActionResult GetReceivedFriendRequests(string userId)
        {
            List<FriendRequestResultModel> receivedRequest = userService.GetReceivedFriendRequests(userId);

            return receivedRequest == null ? NotFound() : Ok(receivedRequest);
        }



        [HttpGet("GetAllSentFriendRequests")]
        public IActionResult GetAllSentFriendRequests()
        {
            List<FriendRequestResultModel> sentRequests = userService.GetAllSentFriendRequests();

            return sentRequests == null ? NotFound() : Ok(sentRequests);
        }

        [HttpGet("SearchUsers")]
        public IActionResult SearchUsers(UserProfile model)
        {
            List<UserSearchResultModel> result = userService.SearchUsers(model.FirstName, model.UserId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("SendFriendRequest")]
        public IActionResult SendFriendRequest(FriendRequestResultModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            userService.SendFriendRequest(model.EndUserId, model.UserId);

            return Ok();
        }


        [HttpPost("SaveUserNotification")]
        public IActionResult SaveUserNotification(UserNotification model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            UserNotification result = userService.SaveUserNotification(model.NotificationType, model.FromUserId, model.ToUserId);

            return Ok(result);
        }

        [HttpGet("GetFriendRequestStatus")]
        public IActionResult GetFriendRequestStatus(string userId)
        {
            FriendMapping result = userService.GetFriendRequestStatus(userId);

            return result == null ? NotFound() : Ok(result);
        }



        [HttpGet("ResponseToFriendRequest")]
        public IActionResult ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            int result = userService.ResponseToFriendRequest(requestorId, requestResponse, endUserId);

            return result == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetUserNotifications")]
        public IActionResult GetUserNotifications(string toUserId)
        {
            List<UserNotificationListResultModel> result = userService.GetUserNotifications(toUserId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("GetUserNotificationCounts")]
        public IActionResult GetUserNotificationCounts(string toUserId)
        {
            int result = userService.GetUserNotificationCounts(toUserId);

            return result == 0 ? NotFound() : Ok(result);
        }

        [HttpPost("ChangeNotificationStatus")]
        public IActionResult ChangeNotificationStatus(int[] notificationIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            userService.ChangeNotificationStatus(notificationIds);

            return Ok();
        }

        [HttpPost("RemoveFriendMapping")]
        public IActionResult RemoveFriendMapping(int friendMappingId)
        {
            FriendMapping result = userService.RemoveFriendMapping(friendMappingId);

            return Ok(result);
        }

        [HttpGet("GetRecentChats")]
        public IActionResult GetRecentChats(string currentUserId)
        {
            List<OnlineUserDetailViewModel> result = userService.GetRecentChats(currentUserId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("GetUserOnlineStatus")]
        public IActionResult GetUserOnlineStatus(string userId)
        {
            OnlineUserDetailViewModel result = userService.GetUserOnlineStatus(userId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("SaveProfileImage")]
        public IActionResult SaveProfileImage(UserProfile model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            userService.SaveProfileImage(model.UserId, model.ImageUrl);

            return Ok();
        }

        [HttpPost("SaveUserImage")]
        public IActionResult SaveUserImage(UserImage model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            userService.SaveUserImage(model.UserId, model.ImagePath, false);

            return Ok();
        }

        [HttpGet("GetFriends")]
        public IActionResult GetFriends(string userId)
        {
            List<OnlineUserDetailViewModel> result = userService.GetFriends(userId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId(string UserId)
        {
            UserProfile userprofile = userService.GetByUserId(UserId);

            return userprofile == null ? NotFound() : Ok(userprofile);
        }

        [HttpPost("SaveProfile")]
        public IActionResult SaveProfile(UserProfile profile)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            userService.SaveProfile(profile);

            return Ok();
        }

        [HttpPost("CreateUserProfile")]
        public async Task<IActionResult> CreateUserProfile(UserProfile model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            await userService.CreateUserProfileAsync(model.UserId, model.FirstName);

            return Ok();

        }

        [HttpGet("SerachUser")]
        public List<UserSearchResultModel> SerachUser(string userName)
        {
            return userService.SerachUser(userName);
        }

        [HttpGet("GetManyDummyUser")]
        public List<UserProfile> GetManyDummyUser(int pageNo, int pageSize)
        {
            //return _randomUserService.GetManyDummyUser(pageNo, pageSize);
            return new List<UserProfile>();
        }
    }
}

