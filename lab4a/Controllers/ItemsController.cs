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
            ViewData["id"] = UserId;
            ViewData["items"] = await _dao.Read(UserId);
            return View();
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
                await _dao.Create(item);
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
                    var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await _dao.Get(item.Id);
                    item.Done = !item.Done;
                    await _dao.Update(item);
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
            await _dao.Get(id);
            await _dao.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
