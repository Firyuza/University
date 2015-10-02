using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebUniversity.Models;

namespace WebUniversity.ViewModel
{
    public class ExaminationDatasheetsViewModel
    {
        [Required]
        [Display(Name="Group")]
        public long GroupId { get; set; }

        [Required]
        [Display(Name = "Course")]
        public long CourseId { get; set; }

        [Required]
        [Display(Name = "Teacher")]
        public long TeacherId { get; set; } 
    }
}