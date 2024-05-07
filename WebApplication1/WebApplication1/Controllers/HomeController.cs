using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;
using WebApplication1.ViewModels.Categories;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProniaContext _context;

        public HomeController(ProniaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Categories
            .Where(x => x.IsDeleted == false)
            .Select(x => new GetCategoryVM
            {
                Id= x.Id,
                Name = x.Name,
            }).ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            var plant = await _context.Categories.FindAsync(id);
            if (plant == null) return NotFound();
            _context.Remove(plant);
            await _context.SaveChangesAsync();
            return Content(plant.Name);
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        public async Task<IActionResult> Shop()
        {
            return View();
        }
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
    }
}
