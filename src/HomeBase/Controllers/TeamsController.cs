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
    public class TeamsController : Controller
    {
        private readonly HomeBaseContext _context;

        public TeamsController(HomeBaseContext context)
        {
            _context = context;    
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var homeBaseContext = _context.Teams
                .Include(t => t.Captain)
                .Include(t => t.Players)
               
                
                .AsNoTracking();
            return View(await homeBaseContext.ToListAsync());
            //check related data
            //var teams = _context.Teams
               // .Include(t => t.)
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["CaptainID"] = new SelectList(_context.Captains, "CaptainID", "CaptainID");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,CaptainID,TeamName")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CaptainID"] = new SelectList(_context.Captains, "CaptainID", "CaptainID", team.CaptainID);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["CaptainID"] = new SelectList(_context.Captains, "CaptainID", "CaptainID", team.CaptainID);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamID,CaptainID,TeamName")] Team team)
        {
            if (id != team.TeamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CaptainID"] = new SelectList(_context.Captains, "CaptainID", "CaptainID", team.CaptainID);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.SingleOrDefaultAsync(m => m.TeamID == id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamID == id);
        }
    }
}
