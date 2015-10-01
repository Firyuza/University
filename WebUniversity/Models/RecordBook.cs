using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Record book")]
        public string number { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}