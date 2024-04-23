using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinic_management_system.Data;
using clinic_management_system.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

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
            var specialistDoctors = _context.Doctors.Where(d => d.Title == "Specialist").ToList();
            var filteredPatients = _context.Patients
                .Where(p => !_context.Admissions.Any(a => a.PatientId == p.Id))
                .ToList();

            if (filteredPatients.Count == 0 || specialistDoctors.Count == 0)
            {
                TempData["AdmissionCreationError"] = "Unable to create admission " +
                    "because there is no specialist doctor or all patients already have their admission!";
                return RedirectToAction("Index");
            }
            ViewData["doctors"] = specialistDoctors;
            ViewData["patients"] = filteredPatients;
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

        public IActionResult DownloadPDF(int admissionId)
        {
            var admission = _context.Admissions
                                     .Include(a => a.Patient)
                                     .Include(a => a.Doctor)
                                     .FirstOrDefault(a => a.Id == admissionId);

            if (admission == null)
            {
                return NotFound();
            }

            // Generiranje PDF-a
            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // Dodavanje podataka o prijemu, pacijentu, i ljekaru u PDF
            document.Add(new Paragraph($"Admission date and time: {admission.AdmissionDateTime}"));
            if (admission.Urgency) document.Add(new Paragraph($"Urgency: YES"));
            document.Add(new Paragraph($"Patient full name: {admission.Patient.Name} {admission.Patient.Surname}"));
            document.Add(new Paragraph($"Patient birth date: {admission.Patient.DateOfBirth.ToString("MM/dd/yyyy")}"));
            document.Add(new Paragraph($"Patient gender: {admission.Patient.Gender}"));
            if (admission.Patient.Address != null)
                document.Add(new Paragraph($"Patient address: {admission.Patient.Address}"));
            if (admission.Patient.PhoneNumber != null)
                document.Add(new Paragraph($"Patient phone number: {admission.Patient.PhoneNumber}"));
            document.Add(new Paragraph($"Doctor full name: {admission.Doctor.Name} {admission.Doctor.Surname}"));
            document.Add(new Paragraph($"Doctor title: {admission.Doctor.Title}"));
            document.Add(new Paragraph($"Doctor code: {admission.Doctor.Code}"));
           
            if (admission.MedicalReportId != null)
            {
                var medicalReport = _context.MedicalReports.Find(admission.MedicalReportId);
                document.Add(new Paragraph($"Date of medical report creation: {medicalReport.CreationDate}"));
                document.Add(new Paragraph($"Medical report description: {medicalReport.ReportDescription}"));
            }
            document.Close();

            // Vraćanje PDF-a kao File rezultata
            return File(memoryStream.ToArray(), "application/pdf", "Podaci_o_prijemu.pdf");
        }

        private bool AdmissionExists(int id)
        {
            return _context.Admissions.Any(e => e.Id == id);
        }
    }
}
