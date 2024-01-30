using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.DataStores
{
    public class DepartmentDataStore
    {
        private readonly CompanyEntities database;

        public DepartmentDataStore()
        {
            database = new CompanyEntities();
        }

        
        public List<dept> GetDepartment()
        {
            var departments = (database.dept.ToList());
            return departments;
        }
        public List<dept> GetDepartment(string name)
        {
            var departments = (database.dept.
                Where(x=>x.dname
                .Contains(name))
                .ToList());
            return departments;
        }
        public void AddOrUpdateDepartments(dept department)
        {
            database.dept.AddOrUpdate(department);
            database.SaveChanges();
        }
        
      
        public void DeleteDepartment(dept department)
        {
            
            if (department == null)
            {
                return;
            }
            database.dept.Remove(department);
            database.SaveChanges();
        }
        
    }
}
