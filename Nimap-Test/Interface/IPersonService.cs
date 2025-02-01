using Nimap_Test.Models;

namespace Nimap_Test.Interface
{
    public interface IPersonService
    {
        public Task<bool> Create(Person p);

        public Task<bool> Edit(Person p);


    }
}
