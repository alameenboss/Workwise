﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workwise.Data;
using Workwise.Data.Models;
using Workwise.ViewModel;

namespace Workwise.Helper
{

    public static class DefaultsHelper
    {
        public static string GetProfilePicture(string profilePicture, string gender)
        {
            string profilePicturePath = "";
            if (string.IsNullOrEmpty(profilePicture))
            {
                if (gender == "Female")
                {
                    profilePicturePath = "/Content/Images/female-default-pic.jpg";
                }
                else
                {
                    profilePicturePath = "/Content/Images/male-default-pic.jpg";
                }
            }
            else
            {
                profilePicturePath = profilePicture;
            }
            return profilePicturePath;
        }
        public static UserViewModel GetUserModel(string id, UserProfile objentity = null, string friendRequestStatus = "", bool isRequestReceived = false)
        {
            var user = new UserProfile();
            if (objentity != null)
            {
                user = objentity;
            }
            else
            {
                UserRepository _UserRepo = new UserRepository();
                user = _UserRepo.GetUserById(id);
            }
            UserViewModel objmodel = new UserViewModel();
            if (user != null)
            {
                objmodel.IsRequestReceived = isRequestReceived;
                objmodel.FriendRequestStatus = friendRequestStatus;
                objmodel.UserId = user.UserId;
                objmodel.Name = user.FirstName;
                objmodel.ProfilePicture = DefaultsHelper.GetProfilePicture(user.ImageUrl, user.Gender);
                objmodel.Gender = user.Gender;
                objmodel.DOB = user.DOB?.ToShortDateString();
                if (user.DOB != null)
                {
                    objmodel.Age = Convert.ToString(Math.Floor(DateTime.Now.Subtract(Convert.ToDateTime(user.DOB)).TotalDays / 365.0)) + " Years";
                }
                else
                {
                    objmodel.Age = "NaN";
                }
                objmodel.Bio = user.Bio;
            }
            return objmodel;
        }
        public static MessageViewModel GetMessageModel(ChatMessage objentity)
        {
            MessageViewModel objmodel = new MessageViewModel();
            objmodel.ChatMessageId = objentity.ChatMessageId;
            objmodel.FromUserId = objentity.FromUserId;
            objmodel.ToUserId = objentity.ToUserId;
            objmodel.Message = objentity.Message;
            objmodel.Status = objentity.Status;
            objmodel.CreatedOn = Convert.ToString(objentity.CreatedOn);
            objmodel.UpdatedOn = Convert.ToString(objentity.UpdatedOn);
            objmodel.ViewedOn = Convert.ToString(objentity.ViewedOn);
            objmodel.IsActive = objentity.IsActive;
            return objmodel;
        }
    }
}