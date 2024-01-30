namespace CompanyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        deptno = c.Int(nullable: false, identity: true),
                        dname = c.String(),
                        location = c.String(),
                    })
                .PrimaryKey(t => t.deptno);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        empno = c.Int(nullable: false, identity: true),
                        ename = c.String(),
                        job = c.String(),
                        mgr = c.Int(),
                        hiredate = c.DateTime(),
                        sal = c.Decimal(precision: 18, scale: 2),
                        comm = c.Decimal(precision: 18, scale: 2),
                        deptno = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.empno);
            
            CreateTable(
                "dbo.Salgrades",
                c => new
                    {
                        grade = c.Int(nullable: false, identity: true),
                        losal = c.Decimal(precision: 18, scale: 2),
                        hisal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.grade);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Salgrades");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
