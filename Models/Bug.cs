using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models
{
    public class Bug
    {
        //[Key]
        public int Id { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; } // Open, In Progress, Closed, etc. Add enum if needed

        // Foreign key to link this bug to the user who reported it (QA)
        public string ReporterId { get; set; }
        public User Reporter { get; set; }

        // Foreign key to link this bug to the user who resolved it (RD)
        public string ResolverId { get; set; }
        public User Resolver { get; set; }
    }
}
