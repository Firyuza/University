using System.Collections.Generic;

namespace WebUniversity.Models
{
    public class ExaminationDatasheet
    {
        public Group Group { get; set; }

        public Teacher Teacher { get; set; }

        public double AvarageScore { get; set; }

        public IEnumerable<AcademicProgress> AcademicProgresses { get; set; } 
    }
}