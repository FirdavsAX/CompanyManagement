using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Models
{
    public class Department
    {
        [Key]
        public int deptno { get; set; }
        public string dname { get; set; }
        public string location { get; set; }
    }
}
