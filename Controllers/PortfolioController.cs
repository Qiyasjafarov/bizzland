using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizLand.Controllers
{
    public class PortfolioController : Controller
    {
        private AppDbContext _db { get; }
        public PortfolioController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Detailed(int id)
        {
            PortfolioItem portfolio = await _db.Portfolios.Where(el=>el.Id==id).Include(el=>el.Category).FirstOrDefaultAsync(); 
            return View(portfolio);
        }
    }
}
