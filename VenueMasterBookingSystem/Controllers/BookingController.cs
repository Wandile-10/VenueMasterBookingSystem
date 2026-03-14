using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueMasterBookingSystem.Data;
using VenueMasterBookingSystem.Models;
using System.Linq;

namespace VenueMasterBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // VIEW BOOKINGS
        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .ToList();

            return View(bookings);
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            ViewBag.Events = _context.Events.ToList();
            ViewBag.Venues = _context.Venues.ToList();
            return View();
        }

        // CREATE BOOKING
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            var existing = _context.Bookings
                .Any(b => b.VenueId == booking.VenueId && b.EventId == booking.EventId);

            if (existing)
            {
                ModelState.AddModelError("", "This venue is already booked for this event.");
            }

            if (ModelState.IsValid)
            {
                booking.BookingDate = DateTime.Now;

                _context.Bookings.Add(booking);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Events = _context.Events.ToList();
            ViewBag.Venues = _context.Venues.ToList();

            return View(booking);
        }

        // DELETE BOOKING
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _context.Bookings.Find(id);

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
