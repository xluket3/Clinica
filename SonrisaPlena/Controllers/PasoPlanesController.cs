using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaSonrrisaPlena.Models.Data;
using ClinicaSonrrisaPlena.Models.Entities;

namespace MVCClinica.Controllers
{
    public class PasoPlanesController : Controller
    {
        private readonly AppDbContext _context;

        public PasoPlanesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PasoPlanes
        public async Task<IActionResult> Index()
        {
            var pasos = _context.Pasos.Include(p => p.Plan).Include(p => p.Tratamiento);
            return View(await pasos.ToListAsync());
        }

        // GET: PasoPlanes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var paso = await _context.Pasos
                .Include(p => p.Plan)
                .Include(p => p.Tratamiento)
                .FirstOrDefaultAsync(m => m.IdPaso == id);
            if (paso == null) return NotFound();

            return View(paso);
        }

        // GET: PasoPlanes/Create
        public IActionResult Create(int? idPlan)
        {
            ViewData["IdPlan"] = new SelectList(_context.Planes, "IdPlan", "IdPlan", idPlan);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre");
            return View();
        }

        // POST: PasoPlanes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaso,FechaEstimada,Estado,Observaciones,IdPlan,IdTratamiento")] PasoPlan paso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paso);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "PlanTratamientoes", new { id = paso.IdPlan });
            }
            ViewData["IdPlan"] = new SelectList(_context.Planes, "IdPlan", "IdPlan", paso.IdPlan);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", paso.IdTratamiento);
            return View(paso);
        }

        // GET: PasoPlanes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var paso = await _context.Pasos.FindAsync(id);
            if (paso == null) return NotFound();

            ViewData["IdPlan"] = new SelectList(_context.Planes, "IdPlan", "IdPlan", paso.IdPlan);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", paso.IdTratamiento);
            return View(paso);
        }

        // POST: PasoPlanes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaso,FechaEstimada,Estado,Observaciones,IdPlan,IdTratamiento")] PasoPlan paso)
        {
            if (id != paso.IdPaso) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasoPlanExists(paso.IdPaso)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Details", "PlanTratamientoes", new { id = paso.IdPlan });
            }
            ViewData["IdPlan"] = new SelectList(_context.Planes, "IdPlan", "IdPlan", paso.IdPlan);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", paso.IdTratamiento);
            return View(paso);
        }

        // GET: PasoPlanes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var paso = await _context.Pasos
                .Include(p => p.Plan)
                .Include(p => p.Tratamiento)
                .FirstOrDefaultAsync(m => m.IdPaso == id);
            if (paso == null) return NotFound();

            return View(paso);
        }

        // POST: PasoPlanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paso = await _context.Pasos.FindAsync(id);
            if (paso != null)
            {
                _context.Pasos.Remove(paso);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "PlanTratamientoes", new { id = paso.IdPlan });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PasoPlanExists(int id)
        {
            return _context.Pasos.Any(e => e.IdPaso == id);
        }
    }
}