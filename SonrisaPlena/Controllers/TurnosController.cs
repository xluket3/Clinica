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
    public class TurnosController : Controller
    {
        private readonly AppDbContext _context;

        public TurnosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Turnos.Include(t => t.Odontologo).Include(t => t.Paciente);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var turno = await _context.Turnos
                .Include(t => t.Odontologo)
                .Include(t => t.Paciente)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            if (turno == null) return NotFound();

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre");
            return View();
        }

        // POST: Turnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTurno,FechaHora,Duracion,Estado,IdPaciente,IdOdontologo")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.IdPaciente);
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null) return NotFound();

            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.IdPaciente);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTurno,FechaHora,Duracion,Estado,IdPaciente,IdOdontologo")] Turno turno)
        {
            if (id != turno.IdTurno) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.IdTurno)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOdontologo"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.IdOdontologo);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.IdPaciente);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var turno = await _context.Turnos
                .Include(t => t.Odontologo)
                .Include(t => t.Paciente)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            if (turno == null) return NotFound();

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ MÉTODO DE RESERVA DESDE EL HOME
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservarDesdePublico(string Nombre, string RUT, string Email, string Telefono, DateTime Fecha, TimeSpan Hora)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.RUT == RUT);

            if (paciente == null)
            {
                paciente = new Paciente
                {
                    Nombre = Nombre,
                    RUT = RUT,
                    Email = Email,
                    Telefono = Telefono,
                    Direccion = "Sin dirección registrada"
                };
                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
            }

            var odontologo = await _context.Odontologos.FirstOrDefaultAsync();
            if (odontologo == null)
            {
                TempData["Mensaje"] = "No hay odontólogos disponibles actualmente.";
                return RedirectToAction("Index", "Home");
            }

            var turno = new Turno
            {
                FechaHora = Fecha.Date + Hora,
                Duracion = 30,
                Estado = "Pendiente",
                IdPaciente = paciente.Id,
                IdOdontologo = odontologo.Id
            };

            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "¡Tu cita ha sido registrada exitosamente!";
            return RedirectToAction("Index", "Home");
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.IdTurno == id);
        }
    }
}

