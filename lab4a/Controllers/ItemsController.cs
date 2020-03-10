using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4a.Data;
using lab4a.Models;
using lab4a.Data.Dao;
using System.Security.Claims;

namespace lab4a.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemDao _dao;
        public ItemsController(IAtlasSettings settings)
        {
            _dao = new ItemDao(settings);
        }

        // GET: Items
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            ViewData["list"] = await _dao.Item.ToListAsync();
            ViewData["id"] = UserId;
            return View();
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Text,Date")] Item item)
        {
            if (ModelState.IsValid)
            {
                _dao.Add(item);
                await _dao.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,Text,Done,Date")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dao.Update(item);
                    item.Done = !item.Done;
                    await _dao.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var item = await _dao.Item.FindAsync(id);
            _dao.Item.Remove(item);
            await _dao.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
