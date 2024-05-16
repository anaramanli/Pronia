using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplication1.DataAccessLayers;
using WebApplication1.Extentions;
using WebApplication1.Models;
using WebApplication1.ViewModels.Products;
using WebApplication1.ViewModels.Sliders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pronia.Areas.Admin.Controllers;
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
				Categories = p.ProductCategories.Select(pc => pc.Category.Name).Bind(','),
				CreatedTime = p.CreatedTime.ToString("dd MM yyyy"),
				UpdatedTime = p.UpdatedTime.Year > 1 ? p.UpdatedTime.ToString("dd MM yyyy") : "-"
			})
			.ToListAsync());
	}
	public async Task<IActionResult> Create()
	{
		ViewBag.Categories = await _context.Categories
			.Where(s => !s.IsDeleted)
			.ToListAsync();
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateProductVM data)
	{
		if (data.ImageFile != null)
		{
			if (!data.ImageFile.IsValidType("image"))
				ModelState.AddModelError("ImageFile", "File must be img content.");
			if (!data.ImageFile.IsValidLength(2000))
				ModelState.AddModelError("ImageFile", "File size must be lower than 3mb.");
		}
		bool isImageValid = true;
		StringBuilder sb = new StringBuilder();
		foreach (var img in data.ImageFiles ?? new List<IFormFile>())
		{
			if (!img.IsValidType("image"))
			{
				sb.Append("-" + img.FileName + " File must be img content.");
				isImageValid = false;
			}
			if (!img.IsValidLength(8000))
			{
				sb.Append("-" + img.FileName + " File size must be lower than 3mb.");
				isImageValid = false;
			}
		}
		if (!isImageValid)
		{
			ModelState.AddModelError("ImageFiles", sb.ToString());
		}
		if (await _context.Categories.CountAsync(c => data.CategoryIds.Contains(c.Id)) != data.CategoryIds.Length)
			ModelState.AddModelError("CategoryIds", "Category Not Found");

		if (!ModelState.IsValid)
		{
			ViewBag.Categories = await _context.Categories
			.Where(s => !s.IsDeleted)
			.ToListAsync();
			return View(data);
		}
		string fileName = await data.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
		Product prod = new Product
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
			Images = new List<ProductImage>(),
			//------Many to Many ---------->
			ProductCategories = data.CategoryIds.Select(x => new
			ProductCategory
			{
				CategoryId = x
			}).ToList()
		};
		foreach (var img in data.ImageFiles)
		{
			string imgName = await img.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
			//await _context.ProductImages.AddAsync(new ProductImage
			//{
			//    ImageUrl = Path.Combine("imgs", "products", imgName),
			//    CreatedTime = DateTime.Now,
			//    IsDeleted = false,
			//    Product = prod
			//});
			//-------One To Many --------->
			prod.Images.Add(new ProductImage
			{
				ImageUrl = Path.Combine("imgs", "products", imgName),
				CreatedTime = DateTime.Now,
				IsDeleted = false,
			});
			//----- One To Many End ------->

		}
		await _context.Products.AddAsync(prod);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
	[HttpGet]
	public async Task<IActionResult> Update(int? id)
	{
		if (id == null || id < 1) BadRequest();
		Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
		if (product == null) return NotFound();
		UpdatedProductVM updatedProduct = new UpdatedProductVM
		{
			Name = product.Name,
			CostPrice = product.CostPrice,
			SellPrice = product.SellPrice,
			Rating = product.Rating,
			StockCount = product.StockCount,
			Discount = product.Discount,

		};
		return View(updatedProduct);
	}
	[HttpPost]
	public async Task<IActionResult> Update(int id, UpdatedProductVM updatedProduct)
	{
		//      var existed = await _context.Products.FindAsync(id);

		//      if (existed == null) return NotFound();

		//      existed.Name = updatedProduct.Name;
		//      existed.SellPrice = updatedProduct.SellPrice;
		//      existed.CostPrice = updatedProduct.CostPrice;
		//      existed.Rating = updatedProduct.Rating;
		//      existed.StockCount = updatedProduct.StockCount;
		//      foreach (var img in existed.Images)
		//      {

		//      }
		//await _context.Products.AddAsync(prod)
		string fileName = await updatedProduct.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
		Product prod = new Product
		{

			CostPrice = updatedProduct.CostPrice,
			CreatedTime = DateTime.Now,
			Discount = updatedProduct.Discount,
			ImageUrl = Path.Combine("imgs", "products", fileName),
			IsDeleted = false,
			Name = updatedProduct.Name,
			Rating = updatedProduct.Rating,
			SellPrice = updatedProduct.SellPrice,
			StockCount = updatedProduct.StockCount,
			Images = new List<ProductImage>(),
			//------Many to Many ---------->
			ProductCategories = updatedProduct.CategoryIds.Select(x => new
			ProductCategory
			{
				CategoryId = x
			}).ToList()
		};
		foreach (var img in updatedProduct.ImageFiles)
		{
			string imgName = await img.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
			//await _context.ProductImages.AddAsync(new ProductImage
			//{
			//    ImageUrl = Path.Combine("imgs", "products", imgName),
			//    CreatedTime = DateTime.Now,
			//    IsDeleted = false,
			//    Product = prod
			//});
			//-------One To Many --------->
			prod.Images.Add(new ProductImage
			{
				ImageUrl = Path.Combine("imgs", "products", imgName),
				CreatedTime = DateTime.Now,
				IsDeleted = false,
			});
			//----- One To Many End ------->

		}
		await _context.Products.AddAsync(prod);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Delete(int? id, Product productToDelete)
	{
		if (id == null || id < 1) BadRequest();
		var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
		if (product == null) return NotFound();
		_context.Products.Remove(product);
		await _context.SaveChangesAsync();
		return RedirectToAction("Index");
	}
}