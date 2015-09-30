using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class Teacher
    {
        public Teacher()
        {
            this.AcademicProgresses = new HashSet<AcademicProgress>();
            this.Schedules = new HashSet<Schedule>();
        }

        public long id { get; set; }

        public virtual Course Course { get; set; }
        public virtual Department Department { get; set; }
        public virtual Person Person { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<AcademicProgress> AcademicProgresses { get; set; }
        
    }
}