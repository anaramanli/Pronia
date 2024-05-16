using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;

namespace WebApplication1.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProniaContext _context;

        public ShopController(ProniaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 0)
        {
            double max = await _context.Products.CountAsync();
            ViewBag.MaxPage= Math.Ceiling((double) max / 2);
            ViewBag.CurrentPage = page + 1;
            var products = await _context.Products.Skip(2*page).Take(2).ToListAsync();
            return View(products);
        }
    }
}
