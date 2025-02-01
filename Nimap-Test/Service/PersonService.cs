using Nimap_Test.Data;
using Nimap_Test.Interface;
using Nimap_Test.Models;

namespace Nimap_Test.Service
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Person p)
        {
            await _context.Peoples.AddAsync(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Person p)
        {
             _context.Peoples.Update(p);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
