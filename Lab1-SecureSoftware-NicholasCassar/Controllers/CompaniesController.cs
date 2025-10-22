
// I, Nicholas Cassar, student number 000902104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1_SecureSoftware_NicholasCassar.Data;
using Lab1_SecureSoftware_NicholasCassar.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab1_SecureSoftware_NicholasCassar.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        /// <summary>
        /// This method retrieves a list of companies to be displayed on the website. This method is authorized by both
        /// the employee and supervisor roles. 
        /// </summary>
        /// <returns>A "ViewResult" viewable list of companies formatted in a nice uniform table on the webpage.</returns>
        [Authorize(Roles ="Supervisor, Employee")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Company.ToListAsync());
        }

        /// <summary>
        /// Details - 
        ///This method retrieves a companies detailes by the "id" accessor. 
        /// </summary>
        /// <param name="id">The ID value of that company (key)</param>
        /// <returns>A "ViewResult" viewable response holding the details of the object specified. If no object is found
        /// a not found response is returned.</returns>
        // GET: Companies/Details/5
        [Authorize(Roles = "Supervisor, Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        /// <summary>
        /// This methods role is to populate the webpage with the appropriate view response upon creation. This view
        /// holds all companies that were previously created.
        /// </summary>
        /// <returns>A "ViewResult" viewable response.</returns>
        // GET: Companies/Create
        [Authorize(Roles = "Supervisor")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This method returns the view of a company upon the creation of a new company. When the create
        /// method is called a company is created and the main company page view with that updated company
        /// is displayed.
        /// </summary>
        /// <param name="company">The company object to be created.</param>
        /// <returns>A "ViewResult" viewable response holding the details of the object specified.</returns>
        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Create([Bind("Id,Name,YearsInBusiness,Website,Province")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        /// <summary>
        /// Edit - 
        /// This method is incharge of updating the webpage to the /edit view. This enables the user to access the UI to complete
        /// an update on the company model.
        /// </summary>
        /// <param name="id">The company id referencing the company at which a supervisor would like to edit. </param>
        /// <returns>A "ViewResult" viewable response holding the updated view of the object specified. If no object
        /// is found for edit, a not found result is returned.</returns>
        // GET: Companies/Edit/5
        [Authorize(Roles = "Supervisor")]

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        /// <summary>
        /// Edit - 
        /// This method is in charge of updating one or many of the paramaters held by a company. This enables a supervisor
        /// to enter the edit page, and make changes to a previously published company. 
        /// </summary>
        /// <param name="id">The company id referencing the company at which a supervisor would like to edit. </param>
        /// <returns>A view result holding the specified company. If no company is found, a not found result is returned.</returns>
        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,YearsInBusiness,Website,Province")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        /// <summary>
        /// Delete - 
        /// This methods purpose is to complete a delete operation on a Company object. This will ultimetly remove
        /// the data stored at that Company Id's location. 
        /// </summary>
        /// <param name="id">The ID referencing the Company to be deleted.</param>
        /// <returns>A view result holding the object specified or a not found result.</returns>
        // GET: Companies/Delete/5
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        /// <summary>
        /// DeleteConfirmed - 
        /// This methods purpose is to confirm a deletion was successful. 
        /// </summary>
        /// <param name="id">The ID holding a reference to the company we are confirming a deletion on.</param>
        /// <returns>A redirect to the specified action name.</returns>
        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var company = await _context.Company.FindAsync(id);
            if (company != null)
            {
                _context.Company.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// CompanyExists -
        /// This method is in charge of evaluating whether a company exists within the database or not. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A true or false statement representing if the object was found or not.</returns>
        private bool CompanyExists(string id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
    }
}
