using System;
using System.Collections.Generic;

namespace BusinessObjects.Model;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Position { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
}
