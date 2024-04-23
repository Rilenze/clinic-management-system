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
    public class MedicalReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicalReports
        public async Task<IActionResult> Index(int admissionId, int? medicalReportId)
        {
            if (medicalReportId != null)
            {
                return RedirectToAction("Edit", new { id = admissionId });
            }
            else
            {
                return RedirectToAction("Create", new { id = admissionId });
            }

            //var applicationDbContext = _context.MedicalReports.Include(m => m.Admission);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedicalReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalReport = await _context.MedicalReports
                .Include(m => m.Admission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalReport == null)
            {
                return NotFound();
            }

            return View(medicalReport);
        }

        // GET: MedicalReports/Create
        public IActionResult Create(int id)
        {

            ViewData["AdmissionId"] = id;
            //ViewData["AdmissionId"] = new SelectList(_context.Admissions, "Id", "Id", medicalReport.AdmissionId);
            return View();
        }

        // POST: MedicalReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, MedicalReport medicalReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalReport);
                await _context.SaveChangesAsync();

                var admission = await _context.Admissions.FindAsync(medicalReport.AdmissionId);
                admission.MedicalReport = medicalReport;

                _context.Update(admission);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", controllerName: "Admissions");
            }
            //ViewData["AdmissionId"] = new SelectList(_context.Admissions, "Id", "Id", medicalReport.AdmissionId);
            return View(medicalReport);
        }

        // GET: MedicalReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalReport = await _context.MedicalReports.FindAsync(id);
            if (medicalReport == null)
            {
                return NotFound();
            }
            ViewData["AdmissionId"] = new SelectList(_context.Admissions, "Id", "Id", medicalReport.AdmissionId);
            return View(medicalReport);
        }

        // POST: MedicalReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReportDescription,CreationDate,AdmissionId")] MedicalReport medicalReport)
        {
            if (id != medicalReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalReportExists(medicalReport.Id))
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
            ViewData["AdmissionId"] = new SelectList(_context.Admissions, "Id", "Id", medicalReport.AdmissionId);
            return View(medicalReport);
        }

        // GET: MedicalReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalReport = await _context.MedicalReports
                .Include(m => m.Admission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalReport == null)
            {
                return NotFound();
            }

            return View(medicalReport);
        }

        // POST: MedicalReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalReport = await _context.MedicalReports.FindAsync(id);
            if (medicalReport != null)
            {
                _context.MedicalReports.Remove(medicalReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalReportExists(int id)
        {
            return _context.MedicalReports.Any(e => e.Id == id);
        }

    }
}
