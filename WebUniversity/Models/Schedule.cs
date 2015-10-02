using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class Schedule
    {
        public long id { get; set; }

        [Display(Name="Group")]
        public virtual Group Group { get; set; }

        [Display(Name = "Teacher")]
        public virtual Teacher Teacher { get; set; }

        [Display(Name = "Day")]
        public string day { get; set; }
    }
}