namespace Shared.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        public Person()
        {
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
        }

        public long id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string firstname { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string lastname { get; set; }

        [Display(Name = "Middle name")]
        public string middlename { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
