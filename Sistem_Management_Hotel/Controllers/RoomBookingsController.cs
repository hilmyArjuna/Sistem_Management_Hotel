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
    public class RoomBookingsController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public RoomBookingsController(HotelManagementDbContext context)
        {
            _context = context;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index()
        {
            var hotelManagementDbContext = _context.RoomBookings.Include(r => r.CreatedByNavigation).Include(r => r.Guest);
            return View(await hotelManagementDbContext.ToListAsync());
        }

        // GET: RoomBookings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.RoomBookings == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.CreatedByNavigation)
                .Include(r => r.Guest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["GuestId"] = new SelectList(_context.Guests, "Id", "Id");
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KodeBooking,GuestId,TglCheckin,TglCheckout,JumlahMalam,JumlahKamar,MetodeBayar,RefNumber,TotalBayar,Sumber,Status,Keterangan,CreatedBy,CreatedAt,UpdatedAt")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", roomBooking.CreatedBy);
            ViewData["GuestId"] = new SelectList(_context.Guests, "Id", "Id", roomBooking.GuestId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.RoomBookings == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", roomBooking.CreatedBy);
            ViewData["GuestId"] = new SelectList(_context.Guests, "Id", "Id", roomBooking.GuestId);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,KodeBooking,GuestId,TglCheckin,TglCheckout,JumlahMalam,JumlahKamar,MetodeBayar,RefNumber,TotalBayar,Sumber,Status,Keterangan,CreatedBy,CreatedAt,UpdatedAt")] RoomBooking roomBooking)
        {
            if (id != roomBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingExists(roomBooking.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", roomBooking.CreatedBy);
            ViewData["GuestId"] = new SelectList(_context.Guests, "Id", "Id", roomBooking.GuestId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.RoomBookings == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.CreatedByNavigation)
                .Include(r => r.Guest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.RoomBookings == null)
            {
                return Problem("Entity set 'HotelManagementDbContext.RoomBookings'  is null.");
            }
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking != null)
            {
                _context.RoomBookings.Remove(roomBooking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomBookingExists(long id)
        {
          return (_context.RoomBookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
