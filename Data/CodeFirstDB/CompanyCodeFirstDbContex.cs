using CompanyManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace CompanyManagement.Data.CodeFirstDB
{
    public class CompanyCodeFirstDbContex : DbContext
    {
        // Your context has been configured to use a 'CompanyCodeFirstDbContex' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CompanyManagement.Data.CodeFirstDB.CompanyCodeFirstDbContex' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CompanyCodeFirstDbContex' 
        // connection string in the application configuration file.
        public CompanyCodeFirstDbContex()
            : base("Data Source=DESKTOP-I4DJQII;Initial Catalog=Company1;Integrated Security=True;Pooling=False")
        {
        }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Department> Department{ get; set; }
        public virtual DbSet<Salgrade> Salgrade{ get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}