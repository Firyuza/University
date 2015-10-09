namespace Shared.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Schedule
    {
        public long id { get; set; }

        [Display(Name = "Group")]
        public virtual Group Group { get; set; }

        [Display(Name = "Teacher")]
        public virtual Course Course { get; set; }

        [Display(Name = "Day")]
        public string day { get; set; }
    }
}
