using System;
using System.Collections.Generic;

namespace NumeroLetraAPI.Models;

public partial class User
{
    public byte IdUser { get; set; }

    public string? StrName { get; set; }

    public string? StrLastName { get; set; }

    public string? FxCompleteName { get; }

    public string? StrUser { get; set; }

    public string? StrPassword { get; set; }

    public bool? BitActive { get; set; }
}
