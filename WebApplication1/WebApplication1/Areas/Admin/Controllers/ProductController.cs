using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplication1.DataAccessLayers;
using WebApplication1.Extentions;
using WebApplication1.Models;
using WebApplication1.ViewModels.Products;

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
                StockCount = p.StockCount
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
            if (!data.ImageFile.IsValidLength(300))
                ModelState.AddModelError("ImageFile", "File size must be lower than 3mb.");
        }
        bool isImageValid = true;
        StringBuilder sb = new StringBuilder();
        foreach (var img in data.ImageFiles)
        {
            if (!img.IsValidType("image"))
            {
                sb.Append("-" + img.FileName + " File must be img content.");
                isImageValid = false;
            }
            if (!img.IsValidLength(300))
            {
                sb.Append("-" + img.FileName + " File size must be lower than 3mb.");
                isImageValid = false;
            }
        }
        if (!isImageValid)
        {
            ModelState.AddModelError("ImageFiles", sb.ToString());
        }
        if (!ModelState.IsValid)
            return View(data);
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
            Images = new List<ProductImage>()
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
            prod.Images.Add(new ProductImage
            {
                ImageUrl = Path.Combine("imgs", "products", imgName),
                CreatedTime = DateTime.Now,
                IsDeleted = false,
            });
        }
        await _context.Products.AddAsync(prod);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}