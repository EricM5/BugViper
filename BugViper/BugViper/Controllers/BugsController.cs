using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugViper.Data;
using BugViper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace BugViper.Controllers
{
    public class BugsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BugsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bugs
        public async Task<IActionResult> Index()
        {
              return _context.Bug != null ?
                            View("Index", await _context.Bug.Where(obj => (obj.PostedBy.Equals(_userManager.GetUserId(HttpContext.User)))).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bug'  is null.");
        }
        //GET: Bugs/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResults(string subject, int priority, DateTime dateposted, DateTime duedate, string postedby)
        {
            return View("Index", await _context.Bug.Where(obj => (subject != null && obj.Subject.Contains(subject)) | (postedby != null && obj.PostedBy.Contains(postedby))).ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bug == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bugs/Create
        [Authorize]
        public IActionResult Create()
        {
            //ViewBag.userEmail = _userManager.(HttpContext.User);
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Priority,DatePosted,DueDate,ExpectedHours,PostedBy")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                bug.PostedBy = _userManager.GetUserId(HttpContext.User);
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bug == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Priority,DatePosted,DueDate,ExpectedHours,PostedBy")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
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
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bug == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bug == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bug'  is null.");
            }
            var bug = await _context.Bug.FindAsync(id);
            if (bug != null)
            {
                _context.Bug.Remove(bug);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
          return (_context.Bug?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
