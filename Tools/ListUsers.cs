using System;
using System.Linq;
using Hospital.Data;

class ListUsers
{
    static void Main()
    {
        using var ctx = new HospitalDbContext();
        var users = ctx.Users.ToList();
        foreach (var u in users)
        {
            Console.WriteLine($"Id: {u.Id}\tLogin: {u.Login}\tRole: {u.Role}\tPatientId: {u.PatientId}\tPasswordHash: {u.PasswordHash}");
        }
    }
}