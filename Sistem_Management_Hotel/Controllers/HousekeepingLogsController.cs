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
    public class HousekeepingLogsController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public HousekeepingLogsController(HotelManagementDbContext context)
        {
            _context = context;
        }

        // GET: HousekeepingLogs
        public async Task<IActionResult> Index()
        {
            var hotelManagementDbContext = _context.HousekeepingLogs.Include(h => h.Room).Include(h => h.User);
            return View(await hotelManagementDbContext.ToListAsync());
        }

        // GET: HousekeepingLogs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.HousekeepingLogs == null)
            {
                return NotFound();
            }

            var housekeepingLog = await _context.HousekeepingLogs
                .Include(h => h.Room)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (housekeepingLog == null)
            {
                return NotFound();
            }

            return View(housekeepingLog);
        }

        // GET: HousekeepingLogs/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: HousekeepingLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,Status,Catatan,UserId,CreatedAt")] HousekeepingLog housekeepingLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(housekeepingLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", housekeepingLog.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", housekeepingLog.UserId);
            return View(housekeepingLog);
        }

        // GET: HousekeepingLogs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.HousekeepingLogs == null)
            {
                return NotFound();
            }

            var housekeepingLog = await _context.HousekeepingLogs.FindAsync(id);
            if (housekeepingLog == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", housekeepingLog.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", housekeepingLog.UserId);
            return View(housekeepingLog);
        }

        // POST: HousekeepingLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,RoomId,Status,Catatan,UserId,CreatedAt")] HousekeepingLog housekeepingLog)
        {
            if (id != housekeepingLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(housekeepingLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HousekeepingLogExists(housekeepingLog.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", housekeepingLog.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", housekeepingLog.UserId);
            return View(housekeepingLog);
        }

        // GET: HousekeepingLogs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.HousekeepingLogs == null)
            {
                return NotFound();
            }

            var housekeepingLog = await _context.HousekeepingLogs
                .Include(h => h.Room)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (housekeepingLog == null)
            {
                return NotFound();
            }

            return View(housekeepingLog);
        }

        // POST: HousekeepingLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.HousekeepingLogs == null)
            {
                return Problem("Entity set 'HotelManagementDbContext.HousekeepingLogs'  is null.");
            }
            var housekeepingLog = await _context.HousekeepingLogs.FindAsync(id);
            if (housekeepingLog != null)
            {
                _context.HousekeepingLogs.Remove(housekeepingLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousekeepingLogExists(long id)
        {
          return (_context.HousekeepingLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
