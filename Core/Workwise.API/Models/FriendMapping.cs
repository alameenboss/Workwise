using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class FriendMapping
{
    public int FriendMappingId { get; set; }

    public string? UserId { get; set; }

    public string? EndUserId { get; set; }

    public string? RequestStatus { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsActive { get; set; }
}
