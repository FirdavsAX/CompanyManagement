using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Models
{
    public class Salgrade
    {
        [Key]
        public int grade { get; set; }
        public decimal? losal { get; set; }
        public decimal? hisal { get; set; }
    }
}
