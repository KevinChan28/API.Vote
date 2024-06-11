using System;
using System.Collections.Generic;

namespace API.UsersVote.Models;

public partial class PoliticParty
{
    public int Id { get; set; }

    public string? Applicant { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }
}
