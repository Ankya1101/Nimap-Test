using Nimap_Test.Models;

namespace Nimap_Test.Interface
{
    public interface IProductService
    {
        public Task<bool> Create(Product p);

        public Task<bool> Edit(Product p);

        public Task<bool> Delete(int id);

    }
}
