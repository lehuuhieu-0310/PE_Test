using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PE_Test.Models
{
    public class Professor
    {
        [Key]
        public int professorId { get; set; }
        [Required]
        public string professorName { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public int salary { get; set; }
        [Required]
        public string email { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthdate { get; set; }
 
        public string photo { get; set; }
        [Required]
        public bool married { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public int? departmentId { get; set; }
        public Department Department { get; set; }
    }
}
