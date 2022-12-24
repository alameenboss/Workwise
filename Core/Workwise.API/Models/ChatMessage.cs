using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class ChatMessage
{
    public int ChatMessageId { get; set; }

    public string? FromUserId { get; set; }

    public string? ToUserId { get; set; }

    public string? Message { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public DateTime ViewedOn { get; set; }

    public bool IsActive { get; set; }
}
