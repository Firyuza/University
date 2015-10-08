namespace Shared.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public Course()
        {
            this.Teachers = new HashSet<Teacher>();
        }

        [Required]
        public long id { get; set; }

        [Display(Name = "Course")]
        public string name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
