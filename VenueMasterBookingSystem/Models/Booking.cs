using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenueMasterBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int EventId { get; set; }

        public int VenueId { get; set; }

        public DateTime BookingDate { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }
    }
} 
