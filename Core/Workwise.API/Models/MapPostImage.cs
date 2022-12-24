using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class MapPostImage
{
    public int Id { get; set; }

    public int? PostId { get; set; }

    public int? ImageId { get; set; }

    public virtual Image? Image { get; set; }

    public virtual Post? Post { get; set; }
}
