using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT_3.Models
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }
    }
}
