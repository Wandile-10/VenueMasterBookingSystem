using System.ComponentModel.DataAnnotations;

namespace VenueMasterBookingSystem.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        public string VenueName { get; set; }

        [Required]
        public string Location { get; set; }

        public int Capacity { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
} 
