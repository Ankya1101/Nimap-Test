using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using Nimap_Test.Data;
using Nimap_Test.Interface;
using Nimap_Test.Models;

namespace Nimap_Test.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _service;

        public ProductController(ApplicationDbContext context,IProductService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
  
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            var product = await _context.Products.Include(c => c.Category).ToListAsync();

            var totalProducts = await _context.Products.CountAsync();


            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            if (pageNumber > totalPages) pageNumber = totalPages;


            var offset = (pageNumber - 1) * pageSize;


            var products = await _context.Products
                .Include(c => c.Category)
                .Skip(offset)  
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedProductViewModel
            {
                Products = products,
                TotalProducts = totalProducts,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

   
            ViewBag.Message = TempData["Message"];

            return View(model);

            //var product = await _context.Products.Include(c => c.Category).ToListAsync();
            //ViewBag.Message = TempData["Message"];
            //return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,CategoryId")] Product product)
        {
            if ((ModelState.IsValid) ||( !(string.IsNullOrEmpty(product.ProductName) && product.CategoryId > 0)))
            {
                bool check = await _service.Create(product);
                if(check)
                {
                    TempData["Message"] = "Product created successfully!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Product is already available";
                return View();
            }

            //ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if(ModelState.IsValid ||( !(string.IsNullOrEmpty(p.ProductName) && p.CategoryId > 0)))
            {
                await _service.Edit(p);
                TempData["Message"] = "Product is updated Succefully";
                return RedirectToAction("Index");
            } 
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id);
            //ViewBag.Categoty = product.Category.CategoryName;
            return View(product);
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTest(int id)
        {
            await _service.Delete(id);
            TempData["Message"] = "Product delelete Succesully";
            return RedirectToAction("Index");
        }

    }
}
