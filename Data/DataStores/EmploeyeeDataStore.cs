using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.DataStores
{
    public class EmployeeDataStore
    {
        private readonly CompanyEntities database;

        public EmployeeDataStore()
        {
            database = new CompanyEntities();
        }

        public void AddOrEdit(emp newEmployee)
        {
            database.emp.AddOrUpdate(newEmployee);
            database.SaveChanges();
        }
        public List<emp> GetEmployees()
        {
            var employees = (database.emp.ToList());
            return employees;
        }
        
        public List<emp> GetEmployees(int id)
        {
            return database.emp
                .Where(x => x.deptno == id)
                .ToList();
        }
        public List<emp> GetEmployees(string name)
        {
            return database.emp
                .Where(x => x.ename.Contains(name))
                .ToList();
        }
        public void DeleteEmployee(emp deleteEmployee)
        {
            if(deleteEmployee == null)
            {
                return;
            }
            database.emp.Remove(deleteEmployee);
            database.SaveChanges();
        }

    }
}
