using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class Schedule
    {
        public long id { get; set; }
        public virtual Group Group { get; set; }
        public virtual Teacher Teacher { get; set; }
        public string day { get; set; }
    }
}