using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class Department
    {
        public Department()
        {
            this.Teachers = new HashSet<Teacher>();
        }

        public long id { get; set; }
        public string name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}