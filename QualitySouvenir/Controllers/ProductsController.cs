using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenir.Data;
using QualitySouvenir.Models;
using Microsoft.AspNetCore.Authorization;

namespace QualitySouvenir.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Admin, Member")]
    public class ProductsController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(         
            string sortOrder,           
            string searchString,
            string minPrice,
            string maxPrice,
            int? page,
            int? categoryId)
        {
            ViewData["SortParm"] = sortOrder;
            ViewData["NameSortParm"] =String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["SearchFilter"] = searchString;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            var applicationDbContext = _context.Souvenirs;
            var souvenirs = from s in applicationDbContext
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                souvenirs = souvenirs.Where(s => s.Name.Contains(searchString)
                                           || s.Description.Contains(searchString) 
                                           || s.Category.Description.Contains(searchString)
                                           || s.Category.Name.Contains(searchString));
            }
          
            if (!(String.IsNullOrEmpty(minPrice)))
            {
                souvenirs = souvenirs.Where(s => s.Price >= Convert.ToDecimal(minPrice));
            }

            if (!(String.IsNullOrEmpty(maxPrice)))
            {
                souvenirs = souvenirs.Where(s => s.Price <= Convert.ToDecimal(maxPrice));
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
                default:
                    souvenirs = souvenirs.OrderBy(s => s.Name);
                    break;
            }
            
            int pageSize = 8;

            if (categoryId != null)
            {
                souvenirs = souvenirs.Where(c => c.CategoryID == categoryId);            
            }
            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
            ViewData["categoryId"] = categoryId;

            return View(await PaginatedList<Souvenir>.CreatAsync(souvenirs.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }
    }
}
