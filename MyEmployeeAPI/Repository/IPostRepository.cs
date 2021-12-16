using MyEmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEmployeeAPI.Repository
{
    public interface IPostRepository
    {
        Task<List<Employee>> Get();

        Task<Employee> GetId(int EmpId);

        Task<int> Add(Employee Emp);

        Task Update(Employee Emp);

        Task<int> Delete(int? EmpId);
    }
}
