using Microsoft.AspNetCore.Mvc;
using VenueMasterBookingSystem.Data;
using VenueMasterBookingSystem.Models;
using System.Linq;

namespace VenueMasterBookingSystem.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // VIEW ALL VENUES
        public IActionResult Index()
        {
            var venues = _context.Venues.ToList();
            return View(venues);
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            return View();
        }

        // CREATE VENUE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Add(venue);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // EDIT PAGE
        public IActionResult Edit(int id)
        {
            var venue = _context.Venues.Find(id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // EDIT VENUE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Update(venue);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // DELETE PAGE
        public IActionResult Delete(int id)
        {
            var venue = _context.Venues.Find(id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // DELETE VENUE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var venue = _context.Venues.Find(id);

            var hasBookings = _context.Bookings.Any(b => b.VenueId == id);

            if (hasBookings)
            {
                return BadRequest("Cannot delete venue with existing bookings.");
            }

            _context.Venues.Remove(venue);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}