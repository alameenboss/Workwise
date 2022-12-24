using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class UserProfile
{
    public string UserId { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Designation { get; set; }

    public string? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? Bio { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }

    public int? PostId { get; set; }

    public int? PostId1 { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Post? Post { get; set; }

    public virtual Post? PostId1Navigation { get; set; }
}
