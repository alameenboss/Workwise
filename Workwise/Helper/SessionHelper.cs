using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Web;
using Workwise.Data;
using Workwise.Model;
namespace Workwise.Helper
{

    /// <summary>
    /// Static Session facade class
    /// </summary>
    public static class SessionHelper
    {
        #region Private Constants

        private const string userName = "UserName";

        private const string userImage = "UserImageUrl";
        private const string userId = "UserId";
        #endregion



        #region Public Static Methods
        /// <summary>
        /// Clears Session
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// Abandons Session
        /// </summary>
        public static void Abandon()
        {
            ClearSession();
            HttpContext.Current.Session.Abandon();
        }

        #endregion


        #region Public Static Properties

        /// <summary>
        /// Gets/Sets Session for UserName
        /// </summary>

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[userName] == null)
                    return "";
                else
                    return HttpContext.Current.Session[userName].ToString();
            }
            set { HttpContext.Current.Session[userName] = value; }
        }
        
        public static string UserImage
        {
            get
            {
                if (HttpContext.Current.Session[userImage] == null)
                    return "";
                else
                    return HttpContext.Current.Session[userImage].ToString();
            }
            set { HttpContext.Current.Session[userImage] = value; }
        }

        public static string UserId
        {
            get
            {
                
                if (HttpContext.Current.Session[userId] == null)
                    return "";
                else
                    return HttpContext.Current.Session[userId].ToString();
            }
            set { HttpContext.Current.Session[userId] = value; }
        }

        public static T Get<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public static void Set<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;

        }

        public  static UserProfileViewModel GetUser(string userid)
        {

            if (SessionHelper.Get<UserProfileViewModel>(userid) == null)
            {
                var repo = new UserService();
                var model = repo.GetByUserId(userid);
                if (model == null)
                {
                    model = new UserProfileViewModel()
                    {
                        FirstName = userid,
                        ImageUrl = @"\images\DefaultPhoto.png"
                    };
                }
                SessionHelper.Set<UserProfileViewModel>(userid, model);
                SessionHelper.UserImage = model.ImageUrl;
                SessionHelper.UserName = model.FirstName;
                SessionHelper.UserId = userid;
            }

            return SessionHelper.Get<UserProfileViewModel>(userid);

        }
        #endregion
    }

}