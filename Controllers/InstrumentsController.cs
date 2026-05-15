// liga views, modelos, base de dados, e usa entity framework core via DbContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _14480_employes_managment.Data;
using _14480_employes_managment.Models;

namespace _14480_employes_managment.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstrumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instrument.ToListAsync()); //mostra todos os funcionários
        }

        // GET: Instrument/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = await _context.Instrument
                .FirstOrDefaultAsync(m => m.Id == id); // Mostra 1 instrumento pelo Id
            if (instrument == null)
            {
                return NotFound();
            }

            return View(instrument);
        }

        // GET: Instrument/Create
        public IActionResult Create() // Mostra Formulário Vazio
        {
            return View();
        }

        // POST: Instrument/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instrument instrument)
        {
            instrument.CreatedById = "Gustavo SLB";
            instrument.CreatedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(instrument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instrument);
        }
        //recebe Instrument do formulário
        //valida ModelState
        //adiciona à BD
        //SaveChangesAsync

        // GET: Instrument/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = await _context.Instrument.FindAsync(id); // carrega os dados
            if (instrument == null)
            {
                return NotFound();
            }
            return View(instrument);
        }

        // POST: Instrument/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoInstrumento,Instrumento,UsaCordas")] Instrument instrument)
        {
            if (id != instrument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instrument); // atualiza os dados
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(instrument.Id))
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
            return View(instrument);
        }

        // GET: Instrument/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = await _context.Instrument
                .FirstOrDefaultAsync(m => m.Id == id); // Mostra a Configuração
            if (instrument == null)
            {
                return NotFound();
            }

            return View(instrument);
        }

        // POST: Instrument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instrument = await _context.Instrument.FindAsync(id);
            if (instrument != null)
            {
                _context.Instrument.Remove(instrument); //remove da BD
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Instrument.Any(e => e.Id == id);
        }
        //Verifica se existe registo na BD.
    }
}
