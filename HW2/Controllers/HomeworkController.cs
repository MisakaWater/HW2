﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW2.Data;
using HW2.Models;
using Microsoft.AspNetCore.Authorization;
using HW2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HW2.Controllers
{
    [Authorize(Roles = "管理员,作业管理,答案管理")]
    public class HomeworkController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;
        private string _dir;
        private string _savePath;
        public HomeworkController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            _dir = _env.WebRootPath;
            _savePath = Path.Combine(_dir,"upload");
            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Homeworks.ToListAsync());
        }
        [Authorize(Roles = "管理员,作业管理")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "管理员,作业管理")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Homework homework)
        {
            if (ModelState.IsValid)
            {
                homework.GUID = Guid.NewGuid() + "";
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homework);
        }
        [Authorize(Roles = "管理员,作业管理")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            return View(homework);
        }
        [Authorize(Roles = "管理员,作业管理")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GUID,Course,WorkTitle,WorkContent,AnswerText,AnswerFiles,ReleaseDate,EndDate")] Homework homework)
        {
            if (id != homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.Id))
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
            return View(homework);
        }
        [Authorize(Roles = "管理员,答案管理")]
        public async Task<IActionResult> EditAnswer(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var homework = await _context.Homeworks.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            return View(homework);
        }
        [Authorize(Roles = "管理员,答案管理")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnswer(long? id, [Bind("Id,AnswerText")] Homework homework, List<IFormFile> formFiles)
        {
            if (id != homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var hw = await _context.Homeworks.FindAsync(id);
                    

                    var exts = new List<string>()
                    {
                        ".pdf",
                        ".jpg",
                        ".png",
                        ".py",
                        ".sql"
                    };
                    foreach (var file in formFiles)
                    {
                        var fileExt = Path.GetExtension(file.FileName);
                        if (!exts.Any(x => x.Contains(fileExt)))
                        {
                            break;
                        }
                        var guid = Guid.NewGuid()+ "";
                        var fileName = guid+fileExt;
                        var files = new UploadFile()
                        {
                            GUID = guid,
                            Name = guid,
                            Path = Path.Combine(_savePath, fileName),
                            UploadDate = DateTime.Now,
                        };
                        using (var fileStream = new FileStream(
                            Path.Combine(_savePath, fileName),
                            FileMode.Create, FileAccess.Write))
                        {
                            file.CopyTo(fileStream);
                        }
                        hw.AnswerFiles.Add(files);
                    }

                    hw.AnswerText = homework.AnswerText;
                    //_context.Attach(homework);
                    //_context.Entry(homework).Property(x => x.AnswerText).IsModified = true;
                    _context.Update(hw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.Id))
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
            return View(homework);
        }
        [Authorize(Roles = "管理员,作业管理")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }
        [Authorize(Roles = "管理员,作业管理")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(long id)
        {
            return _context.Homeworks.Any(e => e.Id == id);
        }
    }
}
