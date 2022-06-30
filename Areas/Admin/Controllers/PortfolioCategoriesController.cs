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
    public class PortfolioCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PortfolioCategories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Categories'  is null.");
        }

        // GET: Admin/PortfolioCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // GET: Admin/PortfolioCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PortfolioCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,DataFilter,Id")] PortfolioCategory portfolioCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioCategory);
        }

        // GET: Admin/PortfolioCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.Categories.FindAsync(id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }
            return View(portfolioCategory);
        }

        // POST: Admin/PortfolioCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryName,DataFilter,Id")] PortfolioCategory portfolioCategory)
        {
            if (id != portfolioCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioCategoryExists(portfolioCategory.Id))
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
            return View(portfolioCategory);
        }

        // GET: Admin/PortfolioCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // POST: Admin/PortfolioCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'AppDbContext.Categories'  is null.");
            }
            var portfolioCategory = await _context.Categories.FindAsync(id);
            if (portfolioCategory != null)
            {
                _context.Categories.Remove(portfolioCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioCategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
