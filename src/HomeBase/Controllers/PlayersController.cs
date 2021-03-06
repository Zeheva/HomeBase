using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeBase.Data;
using HomeBase.Models;



namespace HomeBase.Controllers
{
    public class PlayersController : Controller
    {
        private readonly HomeBaseContext _context;

        public PlayersController(HomeBaseContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, 
            string searchString, int? page)//issues with sorting need to fix
        {


            ViewData["CurrentSort"] = sortOrder; //seachfilter not working get or GET? maybe on index.cshml
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var players = from p in _context.Players select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                players = players.Where(p => p.LastName.Contains(searchString)
                || p.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    players = players.OrderByDescending(p => p.LastName);
                    break;
                case "Date":
                    players = players.OrderBy(p => p.EnrollmentDate);
                    break;
                case "date_desc":
                    players = players.OrderByDescending(p => p.EnrollmentDate);
                    break;
                default:
                    players = players.OrderBy(p => p.LastName);
                    break;
            }


            int pageSize = 10;
            return View(await PaginatedList<Player>.CreateAsync(players.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //added second line for work on details page
            var player = await _context.Players
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Team)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Address,Email,EnrollmentDate,Experience,FirstName,LastName,PhoneNumber,Position,TeamRequested")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(player);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
          
            }

            catch (DbUpdateException /* ex */)// fix to log error
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                   "Try again, and if the problem persists " + "see your system Admin.");
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.SingleOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerToUpdate = await _context.Players.SingleOrDefaultAsync(p => p.PlayerID == id);

            if (await TryUpdateModelAsync<Player>(playerToUpdate, "",
                p => p.FirstName, p => p.LastName, p => p.Email, p => p.PhoneNumber,
                p => p.Address, p => p.Position, p => p.Experience, p => p.TeamRequested, p => p.EnrollmentDate,
                p => p.Team))
            {
                try
                {
                    
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                
            }
            return View(playerToUpdate);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete Failed. Try again, if the problem persists " +
                    "you have failed at life and may not want to go on.";
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.PlayerID == id);
            if(player == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch(DbUpdateException /* ex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerID == id);
        }
    }
}
