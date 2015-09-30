using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class Student
    {
        public long id { get; set; }

        public virtual Group Group { get; set; }
        public virtual Person Person { get; set; }
        public virtual RecordBook RecordBook { get; set; }
        public virtual ICollection<AcademicProgress> AcademicProgresses { get; set; }   
    }
}