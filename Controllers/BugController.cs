using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BugTrackingSystem.Models;

public class BugController : Controller
{
    private readonly ApplicationDbContext _context;

    public BugController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Bug/Index
    public async Task<IActionResult> Index()
    {
        var bugs = await _context.Bugs.ToListAsync();
        return View(bugs);
    }

    // GET: Bug/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bug = await _context.Bugs.FirstOrDefaultAsync(m => m.Id == id);

        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    // GET: Bug/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Bug/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Summary,Description")] Bug bug)
    {
        if (ModelState.IsValid)
        {
            bug.CreatedDate = DateTime.Now;
            _context.Add(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(bug);
    }

    // GET: Bug/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bug = await _context.Bugs.FindAsync(id);

        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    // POST: Bug/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Summary,Description")] Bug bug)
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

    // GET: Bug/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bug = await _context.Bugs.FirstOrDefaultAsync(m => m.Id == id);

        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    // POST: Bug/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bug = await _context.Bugs.FindAsync(id);

        if (bug != null)
        {
            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool BugExists(int id)
    {
        return _context.Bugs.Any(e => e.Id == id);
    }
}
