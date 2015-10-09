namespace Shared.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        [Required]
        public long id { get; set; }

        [Display(Name = "Course")]
        public string name { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
