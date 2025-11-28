using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;

public class EmployeeCreatePageVm
{
    // Employee Bilgileri
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TCKN { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public DateTime StartDate { get; set; }
    public decimal Salary { get; set; }
    public JobType JobType { get; set; }

    // Admin'in vereceği şifre
    public string UserName { get; set; }
    public string Password { get; set; }

    // Dropdownlar
    public int DepartmanId { get; set; }
    public int PositionId { get; set; }
    public int BranchId { get; set; }

    public List<Departmant> Departmants { get; set; }
    public List<Position> Positions { get; set; }
    public List<Branch> Branches { get; set; }
}
