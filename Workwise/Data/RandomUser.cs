using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Workwise.Data.Models;

namespace Workwise.Data
{
    /// <summary>
    /// Using https://randomuser.me
    /// </summary>

    public class RandomUserGenerator
    {
        public class Name
        {
            public string title { get; set; }
            public string first { get; set; }
            public string last { get; set; }
        }

        public class Street
        {
            public int number { get; set; }
            public string name { get; set; }
        }

        public class Coordinates
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }

        public class Timezone
        {
            public string offset { get; set; }
            public string description { get; set; }
        }

        public class Location
        {
            public Street street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public object postcode { get; set; }
            public Coordinates coordinates { get; set; }
            public Timezone timezone { get; set; }
        }

        public class Login
        {
            public string uuid { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string salt { get; set; }
            public string md5 { get; set; }
            public string sha1 { get; set; }
            public string sha256 { get; set; }
        }

        public class Dob
        {
            public DateTime date { get; set; }
            public int age { get; set; }
        }

        public class Registered
        {
            public DateTime date { get; set; }
            public int age { get; set; }
        }

        public class Id
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public class Picture
        {
            public string large { get; set; }
            public string medium { get; set; }
            public string thumbnail { get; set; }
        }

        public class Result
        {
            public string gender { get; set; }
            public Name name { get; set; }
            public Location location { get; set; }
            public string email { get; set; }
            public Login login { get; set; }
            public Dob dob { get; set; }
            public Registered registered { get; set; }
            public string phone { get; set; }
            public string cell { get; set; }
            public Id id { get; set; }
            public Picture picture { get; set; }
            public string nat { get; set; }
        }

        public class Info
        {
            public string seed { get; set; }
            public int results { get; set; }
            public int page { get; set; }
            public string version { get; set; }
        }

        public class UserResult
        {
            public List<Result> results { get; set; }
            public Info info { get; set; }
        }

        public static Result GetSingleDummyUser()
        {
            var user = new Result();

            string url = "http://api.randomuser.me/";

            var data = FetchJson(url);

            if (data != null)
                user = data.results.FirstOrDefault();

            return user;
        }

        public static List<UserProfile> GetManyDummyUser(int take)
        {
            var users = new List<UserProfile>();
            string url = "http://api.randomuser.me/?results=" + take;

            var data = FetchJson(url);

            if (data.results != null)
            {
                foreach (var item in data.results)
                {
                    users.Add(new UserProfile()
                    {
                        FirstName = item.name.first + " " + item.name.last,
                        DOB = item.dob.date,
                        Gender = item.gender,
                        ImageUrl = item.picture.thumbnail,
                        Designation = item.location.city,
                        UserId = Guid.NewGuid().ToString()
                    });
                }
            }

            return users;

        }

        public static List<Result> GetManyUser(int take)
        {
            var users = new List<Result>();
            string url = "http://api.randomuser.me/?results=" + take;

            var data = FetchJson(url);

            return data.results;

        }

        private static UserResult FetchJson(string url)
        {
            var data = new UserResult();

            try
            {
                using (WebClient wc = new WebClient())
                {
                    string json = wc.DownloadString(url);
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResult>(json);
                }

            }
            catch (Exception)
            {
                // throw;
            }

            return data;
        }
    }
}
