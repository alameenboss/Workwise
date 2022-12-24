namespace Workwise.API.Model.RequestModel
{
    public class UpdateMessageStatusRequest
    {
        public string? CurrentUserId { get; set; }
        public string? FromUserId { get; set; }
    }
}
