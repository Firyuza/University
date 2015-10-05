﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUniversity.Models
{
    public class AcademicProgress
    {
        public long id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public Nullable<double> score { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<DateTime> date { get; set; }
    }
}