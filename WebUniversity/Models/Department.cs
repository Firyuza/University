using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class Department
    {
        public Department()
        {
            this.Teachers = new HashSet<Teacher>();
        }

        [Required]
        public long id { get; set; }

        [Display(Name = "Department")]
        public string name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}