using Microsoft.EntityFrameworkCore;
using Nimap_Test.Data;
using Nimap_Test.Interface;
using Nimap_Test.Models;

namespace Nimap_Test.Service
{
    public class ProductService : IProductService
    {
        private ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Product p)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(prod => prod.ProductName == p.ProductName && prod.CategoryId == p.CategoryId);

            if (existingProduct != null)
            {
                return false; 
            }
            _context.Products.Add(p);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var person = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            _context.Products.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Product p)
        {
            _context.Products.Update(p);
            _context.SaveChanges();
            return true;
        }
    }
}
