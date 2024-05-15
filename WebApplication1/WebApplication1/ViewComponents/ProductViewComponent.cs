using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.DataAccessLayers;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents
{
	public class ProductViewComponent : ViewComponent
	{
		private readonly ProniaContext _context;

		public ProductViewComponent(ProniaContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int order = 1)
		{
			List<Product> products = null;

			switch (order)
			{
				case 1:
					products = await _context.Products.OrderBy(p => p.Name).Take(8).ToListAsync();
					break;
				case 2:
					products = await _context.Products.OrderByDescending(p => p.SellPrice).Take(8).ToListAsync();
					break;
				case 3:
					products = await _context.Products.OrderBy(p => p.CreatedTime).Take(8).ToListAsync();
					break;
			}

			return View(products);
		}
	}
}
