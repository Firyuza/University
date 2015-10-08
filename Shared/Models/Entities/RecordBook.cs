namespace Shared.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
