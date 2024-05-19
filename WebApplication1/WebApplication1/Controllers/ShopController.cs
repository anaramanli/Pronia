using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProniaContext _context;

        public ShopController(ProniaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 0, int? categoryId=null)
        {
            IQueryable<Product> query = _context.Products.Include(p=> p.ProductCategories);


            if (categoryId != null)
            {
                query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));
            }
            double max = query.Count();

            ViewBag.CurrentCategory = categoryId;
            ViewBag.MaxPage = Math.Ceiling((double)max / 2);
            ViewBag.CurrentPage = page + 1;

            query = query.Skip(2 * page).Take(2);

            ViewBag.Categories = await _context.Categories.Include(c => c.ProductCategories).ToListAsync();
            
           
            return View(await query.ToListAsync());
        }
    }
}
