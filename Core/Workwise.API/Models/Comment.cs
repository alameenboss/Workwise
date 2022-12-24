using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? CommentedByUserId { get; set; }

    public int? PostId { get; set; }

    public virtual UserProfile? CommentedByUser { get; set; }

    public virtual Post? Post { get; set; }
}
