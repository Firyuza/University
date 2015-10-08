namespace Shared.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
