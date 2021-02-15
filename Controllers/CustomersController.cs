using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodJoint.Data;
using FastFoodJoint.Models;

namespace FastFoodJoint.Controllers
{
    public class CustomersController : Controller
    {
        private readonly FastFoodJointContext _context;

        public CustomersController(FastFoodJointContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var fastFoodJointContext = _context.Customers.Include(c => c.Cuisine).Include(c => c.FoodItem);
            return View(await fastFoodJointContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Cuisine)
                .Include(c => c.FoodItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Name");
            
            if (_context.Cuisines != null && _context.Cuisines.Count() > 0)
            {
                ViewData["FoodItemId"] = new SelectList(_context.FoodItems.Where(x => x.CuisineId == _context.Cuisines.FirstOrDefault().Id), "Id", "Name");
            }
            else
            {
                ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "Name");
            }
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Contact_Number,CuisineId,FoodItemId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Name", customer.CuisineId);
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems.Where(x => x.CuisineId == customer.CuisineId), "Id", "Name", customer.FoodItemId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Name", customer.CuisineId);
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems.Where(x => x.CuisineId == customer.CuisineId), "Id", "Name", customer.FoodItemId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Contact_Number,CuisineId,FoodItemId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Name", customer.CuisineId);
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems.Where(x => x.CuisineId == customer.CuisineId), "Id", "Name", customer.FoodItemId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Cuisine)
                .Include(c => c.FoodItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Json Call to get state  
        [HttpGet]
        public IActionResult GetFoodItems(string id)
        {
            List<SelectListItem> models = new List<SelectListItem>();
            var modelList = _context.FoodItems.Where(cm => cm.CuisineId == Convert.ToInt32(id)).ToList();
            var modelData = modelList.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
            });
            return Ok(modelData);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
