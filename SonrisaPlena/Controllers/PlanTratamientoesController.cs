using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaSonrrisaPlena.Models.Data;
using ClinicaSonrrisaPlena.Models.Entities;

// ✅ PLANESTRATAMIENTOES CONTROLLER - MEJORADO
namespace MVCClinica.Controllers
{
    public class PlanTratamientoesController : Controller
    {
        private readonly AppDbContext _context;

        public PlanTratamientoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlanTratamientoes
        public async Task<IActionResult> Index()
        {
            var planes = _context.Planes.Include(p => p.Odontologo).Include(p => p.Paciente);
            return View(await planes.ToListAsync());
        }

        // GET: PlanTratamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.Planes
                .Include(p => p.Odontologo)
                .Include(p => p.Paciente)
                .Include(p => p.Pasos)
                    .ThenInclude(p => p.Tratamiento)
                .FirstOrDefaultAsync(m => m.IdPlan == id);

            if (plan == null) return NotFound();

            return View(plan);
        }

        // GET: PlanTratamientoes/Create
        public IActionResult Create()
        {
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre");
            return View();
        }

        // POST: PlanTratamientoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlan,FechaCreacion,ObservacionesGenerales,IdPaciente,IdOdontologo")] PlanTratamiento plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", plan.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", plan.IdPaciente);
            return View(plan);
        }

        // GET: PlanTratamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.Planes.FindAsync(id);
            if (plan == null) return NotFound();

            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", plan.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", plan.IdPaciente);
            return View(plan);
        }

        // POST: PlanTratamientoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlan,FechaCreacion,ObservacionesGenerales,IdPaciente,IdOdontologo")] PlanTratamiento plan)
        {
            if (id != plan.IdPlan) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTratamientoExists(plan.IdPlan)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", plan.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", plan.IdPaciente);
            return View(plan);
        }

        // GET: PlanTratamientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.Planes
                .Include(p => p.Odontologo)
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.IdPlan == id);

            if (plan == null) return NotFound();

            return View(plan);
        }

        // POST: PlanTratamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Planes.FindAsync(id);
            if (plan != null)
            {
                _context.Planes.Remove(plan);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PlanTratamientoExists(int id)
        {
            return _context.Planes.Any(e => e.IdPlan == id);
        }
    }
}



