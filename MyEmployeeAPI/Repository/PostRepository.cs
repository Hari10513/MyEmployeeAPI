using Microsoft.EntityFrameworkCore;
using MyEmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEmployeeAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        MyEmpDBContext db;
        public PostRepository(MyEmpDBContext _db)
        {
            db = _db;
        }
        public async Task<List<Employee>> Get()
        {
            if (db != null)
            {
                return await db.Employee.ToListAsync();
            }

            return null;
        }
        public async Task<Employee> GetId(int EmpId)
        {
            try
            {
                if (db != null)
                {
                    var a = await db.Employee.FromSqlRaw("Execute spGetEmployeeById {0}", EmpId).ToListAsync();
                    return a.FirstOrDefault();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Add(Employee Emp)
        {
            if (db != null)
            {
                await db.Employee.AddAsync(Emp);
                await db.SaveChangesAsync();

                return Emp.Id;
            }

            return 0;
        }
        public async Task<int> Delete(int? EmpId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var Emp = await db.Employee.FirstOrDefaultAsync(x => x.Id == EmpId);

                if (Emp != null)
                {
                    //Delete that post
                    db.Employee.Remove(Emp);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
        public async Task Update(Employee Emp)
        {
            if (db != null)
            {
                //Delete that post
                db.Employee.Update(Emp);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
