using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Data;

namespace ReservationBicycles.Controllers
{
    public class BoutiqueController : Controller
    {
        private readonly LocationVeloDbContext _context;
        public BoutiqueController(LocationVeloDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            var BikesList = _context.Bicyclettes.Include(r => r.ModeleNavigation).Include(r => r.EtatNavigation).ToList();
            if (id != 0 && id != null)
            {
                BikesList =  BikesList.Where(r => r.Modele == id).ToList();
            }
           var ModelBycyclette  = _context.ModeleBicyclettes.ToList();
            var selectList = ModelBycyclette.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(), 
                Text = m.Code       
            }).ToList();

            // Passer la liste à la vue via ViewData
            ViewData["Roles"] = selectList;

            return View(BikesList);
        }
    }
}
