using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class OnlineUser
{
    public int OnlineUserId { get; set; }

    public string? UserId { get; set; }

    public string? ConnectionId { get; set; }

    public bool IsOnline { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsActive { get; set; }
}
