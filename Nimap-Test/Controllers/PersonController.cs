using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Nimap_Test.Data;
using Nimap_Test.Interface;
using Nimap_Test.Models;
using System.Diagnostics.Contracts;

namespace Nimap_Test.Controllers
{
    public class PersonController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IPersonService _service;

        public PersonController(ApplicationDbContext context,IPersonService personService)
        {
            _context = context;
            _service = personService;

        }
        public async Task<IActionResult> Index()
        {
            var person = await _context.Peoples.ToListAsync();
            return View(person);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {

            if(ModelState.IsValid)
            {
                bool val = await _service.Create(person);
                if(val)
                {
                    ViewBag.Message = "Data Inserted Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(person); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _context.Peoples.FindAsync(id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Person p)
        {
            if(ModelState.IsValid)
            {
                bool val = await _service.Edit(p);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.Peoples.FindAsync(id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Person p)
        {
            _context.Peoples.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
