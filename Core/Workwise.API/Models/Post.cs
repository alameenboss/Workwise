using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class Post
{
    public int Id { get; set; }

    public int Worktype { get; set; }

    public decimal Rate { get; set; }

    public string? Title { get; set; }

    public DateTime PostedOn { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? PostedById { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<MapPostImage> MapPostImages { get; } = new List<MapPostImage>();

    public virtual ICollection<Tag> Tags { get; } = new List<Tag>();

    public virtual ICollection<UserProfile> UserProfilePostId1Navigations { get; } = new List<UserProfile>();

    public virtual ICollection<UserProfile> UserProfilePosts { get; } = new List<UserProfile>();
}
