using BusinessObjects.Model;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepositoty : IEmployeeRepositoty
    {
        public void DeleteEmployee(Employee Employee) => EmployeeDAO.getInstance().DeleteEmployee(Employee);

        public Employee GetEmployeeById(int id) => EmployeeDAO.getInstance().GetEmployeeById(id);

        public List<Employee> GetEmployees()=> EmployeeDAO.getInstance().GetEmployees();

        public void SaveEmployee(Employee Employee)=>EmployeeDAO.getInstance().SaveEmployee(Employee);
        public void UpdateEmployee(Employee Employee)=>EmployeeDAO.getInstance().UpdateEmployee(Employee);
    }
}
