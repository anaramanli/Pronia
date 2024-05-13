using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;
using WebApplication1.Models;
using WebApplication1.ViewModels.Categories;
using WebApplication1.ViewModels.Sliders;

namespace WebApplication1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController(ProniaContext _sql) : Controller
	{
		// GET: HomeController
		public async Task<ActionResult> Index()
		{
			return View(await _sql.Categories.Select(c => new GetCategoryVM
			{
				Id=c.Id,
				Name=c.Name,

			}).ToListAsync());
		}

		

		// GET: HomeController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: HomeController/Create
		[HttpPost]
		
		public async Task<ActionResult> Create(CreateCategoryVM vM)
		{
			if (vM.Name != null && await _sql.Categories.AnyAsync(c=>c.Name == vM.Name))
			{
				ModelState.AddModelError("Name", "Name already has in database");
			}
			if (!ModelState.IsValid)
			{
				return View(vM);
			}
			await _sql.Categories.AddAsync(new Models.Category
			{
				CreatedTime = DateTime.Now,
				Name = vM.Name,
				IsDeleted = false
			});
			await _sql.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || id < 1) BadRequest();
			Category? category = await _sql.Categories.FirstOrDefaultAsync(s => s.Id == id);
			if (category == null) return NotFound();
			UpdateCategoryVM updateCategoryVM = new UpdateCategoryVM
			{
				Name = category.Name,
				
			};
			return View(updateCategoryVM);
		}
		[HttpPost]

		public async Task<ActionResult> Edit(int? id, UpdateCategoryVM categoryVM)
		{
			if (id == null || id < 1) BadRequest();
			Category existed = await _sql.Categories.FirstOrDefaultAsync(s => s.Id == id);
			if (existed == null) return NotFound();
			existed.Name = categoryVM.Name;
			await _sql.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}



		// GET: HomeController/Delete/5
		public async Task<IActionResult> Delete(int? id, Category categoryToDelete)
		{
			if (id == null || id < 1) BadRequest();
			var category = await _sql.Categories.FirstOrDefaultAsync(s => s.Id == id);
			if (category == null) return NotFound();
			_sql.Categories.Remove(category);
			await _sql.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		// POST: HomeController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
