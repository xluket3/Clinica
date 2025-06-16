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
    public class HistorialTratamientosController : Controller
    {
        private readonly AppDbContext _context;

        public HistorialTratamientosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: HistorialTratamientos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Historiales.Include(h => h.Odontologo).Include(h => h.Paciente).Include(h => h.Tratamiento);
            return View(await appDbContext.ToListAsync());
        }

        // GET: HistorialTratamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialTratamiento = await _context.Historiales
                .Include(h => h.Odontologo)
                .Include(h => h.Paciente)
                .Include(h => h.Tratamiento)
                .FirstOrDefaultAsync(m => m.IdHistorial == id);
            if (historialTratamiento == null)
            {
                return NotFound();
            }

            return View(historialTratamiento);
        }

        // GET: HistorialTratamientos/Create
        public IActionResult Create()
        {
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Id");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Id");
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "IdTratamiento");
            return View();
        }

        // POST: HistorialTratamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHistorial,Fecha,Observaciones,IdPaciente,IdTratamiento,IdOdontologo")] HistorialTratamiento historialTratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialTratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Id", historialTratamiento.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Id", historialTratamiento.IdPaciente);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "IdTratamiento", historialTratamiento.IdTratamiento);
            return View(historialTratamiento);
        }

        // GET: HistorialTratamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialTratamiento = await _context.Historiales.FindAsync(id);
            if (historialTratamiento == null)
            {
                return NotFound();
            }
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Id", historialTratamiento.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Id", historialTratamiento.IdPaciente);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "IdTratamiento", historialTratamiento.IdTratamiento);
            return View(historialTratamiento);
        }

        // POST: HistorialTratamientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHistorial,Fecha,Observaciones,IdPaciente,IdTratamiento,IdOdontologo")] HistorialTratamiento historialTratamiento)
        {
            if (id != historialTratamiento.IdHistorial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialTratamientoExists(historialTratamiento.IdHistorial))
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
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Id", historialTratamiento.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Id", historialTratamiento.IdPaciente);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "IdTratamiento", historialTratamiento.IdTratamiento);
            return View(historialTratamiento);
        }

        // GET: HistorialTratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialTratamiento = await _context.Historiales
                .Include(h => h.Odontologo)
                .Include(h => h.Paciente)
                .Include(h => h.Tratamiento)
                .FirstOrDefaultAsync(m => m.IdHistorial == id);
            if (historialTratamiento == null)
            {
                return NotFound();
            }

            return View(historialTratamiento);
        }

        // POST: HistorialTratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialTratamiento = await _context.Historiales.FindAsync(id);
            if (historialTratamiento != null)
            {
                _context.Historiales.Remove(historialTratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialTratamientoExists(int id)
        {
            return _context.Historiales.Any(e => e.IdHistorial == id);
        }
    }
}
