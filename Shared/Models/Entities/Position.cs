﻿namespace Shared.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Position
    {
        public Position()
        {
            this.Teachers = new HashSet<Teacher>();
        }

        [Required]
        public long id { get; set; }

        [Display(Name = "Position")]
        public string name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
