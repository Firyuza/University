using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class RecordBook
    {
        public RecordBook()
        {
            this.Students = new HashSet<Student>();
        }

        public long id { get; set; }
        public string number { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}