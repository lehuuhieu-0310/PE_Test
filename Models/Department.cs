using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PE_Test.Models
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }
        [Required]
        public string departmentName { get; set; }
        [Required]
        public int max_employee { get; set; }
        [Required]
        public string location { get; set; }
        public ICollection<Professor> Professors { get; set; }
    }
}
