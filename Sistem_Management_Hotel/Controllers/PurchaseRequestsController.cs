using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistem_Management_Hotel.Models;
//PurchaseRequest
namespace Sistem_Management_Hotel.Controllers
{
    public class PurchaseRequestsController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public PurchaseRequestsController(HotelManagementDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseRequests
        public async Task<IActionResult> Index()
        {
            var hotelManagementDbContext = _context.PurchaseRequests.Include(p => p.ApprovedByNavigation).Include(p => p.CreatedByNavigation).Include(p => p.Supplier);
            return View(await hotelManagementDbContext.ToListAsync());
        }

        // GET: PurchaseRequests/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PurchaseRequests == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequests
                .Include(p => p.ApprovedByNavigation)
                .Include(p => p.CreatedByNavigation)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Create
        public IActionResult Create()
        {
            ViewData["ApprovedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: PurchaseRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KodePr,SupplierId,Tanggal,TotalEstimasi,Alasan,Status,ApprovedBy,ApprovedAt,CatatanApproval,CreatedBy,CreatedAt,UpdatedAt")] PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovedBy"] = new SelectList(_context.Users, "Id", "Id", purchaseRequest.ApprovedBy);
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", purchaseRequest.CreatedBy);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", purchaseRequest.SupplierId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PurchaseRequests == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequests.FindAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }
            ViewData["ApprovedBy"] = new SelectList(_context.Users, "Id", "Id", purchaseRequest.ApprovedBy);
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", purchaseRequest.CreatedBy);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", purchaseRequest.SupplierId);
            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,KodePr,SupplierId,Tanggal,TotalEstimasi,Alasan,Status,ApprovedBy,ApprovedAt,CatatanApproval,CreatedBy,CreatedAt,UpdatedAt")] PurchaseRequest purchaseRequest)
        {
            if (id != purchaseRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseRequestExists(purchaseRequest.Id))
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
            ViewData["ApprovedBy"] = new SelectList(_context.Users, "Id", "Id", purchaseRequest.ApprovedBy);
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", purchaseRequest.CreatedBy);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", purchaseRequest.SupplierId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PurchaseRequests == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequests
                .Include(p => p.ApprovedByNavigation)
                .Include(p => p.CreatedByNavigation)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PurchaseRequests == null)
            {
                return Problem("Entity set 'HotelManagementDbContext.PurchaseRequests'  is null.");
            }
            var purchaseRequest = await _context.PurchaseRequests.FindAsync(id);
            if (purchaseRequest != null)
            {
                _context.PurchaseRequests.Remove(purchaseRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseRequestExists(long id)
        {
          return (_context.PurchaseRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
