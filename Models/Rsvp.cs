using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models
{
    public class Rsvp
    {
        [Key]
        public int RsvpId { get; set; }
// foreign key for User
        public int UserId { get; set; }
// foreign key for Wedding
        public int WeddingId { get; set; }

// many to many - a wedding can have many guests
        public User Planner { get; set; }
        public List<Rsvp> GuestList { get; set; }
    }
}