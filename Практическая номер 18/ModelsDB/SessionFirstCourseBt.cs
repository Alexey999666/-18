using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;

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
public partial class User
{
    public int UserID { get; set; }
    public string UserSurname { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? UserPatronymic { get; set; } = null!;
    public string? UserLogin { get; set;} = null!;
    public string UserPassword { get; set;} = null!;
    public int UserRole { get; set; }
    public virtual Role UserRoleNavigation { get; set; } = null!;

}
public partial class Role
{
    public int RoleID { get; set; }
    public string RoleName { get; set; } = null!;
}