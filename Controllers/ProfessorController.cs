using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE_Test.Models;

namespace PE_Test.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly WebContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProfessorController(WebContext context, IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            _context = context;
        }

        // GET: Professor
        public async Task<IActionResult> Index()
        {
            var webContext = _context.Professor.Include(p => p.Department);
            return View(await webContext.ToListAsync());
        }

        // GET: Professor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.professorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professor/Create
        public IActionResult Create()
        {
            ViewData["departmentId"] = new SelectList(_context.Department, "departmentId", "departmentName");
            return View();
        }

        // POST: Professor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("professorId,professorName,age,salary,email,birthdate,photo,married,address,departmentId")] Professor professor, IFormFile hinhanh)
        {
            if (hinhanh == null || hinhanh.Length == 0)
            {
                return Content("please select file");
            }
            if (ModelState.IsValid)
            {
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", hinhanh.FileName);
                var stream = new FileStream(path, FileMode.Create);
                _ = hinhanh.CopyToAsync(stream);
                professor.photo = hinhanh.FileName;
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["departmentId"] = new SelectList(_context.Department, "departmentId", "departmentName", professor.departmentId);
            return View(professor);
        }

        // GET: Professor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            ViewData["departmentId"] = new SelectList(_context.Department, "departmentId", "departmentName", professor.departmentId);
            return View(professor);
        }

        // POST: Professor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("professorId,professorName,age,salary,email,birthdate,photo,married,address,departmentId")] Professor professor , IFormFile hinhanh)
        {
            if (id != professor.professorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hinhanh != null || hinhanh.Length != 0)
                    {
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "images", hinhanh.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        _ = hinhanh.CopyToAsync(stream);
                        professor.photo = hinhanh.FileName;
                    }
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.professorId))
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
            ViewData["departmentId"] = new SelectList(_context.Department, "departmentId", "departmentName", professor.departmentId);
            return View(professor);
        }

        // GET: Professor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.professorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.professorId == id);
        }
    }
}
