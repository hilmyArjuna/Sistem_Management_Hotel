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
    public class PrItemsController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public PrItemsController(HotelManagementDbContext context)
        {
            _context = context;
        }

        // GET: PrItems
        public async Task<IActionResult> Index()
        {
            var hotelManagementDbContext = _context.PrItems.Include(p => p.Item).Include(p => p.Pr);
            return View(await hotelManagementDbContext.ToListAsync());
        }

        // GET: PrItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PrItems == null)
            {
                return NotFound();
            }

            var prItem = await _context.PrItems
                .Include(p => p.Item)
                .Include(p => p.Pr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prItem == null)
            {
                return NotFound();
            }

            return View(prItem);
        }

        // GET: PrItems/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id");
            ViewData["PrId"] = new SelectList(_context.PurchaseRequests, "Id", "Id");
            return View();
        }

        // POST: PrItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrId,ItemId,Qty,HargaEstimasi,Subtotal,Catatan")] PrItem prItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", prItem.ItemId);
            ViewData["PrId"] = new SelectList(_context.PurchaseRequests, "Id", "Id", prItem.PrId);
            return View(prItem);
        }

        // GET: PrItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PrItems == null)
            {
                return NotFound();
            }

            var prItem = await _context.PrItems.FindAsync(id);
            if (prItem == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", prItem.ItemId);
            ViewData["PrId"] = new SelectList(_context.PurchaseRequests, "Id", "Id", prItem.PrId);
            return View(prItem);
        }

        // POST: PrItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PrId,ItemId,Qty,HargaEstimasi,Subtotal,Catatan")] PrItem prItem)
        {
            if (id != prItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrItemExists(prItem.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", prItem.ItemId);
            ViewData["PrId"] = new SelectList(_context.PurchaseRequests, "Id", "Id", prItem.PrId);
            return View(prItem);
        }

        // GET: PrItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PrItems == null)
            {
                return NotFound();
            }

            var prItem = await _context.PrItems
                .Include(p => p.Item)
                .Include(p => p.Pr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prItem == null)
            {
                return NotFound();
            }

            return View(prItem);
        }

        // POST: PrItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PrItems == null)
            {
                return Problem("Entity set 'HotelManagementDbContext.PrItems'  is null.");
            }
            var prItem = await _context.PrItems.FindAsync(id);
            if (prItem != null)
            {
                _context.PrItems.Remove(prItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrItemExists(long id)
        {
          return (_context.PrItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
