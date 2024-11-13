using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class EmployeeDAO
    {
        private static EmployeeDAO employeeDAO;

        public static EmployeeDAO getInstance()
        {
            if (employeeDAO == null)
            {
                employeeDAO = new EmployeeDAO();
            }
            return employeeDAO;
        }

        public List<Employee> GetEmployees()
        {
            var listEmployee = new List<Employee>();
            try
            {
                using var context = new FmartDbContext();
                listEmployee = context.Employees.Include(c => c.EmployeeId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listEmployee;
        }

        public void SaveEmployee(Employee c)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Employees.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateEmployee(Employee Employee)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Entry<Employee>(Employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteEmployee(Employee Employee)
        {
            try
            {
                using var context = new FmartDbContext();
                var c = context.Employees.SingleOrDefault(c => c.EmployeeId == Employee.EmployeeId);
                if (c != null)
                {
                    context.Employees.Remove(c);              
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Employee? GetEmployeeById(int id)
        {
            Employee Employee;

            try
            {
                using var context = new FmartDbContext();
                Employee = context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the employee: " + ex.Message, ex);
            }
            return Employee;
        }

        
    }
}

