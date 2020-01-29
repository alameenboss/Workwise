using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Workwise.Data.Interface;
using Workwise.Models;

namespace Workwise.Data
{
    public class UserProfileRepository 
    {

        public UserProfile GetByUserId(string UserId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.UserProfiles.FirstOrDefault(x => x.UserId == UserId);

            }
        }

        public void SaveUserImage(string userid,string imgPath)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = db.UserProfiles.FirstOrDefault(x => x.UserId == userid); ;
                if (model?.Id > 0)
                {
                    model.ImageUrl = imgPath;
                }
                else
                {
                    db.UserProfiles.Add(new UserProfile()
                    {
                        UserId = userid,
                        ImageUrl = imgPath
                    });
                }
                db.SaveChanges();
            }
        }


        public void SaveProfile(UserProfile profile)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Entry(profile).State = profile.Id == 0 ? EntityState.Added : EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}