using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueMasterBookingSystem.Data;
using VenueMasterBookingSystem.Models;
using System.Linq;

namespace VenueMasterBookingSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // VIEW EVENTS
        public IActionResult Index()
        {
            var events = _context.Events.Include(e => e.Venue).ToList();
            return View(events);
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            ViewBag.Venues = _context.Venues.ToList();
            return View();
        }

        // CREATE EVENT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event ev)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(ev);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _context.Venues.ToList();
            return View(ev);
        }

        // DELETE EVENT
        public IActionResult Delete(int id)
        {
            var ev = _context.Events.Find(id);
            if (ev == null)
                return NotFound();

            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var hasBookings = _context.Bookings.Any(b => b.EventId == id);

            if (hasBookings)
            {
                return BadRequest("Cannot delete event with bookings.");
            }

            var ev = _context.Events.Find(id);

            _context.Events.Remove(ev);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
