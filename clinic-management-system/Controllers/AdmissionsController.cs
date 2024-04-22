using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinic_management_system.Data;
using clinic_management_system.Models;

namespace clinic_management_system.Controllers
{
    public class AdmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admissions
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var admissions = _context.Admissions.Include(a => a.Doctor).Include(a => a.Patient);

            if (fromDate != null && toDate != null)
            {
                var admissions2 = _context.Admissions
                    .Include(a => a.Doctor).Include(a => a.Patient)
                    .Where(a => a.AdmissionDateTime >= fromDate && a.AdmissionDateTime <= toDate);
                return View(await admissions2.ToListAsync());
            }

            return View(await admissions.ToListAsync());
        }

        // GET: Admissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // GET: Admissions/Create
        public IActionResult Create()
        {
            ViewData["doctors"] = _context.Doctors.Where(d => d.Title == "Specialist").ToList();
            var filteredPatients = _context.Patients
                .Where(p => !_context.Admissions.Any(a => a.PatientId == p.Id))
                .ToList();
            ViewData["patients"] = filteredPatients;

            //ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name");
            //ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            return View();
        }

        // POST: Admissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdmissionDateTime,PatientId,DoctorId,Urgency")] Admission admission)
        {

            if (ModelState.IsValid)
            {
                _context.Add(admission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["doctors"] = _context.Doctors;
            ViewData["patients"] = _context.Patients;
            //ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Code", admission.DoctorId);
            //ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Gender", admission.PatientId);
            return View(admission);
        }

        // GET: Admissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }

            ViewData["doctors"] = _context.Doctors.Where(d => d.Title == "Specialist").ToList();
            var filteredPatients = _context.Patients
                .Where(p => !_context.Admissions.Any(a => a.PatientId == p.Id && p.Id != admission.PatientId))
                .ToList();
            ViewData["patients"] = filteredPatients;
            //ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Code", admission.DoctorId);
            //ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Gender", admission.PatientId);
            return View(admission);
        }

        // POST: Admissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdmissionDateTime,PatientId,DoctorId,Urgency")] Admission admission)
        {
            if (id != admission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.Id))
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
            ViewData["doctors"] = _context.Doctors;
            ViewData["patients"] = _context.Patients;
            //ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Code", admission.DoctorId);
            //ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Gender", admission.PatientId);
            return View(admission);
        }

        // GET: Admissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // POST: Admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission != null)
            {
                _context.Admissions.Remove(admission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionExists(int id)
        {
            return _context.Admissions.Any(e => e.Id == id);
        }
    }
}
