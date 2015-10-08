using System;
using System.Collections.Generic;
using System.Linq;
namespace Shared.Models.Entities
{
    public class ExaminationDatasheet
    {
        public Group Group { get; set; }

        public Teacher Teacher { get; set; }

        public double AvarageScore { get; set; }

        public IEnumerable<AcademicProgress> AcademicProgresses { get; set; }
    }
}
