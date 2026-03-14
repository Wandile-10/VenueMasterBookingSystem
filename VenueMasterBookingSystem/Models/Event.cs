using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenueMasterBookingSystem.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public int VenueId { get; set; }

        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}