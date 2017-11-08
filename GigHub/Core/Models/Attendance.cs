using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }

        [Key]
        public int GigId { get; set; }

        [Key]
        public string AttendeeId { get; set; }
    }
}