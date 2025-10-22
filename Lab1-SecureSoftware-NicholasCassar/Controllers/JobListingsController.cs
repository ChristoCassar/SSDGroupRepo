using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1_SecureSoftware_NicholasCassar.Data;
using Lab1_SecureSoftware_NicholasCassar.Models;

namespace Lab1_SecureSoftware_NicholasCassar.Controllers
{
    public class JobListingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobListingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobListings
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobListing.ToListAsync());
        }

        // GET: JobListings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListing
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // GET: JobListings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,JobAddress,JobName,Urgency,JobDetails,Location,Salary")] JobListing jobListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobListing);
        }

        // GET: JobListings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListing.FindAsync(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            return View(jobListing);
        }

        // POST: JobListings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,JobName,Urgency,JobDetails,Location,Salary")] JobListing jobListing)
        {
            if (id != jobListing.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListingExists(jobListing.JobId))
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
            return View(jobListing);
        }

        // GET: JobListings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListing
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // POST: JobListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobListing = await _context.JobListing.FindAsync(id);
            if (jobListing != null)
            {
                _context.JobListing.Remove(jobListing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobListingExists(int id)
        {
            return _context.JobListing.Any(e => e.JobId == id);
        }
    }
}
