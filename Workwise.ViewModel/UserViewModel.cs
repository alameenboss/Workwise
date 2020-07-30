﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Workwise.ViewModel
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserName1 { get; set; }
        [DataType(DataType.Password)]
        public string Password1 { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public string Error { get; set; }
        public string LoginError { get; set; }
        public string FormType { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Bio { get; set; }
        public string Age { get; set; }
        public string FriendRequestStatus { get; set; }
        public string FriendRequestorId { get; set; }
        public string FriendEndUserId { get; set; }
        public bool IsRequestReceived { get; set; }
        public int FriendMappingId { get; set; }
        public bool IsOnline { get; set; }
        public string UnReadMessages { get; set; }
        public string FirstName { get; set; }
    }
}