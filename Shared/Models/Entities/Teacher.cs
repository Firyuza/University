namespace Shared.Models.Entities
{
    using System.Collections.Generic;

    public class Teacher
    {
        public Teacher()
        {
            this.AcademicProgresses = new HashSet<AcademicProgress>();
            this.Schedules = new HashSet<Schedule>();
            this.Courses = new HashSet<Course>();
        }

        public long id { get; set; }
        public virtual Department Department { get; set; }
        public virtual Person Person { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<AcademicProgress> AcademicProgresses { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
