using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using ReservationBicycles.Const;
using Microsoft.AspNetCore.Authorization;

namespace ReservationBicycles.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class LocationBicyclettesController : Controller
    {
        private readonly LocationVeloDbContext _context;

        public LocationBicyclettesController(LocationVeloDbContext context)
        {
            _context = context;
        }

        // GET: Admin/LocationBicyclettes
        public async Task<IActionResult> Index()
        {
            var locationVeloDbContext = _context.LocationBicyclettes.Include(l => l.Statut).Include(l => l.Bicyclette).ThenInclude(l => l.ModeleNavigation).Include(l => l.Client).Include(l => l.ModePaiementNavigation);
            return View(await locationVeloDbContext.OrderByDescending(r=>r.DateDebut).ToListAsync());
        }
        public async Task<IActionResult> AccepterReservation(int id)
        {
            if (id != 0)
            {
                var reservation = _context.LocationBicyclettes.Find(id);
                if (reservation != null)
                {
                    reservation.StatutId = (int)StatutEnum.ACCEPTER;
                    _context.LocationBicyclettes.Update(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RefuserReservation(int id)
        {
            if (id != 0)
            {
                var reservation = _context.LocationBicyclettes.Find(id);
                if (reservation != null)
                {
                    reservation.StatutId = (int)StatutEnum.REFUSER;
                    _context.LocationBicyclettes.Update(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CloturerReservation(int id)
        {
            if (id != 0)
            {
                var reservation = _context.LocationBicyclettes.Find(id);
                if (reservation != null)
                {
                    reservation.StatutId = (int)StatutEnum.CLOTURER;
                    _context.LocationBicyclettes.Update(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            return RedirectToAction(nameof(Index));
        }
        //// GET: Admin/LocationBicyclettes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var locationBicyclette = await _context.LocationBicyclettes
        //        .Include(l => l.Bicyclette)
        //        .Include(l => l.Client)
        //        .Include(l => l.ModePaiementNavigation)
        //        .Include(l => l.Statut)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (locationBicyclette == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(locationBicyclette);
        //}

        // GET: Admin/LocationBicyclettes/Create
        public IActionResult Create()
        {
            var listBicyclette = _context.Bicyclettes.ToList();
            var listClients = _context.Clients.ToList();
            var listModePaiement = _context.ModePaiements.ToList();
            ViewBag.listBicyclette = listBicyclette;
            ViewBag.ListClients = listClients;
            ViewBag.ModePaiement = listModePaiement;
            return View();
        }

        // POST: Admin/LocationBicyclettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BicycletteId,ClientId,DateDebut,DateFin,ModePaiement")] LocationBicyclette model)
        {
            if (model.BicycletteId != 0 && model.ClientId != 0 && model.ModePaiement != 0)
            {

                var bicyclette = _context.Bicyclettes.Find(model.BicycletteId);
                model.Totale = bicyclette.Prix;
                model.StatutId = (int)StatutEnum.ACCEPTER;
                _context.LocationBicyclettes.Add(model);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/LocationBicyclettes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationBicyclette = await _context.LocationBicyclettes.FindAsync(id);
            if (locationBicyclette == null)
            {
                return NotFound();
            }
            ViewData["BicycletteId"] = new SelectList(_context.Bicyclettes, "Id", "Id", locationBicyclette.BicycletteId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Adresse", locationBicyclette.ClientId);
            ViewData["ModePaiement"] = new SelectList(_context.ModePaiements, "Id", "Libelle", locationBicyclette.ModePaiement);
            ViewData["StatutId"] = new SelectList(_context.LocationBicyclettes, "Id", "Id", locationBicyclette.StatutId);
            return View(locationBicyclette);
        }

        // POST: Admin/LocationBicyclettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BicycletteId,ClientId,DateDebut,DateFin,DateRetour,ModePaiement,Totale,StatutId")] LocationBicyclette locationBicyclette)
        {
            if (id != locationBicyclette.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationBicyclette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationBicycletteExists(locationBicyclette.Id))
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
            ViewData["BicycletteId"] = new SelectList(_context.Bicyclettes, "Id", "Id", locationBicyclette.BicycletteId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Adresse", locationBicyclette.ClientId);
            ViewData["ModePaiement"] = new SelectList(_context.ModePaiements, "Id", "Libelle", locationBicyclette.ModePaiement);
            ViewData["StatutId"] = new SelectList(_context.LocationBicyclettes, "Id", "Id", locationBicyclette.StatutId);
            return View(locationBicyclette);
        }

        // GET: Admin/LocationBicyclettes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var locationBicyclette = await _context.LocationBicyclettes
        //        .Include(l => l.Bicyclette)
        //        .Include(l => l.Client)
        //        .Include(l => l.ModePaiementNavigation)
        //        .Include(l => l.Statut)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (locationBicyclette == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(locationBicyclette);
        //}

        //// POST: Admin/LocationBicyclettes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var locationBicyclette = await _context.LocationBicyclettes.FindAsync(id);
        //    if (locationBicyclette != null)
        //    {
        //        _context.LocationBicyclettes.Remove(locationBicyclette);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool LocationBicycletteExists(int id)
        {
            return _context.LocationBicyclettes.Any(e => e.Id == id);
        }
    }
}
