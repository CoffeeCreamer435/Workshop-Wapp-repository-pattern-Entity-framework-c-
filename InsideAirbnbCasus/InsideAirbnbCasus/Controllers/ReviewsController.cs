using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsideAirbnbCasus.Models;

namespace InsideAirbnbCasus.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AIRBNBContext _context;

        public ReviewsController(AIRBNBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = _context.Reviews
                .Include(r => r.Listing)
                .Take(20);

            return View(await result.ToListAsync());
        }

        public async Task<IActionResult> SingleProperty()
        {
            var result = _context.Reviews
                .Select(r => r.ReviewerName)
                .Take(20);

            return View(await result.ToListAsync());
        }

        public async Task<IActionResult> TransformData()
        {
            string[] types = { "Robot", "Human" };
            var rand = new Random();

            var result = _context.Reviews
                .Select(r => new Person(r.ReviewerName, types[rand.Next(2)]))
                .Take(20);

            return View(await result.ToListAsync());
        }

        public async Task<IActionResult> SingleRelatedProperty()
        {
            var result = _context.Reviews
                .Select(r => r.Listing)
                .Select(l => l.Reviews)
                .Take(20);

            return View("Index", await result.ToListAsync());
        }

        //public async Task<IActionResult> SingleRelatedProperty()
        //{
        //    var result = _context.Reviews
        //        .Include(r => r.Listing)
        //            .ThenInclude(l => l.Reviews)
        //        .Take(20);

        //    return View("Index", await result.ToListAsync());
        //}

        public async Task<IActionResult> FilteringName([FromQuery(Name = "name")] string name)
        {
            var result = _context.Reviews
                .Where(r => r.ReviewerName.Equals(name))
                .Take(20);

            return View("Index", await result.ToListAsync());
        }

        public async Task<IActionResult> OrderName()
        {
            var result = _context.Reviews
                .Where(r => !r.ReviewerName.Contains("(EMAIL HIDDEN)"))
                .OrderBy(r => r.ReviewerName) // OrderByDescending
                .Take(20);

            return View("Index", await result.ToListAsync());
        }

        public async Task<IActionResult> Join()
        {
            var result = _context.Listings.Join(
                _context.SummaryReviews,
                listings => listings.Id,
                sumRev => sumRev.ListingId,
                (listings, sumRev) => new TimeLine(
                    listings.Neighbourhood,
                    sumRev.Date.ToLocalTime()
                )
            )
                .Take(10);

            /*
             * <entity_enumer_1>.Join(
             *      <entity_enumer_2>,
             *      <select_related_property_1>,
             *      <select_related_property_2>,
             *      (entity_1, entity_2) => new {
             *          data = entiity_1.property,
             *          data2 = entiity_2.property2
             *      }
             * )
             */

            return View("Join", result.ToList());
        }

        public async Task<IActionResult> Raw()
        {
            var result = _context.Reviews.FromSqlRaw("SELECT TOP 10 * FROM Reviews Where ID != 1");
            return View("Index", await result.ToListAsync());
        }


        // //////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Listing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        public IActionResult Create()
        {
            ViewData["ListingId"] = new SelectList(_context.Listings.Take(20), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,Id,ReviewerId,ReviewerName,Comments")] Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                reviews.Date = DateTime.Now;
                reviews.ReviewerId = 84663259;
                reviews.ListingId = 137026;
                _context.Add(reviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ListingId"] = new SelectList(_context.Reviews, "ListingId", "Access", reviews.ListingId);
            return View(reviews);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            ViewData["ListingId"] = new SelectList(_context.Listings.Take(20), "Id", "Id", reviews.ListingId);
            return View(reviews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingId,Id,Date,ReviewerId,ReviewerName,Comments")] Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.Id))
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
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Access", reviews.ListingId);
            return View(reviews);
        }


        public async Task<IActionResult> EditName(int id)
        {
            var review = _context.Reviews
                .Where(r => r.Id == id)
                .FirstOrDefault();

            review.ReviewerName = "ABC";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Listing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);

            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
