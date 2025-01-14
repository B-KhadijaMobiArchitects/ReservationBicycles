using System.Diagnostics;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationBicycles.Models;

namespace ReservationBicycles.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocationVeloDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LocationVeloDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bikes =  _context.Bicyclettes
                .Include(b => b.EtatNavigation)
                .Include(b => b.ModeleNavigation)
                .AsEnumerable() 
                .DistinctBy(r => r.ModeleNavigation)
                .Take(3)
                .ToList();
            var models = await _context.ModeleBicyclettes.Take(3).ToListAsync();
            var mostBikes = await _context.Bicyclettes.Include(b => b.EtatNavigation).Include(b => b.ModeleNavigation).Where(r => r.LocationBicyclettes.Any()).ToListAsync();
            var model = new HomeModelView
            {
                ListBicyclette = bikes,
                bicyclettesImportnte = mostBikes,
                modeleBicyclettes = models
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
