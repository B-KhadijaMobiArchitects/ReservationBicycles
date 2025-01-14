using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationBicycles.Const;
using ReservationBicycles.Models;
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
                BikesList = BikesList.Where(r => r.Modele == id).ToList();
            }
            var ModelBycyclette = _context.ModeleBicyclettes.ToList();
            var selectList = ModelBycyclette.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Code
            }).ToList();
            var listModePaiement = _context.ModePaiements.ToList();
            ViewBag.ModePaiement = listModePaiement;
            // Passer la liste à la vue via ViewData
            ViewData["Roles"] = selectList;

            return View(BikesList);
        }
        public IActionResult Details(int? id)
        {
            if (id != 0)
            {
                var bike = _context.Bicyclettes.Include(r => r.ModeleNavigation).Include(r => r.EtatNavigation).FirstOrDefault(r => r.Id == id);
                var listModePaiement = _context.ModePaiements.ToList();
                ViewBag.ModePaiement = listModePaiement;
                return View(bike);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Reserverbike(ReservationModel model)
        {
            if (model.BicycletteId != 0 && model.BicycletteId != null)
            {
                int clientId;

                // Gestion des clients existants
                if (model.ClientType == "existing")
                {
                    var client = _context.Clients.FirstOrDefault(r => r.Email == model.Email);
                    if (client != null)
                    {
                        clientId = client.Id;
                    }
                    else
                    {
                        TempData["Error"] = "Client non trouvé. Veuillez vérifier les informations.";
                        return RedirectToAction("Details", new { id = model.BicycletteId });

                    }
                }
                // Gestion des nouveaux clients
                else if (model.ClientType == "new" &&
                         !string.IsNullOrEmpty(model.Email) &&
                         !string.IsNullOrEmpty(model.Telephone) &&
                         !string.IsNullOrEmpty(model.Adresse) &&
                         !string.IsNullOrEmpty(model.NomClient) &&
                         !string.IsNullOrEmpty(model.PrenomClient))
                {
                    var newClient = new Client
                    {
                        PrenomClient = model.PrenomClient,
                        NomClient = model.NomClient,
                        Email = model.Email,
                        Telephone = model.Telephone,
                        Adresse = model.Adresse,
                        DateCreation = DateTime.Now
                    };
                    _context.Clients.Add(newClient);
                    await _context.SaveChangesAsync();
                    clientId = newClient.Id;
                }
                else
                {
                    TempData["Error"] = "Informations client manquantes ou invalides.";
                    return RedirectToAction("Details", new { id = model.BicycletteId });
                }

                // Création de la réservation
                var newReservation = new LocationBicyclette
                {
                    BicycletteId = model.BicycletteId,
                    ClientId = clientId,
                    DateDebut = model.DateDebut,
                    DateFin = model.DateFin,
                    ModePaiement = model.ModePaiement,
                    StatutId = (int)StatutEnum.CREER
                };
                _context.LocationBicyclettes.Add(newReservation);
                await _context.SaveChangesAsync();

                // Ajout d'un message de succès
                TempData["Success"] = "Réservation créée avec succès !";
                return RedirectToAction("Details", new { id = model.BicycletteId });
            }

            TempData["Error"] = "ID de bicyclette invalide.";
            return RedirectToAction("Index");
        }

    }
}
