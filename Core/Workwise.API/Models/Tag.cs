using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string? Tag1 { get; set; }

    public int? PostId { get; set; }

    public virtual Post? Post { get; set; }
}
