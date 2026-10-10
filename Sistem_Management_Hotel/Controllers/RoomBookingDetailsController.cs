using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistem_Management_Hotel.Models;

namespace Sistem_Management_Hotel.Controllers
{
    public class RoomBookingDetailsController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public RoomBookingDetailsController(HotelManagementDbContext context)
        {
            _context = context;
        }

        // GET: RoomBookingDetails
        public async Task<IActionResult> Index()
        {
            var hotelManagementDbContext = _context.RoomBookingDetails.Include(r => r.Booking).Include(r => r.Room);
            return View(await hotelManagementDbContext.ToListAsync());
        }

        // GET: RoomBookingDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.RoomBookingDetails == null)
            {
                return NotFound();
            }

            var roomBookingDetail = await _context.RoomBookingDetails
                .Include(r => r.Booking)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomBookingDetail == null)
            {
                return NotFound();
            }

            return View(roomBookingDetail);
        }

        // GET: RoomBookingDetails/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.RoomBookings, "Id", "Id");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: RoomBookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingId,RoomId,Catatan")] RoomBookingDetail roomBookingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomBookingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.RoomBookings, "Id", "Id", roomBookingDetail.BookingId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomBookingDetail.RoomId);
            return View(roomBookingDetail);
        }

        // GET: RoomBookingDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.RoomBookingDetails == null)
            {
                return NotFound();
            }

            var roomBookingDetail = await _context.RoomBookingDetails.FindAsync(id);
            if (roomBookingDetail == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.RoomBookings, "Id", "Id", roomBookingDetail.BookingId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomBookingDetail.RoomId);
            return View(roomBookingDetail);
        }

        // POST: RoomBookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,BookingId,RoomId,Catatan")] RoomBookingDetail roomBookingDetail)
        {
            if (id != roomBookingDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomBookingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingDetailExists(roomBookingDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.RoomBookings, "Id", "Id", roomBookingDetail.BookingId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomBookingDetail.RoomId);
            return View(roomBookingDetail);
        }

        // GET: RoomBookingDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.RoomBookingDetails == null)
            {
                return NotFound();
            }

            var roomBookingDetail = await _context.RoomBookingDetails
                .Include(r => r.Booking)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomBookingDetail == null)
            {
                return NotFound();
            }

            return View(roomBookingDetail);
        }

        // POST: RoomBookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.RoomBookingDetails == null)
            {
                return Problem("Entity set 'HotelManagementDbContext.RoomBookingDetails'  is null.");
            }
            var roomBookingDetail = await _context.RoomBookingDetails.FindAsync(id);
            if (roomBookingDetail != null)
            {
                _context.RoomBookingDetails.Remove(roomBookingDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomBookingDetailExists(long id)
        {
          return (_context.RoomBookingDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
