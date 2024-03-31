using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        // GET: Employee/Index
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Service) // Include the Service navigation property
                .ToListAsync();

            return View(employees);
        }


        // GET: Employee/Details/5
        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Service) // Include the Service navigation property
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        /// EmployeeController

        // GET: Employee/CreateEmployee
        public IActionResult CreateEmployee()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            return View();
        }

        // POST: Employee/CreateEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee([Bind("EmployeeId,PersonId,Name,Address,ServiceId")] Employee employee)
        {

            Console.WriteLine(ModelState.IsValid);

            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }

                // If model state is not valid, reload the form with the available services
                ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", employee.ServiceId);
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                // Check if the selected service ID is valid
                var service = await _context.Services.FindAsync(employee.ServiceId);
                if (service != null)
                {
                    // Associate the selected service with the employee
                    employee.Service = service;
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If model state is not valid, reload the form with the available services
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", employee.ServiceId);
            return View(employee);
        }


        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Service) // Include the Service navigation property
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            // Prepare the list of available services for the dropdown
            ViewData["AvailableServices"] = new SelectList(_context.Services, "ServiceId", "ServiceName", employee.ServiceId);

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,PersonId,Name,Address,ServiceId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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

            // If model state is not valid, reload the form with the available services
            ViewData["AvailableServices"] = new SelectList(_context.Services, "ServiceId", "ServiceName", employee.ServiceId);
            return View(employee);
        }



        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Service) // Include the Service navigation property
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
