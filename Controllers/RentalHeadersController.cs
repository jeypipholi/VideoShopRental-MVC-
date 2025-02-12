using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoShopRentalV3.Data;
using VideoShopRentalV3.Models;

namespace VideoShopRentalV3.Controllers
{
    public class RentalHeadersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalHeadersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RentalHeaders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RentalHeaders.Include(r => r.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RentalHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalHeader = await _context.RentalHeaders
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RentalHeaderId == id);
            if (rentalHeader == null)
            {
                return NotFound();
            }

            return View(rentalHeader);
        }

        // GET: RentalHeaders/Create
        /* public IActionResult Create()
         {

             ViewData["Title"] = new SelectList(_context.Movies, "Movies", "Title");
             ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
             return View();
         }*/
        public IActionResult Create()
        {
            ViewBag.Movies = _context.Movies.Select(m => new SelectListItem
            {
                Value = m.MovieId.ToString(),
                Text = m.Title
            }).ToList();
            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: RentalHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalHeaderId,CustomerId,RentalDate,ReturnDate,MovieIds")] RentalHeader rentalHeader,Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentalHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var movies = _context.Movies.Select(m => new SelectListItem
            {
                Value = m.MovieId.ToString(),
                Text = m.Title
            }).ToList();
            ViewBag.Title = movies;
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", rentalHeader.CustomerId);
            return View(rentalHeader);
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,RentalDate,ReturnDate,MovieIds")] RentalHeader rentalHeader)
        {
            if (ModelState.IsValid)
            {
                // Add the RentalHeader and save to get the generated ID
                _context.Add(rentalHeader);
                await _context.SaveChangesAsync();

                // Create and save RentalDetails after RentalHeader is saved
                foreach (var movieId in rentalHeader.MovieIds)
                {
                    var rentalDetail = new RentalDetail
                    {
                        RentalHeaderId = rentalHeader.RentalHeaderId, // Now the correct ID is used
                        MovieId = movieId
                    };
                    _context.RentalDetails.Add(rentalDetail);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Movies = _context.Movies.Select(m => new SelectListItem
            {
                Value = m.MovieId.ToString(),
                Text = m.Title
            }).ToList();

            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "FirstName", rentalHeader.CustomerId);

            return View(rentalHeader);
        }



        // GET: RentalHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var rentalHeader = await _context.RentalHeaders.FindAsync(id);
            if (rentalHeader == null)
            {
                return NotFound();
            }*/
            var rental = await _context.RentalHeaders
                        .Include(r => r.RentalDetails) // Ensure rental details are loaded
                        .ThenInclude(d => d.Movie)     // Ensure related movies are loaded
                        .FirstOrDefaultAsync(r => r.RentalHeaderId == id);
            ViewBag.RentalDetails = rental.RentalDetails ?? new List<RentalDetail>();

            // Ensure ViewBag.Movies is always initialized
            ViewBag.Movies = await _context.Movies.ToListAsync();

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", rental.CustomerId);
            return View(rental);
        }
       

        // POST: RentalHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalHeaderId,CustomerId,RentalDate,ReturnDate,MovieIds")] RentalHeader rentalHeader)
        {
            if (id != rentalHeader.RentalHeaderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentalHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalHeaderExists(rentalHeader.RentalHeaderId))
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

            ViewBag.Movies = _context.Movies.Select(m => new SelectListItem
            {
                Value = m.MovieId.ToString(),
                Text = m.Title
            }).ToList();
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", rentalHeader.CustomerId);
            return View(rentalHeader);
        }

        // GET: RentalHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalHeader = await _context.RentalHeaders
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RentalHeaderId == id);
            if (rentalHeader == null)
            {
                return NotFound();
            }

            return View(rentalHeader);
        }

        // POST: RentalHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalHeader = await _context.RentalHeaders.FindAsync(id);
            if (rentalHeader != null)
            {
                _context.RentalHeaders.Remove(rentalHeader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalHeaderExists(int id)
        {
            return _context.RentalHeaders.Any(e => e.RentalHeaderId == id);
        }
    }
}
