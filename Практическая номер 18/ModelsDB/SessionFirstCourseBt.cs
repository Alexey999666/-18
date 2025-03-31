using System;
using System.Collections.Generic;

namespace Практическая_номер_18.ModelsDB;

public partial class SessionFirstCourseBt
{
    public int IndexGroup { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public string Gender { get; set; } = null!;

    public string FamilyStatus { get; set; } = null!;

    public int MathGrade { get; set; }

    public int ComputerScienceGrade { get; set; }

    public int HistoryGrade { get; set; }

    public int PhysicalEducationGrade { get; set; }

    public int LiteratureGrade { get; set; }
}
