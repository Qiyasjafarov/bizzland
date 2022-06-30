using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BizLand.DAL;
using BizLand.Models;

namespace BizLand.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioItemsController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PortfolioItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Portfolios.Include(p => p.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/PortfolioItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Portfolios == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.Portfolios
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: Admin/PortfolioItems/Create
        public IActionResult Create()
        {
            ViewData["PortfolioCategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Admin/PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PortfolioCategoryId,Name,ClientName,Date,ProjectUrl,Image,DetailedInformation,Id")] PortfolioItem portfolioItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioCategoryId"] = new SelectList(_context.Categories, "Id", "Id", portfolioItem.PortfolioCategoryId);
            return View(portfolioItem);
        }

        // GET: Admin/PortfolioItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Portfolios == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.Portfolios.FindAsync(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            ViewData["PortfolioCategoryId"] = new SelectList(_context.Categories, "Id", "Id", portfolioItem.PortfolioCategoryId);
            return View(portfolioItem);
        }

        // POST: Admin/PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PortfolioCategoryId,Name,ClientName,Date,ProjectUrl,Image,DetailedInformation,Id")] PortfolioItem portfolioItem)
        {
            if (id != portfolioItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(portfolioItem.Id))
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
            ViewData["PortfolioCategoryId"] = new SelectList(_context.Categories, "Id", "Id", portfolioItem.PortfolioCategoryId);
            return View(portfolioItem);
        }

        // GET: Admin/PortfolioItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Portfolios == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.Portfolios
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: Admin/PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Portfolios == null)
            {
                return Problem("Entity set 'AppDbContext.Portfolios'  is null.");
            }
            var portfolioItem = await _context.Portfolios.FindAsync(id);
            if (portfolioItem != null)
            {
                _context.Portfolios.Remove(portfolioItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
          return (_context.Portfolios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
