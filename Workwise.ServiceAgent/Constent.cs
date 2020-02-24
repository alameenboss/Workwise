namespace Workwise.ServiceAgent
{
    public static class Constent
    {
        public static class Company
        {
           public const string GetCompanies = "api/Company/GetAllCompanies";
        }
        public static class Message
        {
            public const string SaveChatMessage = "api/Message/SaveChatMessage";
            public const string GetChatMessagesByUserId = "api/Message/SaveChatMessage?currentUserId={0}&toUserId={1}&lastMessageId={2}";
            public const string UpdateMessageStatusByUserId = "api/Message/UpdateMessageStatusByUserId";
            public const string UpdateMessageStatusByMessageId = "api/Message/UpdateMessageStatusByMessageId";
        }

        public static class Post
        {
            public const string GetLatestPostByUser = "api/Post/GetLatestPostByUser?UserId={0}"; 
        }
    }
}
