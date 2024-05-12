using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using WebApplication1.DataAccessLayers;
using WebApplication1.Extentions;
using WebApplication1.ViewModels.Products;

namespace WebApplication1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController(ProniaContext _context, IWebHostEnvironment _env) : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View(await _context.Products
				.Select(p => new GetProductAdminVM
				{
					CostPrice = p.CostPrice,
					Discount = p.Discount,
					Id = p.Id,
					ImageUrl = p.ImageUrl,
					Name = p.Name,
					Rating = p.Rating,
					SellPrice = p.SellPrice,
					StockCount = p.StockCount,

				}).ToListAsync());

		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateProductVM data)
		{
			if (data.ImageFile.IsValidType("image"))
			{
				ModelState.AddModelError("ImageFile", "File Must be Image formated");
			}
			if (data.ImageFile.IsValidLength(200))
			{
				ModelState.AddModelError("ImageFile", "Image cannot bigger than 200mb");
			}
			if (!ModelState.IsValid)
				return View(data);
			return View(data);

			string fileName = await data.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath,"imgs", "products"));
			await _context.Products.AddAsync(new Models.Product
			{
				CostPrice = data.CostPrice,
				CreatedTime = DateTime.Now,
				Discount = data.Discount,
				ImageUrl = Path.Combine("imgs", "products", fileName),
				IsDeleted = false,
				Name = data.Name,
				Rating = data.Rating,
				SellPrice = data.SellPrice,
				StockCount = data.StockCount,

			});
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

	}
}
