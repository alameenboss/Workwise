namespace Workwise.ServiceAgent
{
    public static class Constent
    {
        public static class Company
        {
           public const string GetCompanies = "Company/GetAllCompanies";
        }
        public static class Message
        {
            public const string SaveChatMessage = "Message/SaveChatMessage";
            public const string GetChatMessagesByUserId = "Message/GetChatMessagesByUserId?currentUserId={0}&toUserId={1}&lastMessageId={2}";
            public const string UpdateMessageStatusByUserId = "Message/UpdateMessageStatusByUserId";
            public const string UpdateMessageStatusByMessageId = "Message/UpdateMessageStatusByMessageId";
        }

        public static class Post
        {
            public const string GetLatestPostByUser = "Post/GetLatestPostByUser?UserId={0}";
            public const string SavePost = "Post/SavePost";
        }

        public static class User
        {
            public const string GetUserById = "User/GetByUserId?UserId={0}";
        }
    }
}
