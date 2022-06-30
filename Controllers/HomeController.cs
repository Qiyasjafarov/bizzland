using BizLand.DAL;
using BizLand.Models;
using BizLand.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BizLand.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db { get; }
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel hvm = new HomeViewModel()
            {
                PortfoliDetails = await _db.Portfolios.Include(el=>el.Category).ToListAsync(),
                Categories = await _db.Categories.ToListAsync()
            };
            return View(hvm);
        }
    }
}