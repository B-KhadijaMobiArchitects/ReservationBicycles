using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace ReservationBicycles.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BicyclettesController : Controller
    {
        private readonly LocationVeloDbContext _context;

        public BicyclettesController(LocationVeloDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Bicyclettes
        public async Task<IActionResult> Index()
        {
            var locationVeloDbContext = _context.Bicyclettes.Include(b => b.EtatNavigation).Include(b => b.ModeleNavigation);
            return View(await locationVeloDbContext.ToListAsync());
        }

        // GET: Admin/Bicyclettes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bicyclette = await _context.Bicyclettes
                .Include(b => b.EtatNavigation)
                .Include(b => b.ModeleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bicyclette == null)
            {
                return NotFound();
            }

            return View(bicyclette);
        }

        // GET: Admin/Bicyclettes/Create
        public IActionResult Create()
        {

            var listStatut = _context.StatutBicyclettes.ToList();
            var listModel = _context.ModeleBicyclettes.ToList();

            ViewBag.ListStatut = listStatut;
            ViewBag.ListModel = listModel;
            return View();
        }
         
        // POST: Admin/Bicyclettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Libelle,Modele,Prix,DateAchat,Etat")] Bicyclette bicyclette)
        {
            if(bicyclette.Etat!=0 && bicyclette.Modele!=0)
            {
                var modele = _context.ModeleBicyclettes.Find(bicyclette.Modele);
                var etat = _context.StatutBicyclettes.Find(bicyclette.Etat);
                if (modele != null && etat != null) 
                {
                    bicyclette.ModeleNavigation = modele;
                    bicyclette.EtatNavigation = etat;
                    bicyclette.Disponibilite = true;
                    _context.Add(bicyclette);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(nameof(Index));
        }

        // GET: Admin/Bicyclettes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bicyclette = await _context.Bicyclettes.FindAsync(id);
            if (bicyclette == null)
            {
                return NotFound();
            }
            var listStatut = _context.StatutBicyclettes.ToList();
            var listModel = _context.ModeleBicyclettes.ToList();

            ViewBag.ListStatut = listStatut;
            ViewBag.ListModel = listModel;
            return View(bicyclette);
        }

        // POST: Admin/Bicyclettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Libelle,Modele,Prix,DateAchat,Etat")] Bicyclette bicyclette)
        {
            if (id != bicyclette.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bicyclette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BicycletteExists(bicyclette.Id))
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
            var listStatut = _context.StatutBicyclettes.ToList();
            var listModel = _context.ModeleBicyclettes.ToList();

            ViewBag.ListStatut = listStatut;
            ViewBag.ListModel = listModel;
            return View(bicyclette);
        }

        // GET: Admin/Bicyclettes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bicyclette = await _context.Bicyclettes
        //        .Include(b => b.EtatNavigation)
        //        .Include(b => b.ModeleNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (bicyclette == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bicyclette);
        //}

        // POST: Admin/Bicyclettes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var bicyclette = await _context.Bicyclettes.FindAsync(id);
            if (bicyclette != null)
            {
                _context.Bicyclettes.Remove(bicyclette);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BicycletteExists(int id)
        {
            return _context.Bicyclettes.Any(e => e.Id == id);
        }
    }
}
