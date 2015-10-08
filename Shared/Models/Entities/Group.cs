namespace Shared.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Group
    {
        public Group()
        {
            this.Schedules = new HashSet<Schedule>();
            this.Students = new HashSet<Student>();
        }

        public long id { get; set; }

        [Display(Name = "Group")]
        public string name { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
