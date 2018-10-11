using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenir.Data;
using QualitySouvenir.Models;
using System.Net.Http.Headers; //Week 6
using Microsoft.AspNetCore.Hosting; //Week 6
using Microsoft.AspNetCore.Http; //Week 6
using System.IO; //Week 6
using Microsoft.AspNetCore.Authorization;


namespace QualitySouvenir.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SouvenirsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;//week 6

        public SouvenirsController(ApplicationDbContext context, IHostingEnvironment hEnv)
        {
            _context = context;
            _hostingEnv = hEnv;//week 6
        }

        // GET: Souvenirs
        public async Task<IActionResult> Index(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? page)
        {
            //week3 create sorter
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CategorySortParm"] = String.IsNullOrEmpty(sortOrder) ? "category" : "";
            ViewData["SupplierSortParm"] = String.IsNullOrEmpty(sortOrder) ? "supplier" : "";

            //week3 add paging
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //week3 add search 
            ViewData["CurrentFilter"] = searchString;
            var applicationDbContext = _context.Souvenirs.Include(s => s.Category).Include(s => s.Supplier);
            var souvenirs = from s in applicationDbContext
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                souvenirs = souvenirs.Where(s => s.Name.Contains(searchString)
                                           || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    souvenirs = souvenirs.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.Price);
                    break;
                case "category":
                    souvenirs = souvenirs.OrderBy(s => s.Category);
                    break;
                case "supplier":
                    souvenirs = souvenirs.OrderBy(s => s.Supplier);
                    break;
                default:
                    souvenirs = souvenirs.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Souvenir>.CreatAsync(souvenirs.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Souvenirs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .Include(s => s.Category)
                .Include(s => s.Supplier)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // GET: Souvenirs/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
            return View();
        }

        // POST: Souvenirs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,Description,SupplierID,CategoryID,Image")] Souvenir souvenir, IList<IFormFile> _files)
        {
            var relativeName = "";
            var fileName = "";

            if (_files.Count < 1)
            {
                relativeName = "/images/Default.jpg";
            }
            else
            {
                foreach (var file in _files)
                {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    relativeName = "/images/products/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            souvenir.Image = relativeName;
            //Week 2
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(souvenir);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //log the error
                ModelState.AddModelError("", "Unable to save changes." + 
                    "Try again, and if the problem persists " + 
                    "see your system administrator");
            }

            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", souvenir.SupplierID);
            return View(souvenir);
        }

        // GET: Souvenirs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var souvenir = await _context.Souvenirs.SingleOrDefaultAsync(m => m.ID == id);
            if (id == null)
            {
                return NotFound();
            }

            if (souvenir == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", souvenir.SupplierID);
            return View(souvenir);
        }

        // POST: Souvenirs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile _files)
        {
            var relativeName = "";
            var fileName = "";
            var souvenir = await _context.Souvenirs.SingleOrDefaultAsync(s => s.ID == id);
            if (_files != null)
            {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(_files.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    relativeName = "/images/products/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await _files.CopyToAsync(fs);
                        fs.Flush();
                    }

            }
            else
            {                
                relativeName = souvenir.Image;
            }
            
            souvenir.Image = relativeName;

            if (id != souvenir.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(souvenir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SouvenirExists(souvenir.ID))
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
            //var souvenirToUpdate = await _context.Souvenirs.SingleOrDefaultAsync(s => s.ID == id);
            //if (await TryUpdateModelAsync<Souvenir>(
            //    souvenirToUpdate,
            //    "",
            //    s => s.Name, s => s.Price, s => s.Description, s => s.SupplierID, s => s.CategoryID, s => s.Image))
            //{
            //    try
            //    {
            //        _context.Update(souvenir);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    catch (DbUpdateException /* ex */)
            //    {
            //        ModelState.AddModelError("", "Unable to save changes." + "Try again, and if the problem persists " + "see your system administrator");
            //    }
            //}
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", souvenir.SupplierID);
            return View(souvenir);
        }

        // GET: Souvenirs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .AsNoTracking()//week 2
                .SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())//week 2
            {
                ViewData["ErrorMeassage"] = "Delete faild. Try again, and if the problem persists " + "see your system administrator";
            }

            return View(souvenir);
        }

        // POST: Souvenirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var souvenir = await _context.Souvenirs
                .AsNoTracking()//week 2
                .SingleOrDefaultAsync(m => m.ID == id);
            _context.Souvenirs.Remove(souvenir);
            if (souvenir == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Souvenirs.Remove(souvenir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //log the error
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool SouvenirExists(int id)
        {
            return _context.Souvenirs.Any(e => e.ID == id);
        }
    }
}
