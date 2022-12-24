using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class Image
{
    public int Id { get; set; }

    public string? ImagePath { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<MapPostImage> MapPostImages { get; } = new List<MapPostImage>();
}
