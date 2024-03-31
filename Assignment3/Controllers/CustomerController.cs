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
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var customersWithServices = _context.Customers.Include(c => c.Services);
            return View(await customersWithServices.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Services) // Include the Services navigation property
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["ServiceIds"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,PersonId,Name,Address,ServiceIds")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.ServiceIds != null && customer.ServiceIds.Count > 0)
                {
                    foreach (var serviceId in customer.ServiceIds)
                    {
                        var service = await _context.Services.FindAsync(serviceId);
                        if (service != null)
                        {
                            customer.Services.Add(service);
                        }
                    }
                }

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceIds"] = new SelectList(_context.Services, "ServiceId", "ServiceName", customer.ServiceIds);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Services)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            // Prepare the list of available services for the dropdown
            ViewData["AvailableServices"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            // Prepare the list of IDs of the services currently associated with the customer
            ViewBag.SelectedServices = customer.Services.Select(s => s.ServiceId).ToList();

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,PersonId,Name,Address,ServiceIds")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customerToUpdate = await _context.Customers.Include(c => c.Services).FirstOrDefaultAsync(m => m.CustomerId == id);

                if (customerToUpdate != null)
                {
                    customerToUpdate.PersonId = customer.PersonId;
                    customerToUpdate.Name = customer.Name;
                    customerToUpdate.Address = customer.Address;

                    // Clear existing services and add the newly selected ones
                    customerToUpdate.Services.Clear();
                    if (customer.ServiceIds != null)
                    {
                        foreach (var serviceId in customer.ServiceIds)
                        {
                            var serviceToAdd = await _context.Services.FindAsync(serviceId);
                            if (serviceToAdd != null)
                            {
                                customerToUpdate.Services.Add(serviceToAdd);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Reload the form if we get here
            ViewData["AvailableServices"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            return View(customer);
        }


        private void UpdateCustomerServices(int[] selectedServices, Customer customerToUpdate)
        {
            // Assuming your context has a DbSet named Services
            var allServices = _context.Services.ToList();
            if (selectedServices == null)
            {
                customerToUpdate.Services = new List<Service>();
                return;
            }

            var selectedServicesHS = new HashSet<int>(selectedServices);
            var customerServicesHS = new HashSet<int>(customerToUpdate.Services.Select(c => c.ServiceId));
            foreach (var service in allServices)
            {
                if (selectedServicesHS.Contains(service.ServiceId))
                {
                    if (!customerServicesHS.Contains(service.ServiceId))
                    {
                        customerToUpdate.Services.Add(service);
                    }
                }
                else
                {
                    if (customerServicesHS.Contains(service.ServiceId))
                    {
                        customerToUpdate.Services.Remove(service);
                    }
                }
            }
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Services) // Include the Services navigation property
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

        // GET: Customer/RegisterService/5
        public async Task<IActionResult> RegisterService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.Services = await _context.Services.ToListAsync();
            return View(customer);
        }

        // POST: Customer/RegisterService/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterService(int customerId, int serviceId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            var service = await _context.Services.FindAsync(serviceId);

            if (customer != null && service != null)
            {
                customer.Services.Add(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Customer/UnregisterService/5
        public async Task<IActionResult> UnregisterService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.Services = await _context.Services.ToListAsync(); // Set ViewBag.Services here
            return View(customer);
        }


        // POST: Customer/UnregisterService/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnregisterService(int customerId, int serviceId)
        {
            var customer = await _context.Customers.Include(c => c.Services).FirstOrDefaultAsync(c => c.CustomerId == customerId);
            var service = await _context.Services.FindAsync(serviceId);

            if (customer != null && service != null)
            {
                customer.Services.Remove(service);
                // Mark the customer entity as modified. This might be unnecessary but can be used for troubleshooting.
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
