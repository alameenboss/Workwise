using System;
using System.Collections.Generic;

namespace Workwise.API.Models;

public partial class ErrorLog
{
    public int ErrorLogId { get; set; }

    public int? ErrorNumber { get; set; }

    public int? ErrorSeverity { get; set; }

    public int? ErrorState { get; set; }

    public string? ErrorProcedure { get; set; }

    public int? ErrorLine { get; set; }

    public string? ErrorMessage { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
}
