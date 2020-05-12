using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW2.Data;
using HW2.Models;

namespace HW2.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UploadFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UploadFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.UploadFiles.ToListAsync());
        }

        // GET: UploadFiles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        // GET: UploadFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UploadFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GUID,Name,Path,Uri,Describe,UploadDate")] UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uploadFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uploadFile);
        }

        // GET: UploadFiles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles.FindAsync(id);
            if (uploadFile == null)
            {
                return NotFound();
            }
            return View(uploadFile);
        }

        // POST: UploadFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GUID,Name,Path,Uri,Describe,UploadDate")] UploadFile uploadFile)
        {
            if (id != uploadFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uploadFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UploadFileExists(uploadFile.Id))
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
            return View(uploadFile);
        }

        // GET: UploadFiles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        // POST: UploadFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var uploadFile = await _context.UploadFiles.FindAsync(id);
            _context.UploadFiles.Remove(uploadFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UploadFileExists(long id)
        {
            return _context.UploadFiles.Any(e => e.Id == id);
        }
    }
}
