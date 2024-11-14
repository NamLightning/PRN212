using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepositoty
    {
        void DeleteEmployee(Employee Employee);

        Employee GetEmployeeById(int id);

        List<Employee> GetEmployees();

        void SaveEmployee(Employee Employee);

        void UpdateEmployee(Employee Employee);
    }
}
