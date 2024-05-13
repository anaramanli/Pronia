using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccessLayers;
using WebApplication1.Models;
using WebApplication1.ViewModels.Sliders;

namespace WebApplication1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SliderController(ProniaContext _context) : Controller
	{
		public async Task<IActionResult> Index()
		{
			IEnumerable<GetSliderVM> data = await _context.Sliders.Select(s => new GetSliderVM
			{
				Discount = s.Discount,
				Title = s.Title,
				ImageUrl = s.ImageUrl,
				Subtitle = s.Subtitle,
				Id = s.Id

			}).ToArrayAsync();
			return View(data ?? new List<GetSliderVM>());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateSliderVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			Slider slider = new Slider
			{
				Discount = vm.Discount,
				CreatedTime = DateTime.Now,
				ImageUrl = vm.ImageUrl,
				IsDeleted = false,
				Subtitle = vm.Subtitle,
				Title = vm.Title,
			};
			await _context.Sliders.AddAsync(slider);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null || id < 1) BadRequest();
			Slider? slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
			if (slider == null) return NotFound();
			UpdateSliderVM updateSliderVM = new UpdateSliderVM
			{
				Discount = slider.Discount,
				Title = slider.Title,
				ImageUrl = slider.ImageUrl,
				Subtitle = slider.Subtitle
			};
			return View(updateSliderVM);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int? id, UpdateSliderVM sliderVM)
		{
			if (id == null || id < 1) BadRequest();
			Slider existed = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
			if (existed == null) return NotFound();
			existed.Title = sliderVM.Title;
			existed.Subtitle = sliderVM.Subtitle;
			existed.ImageUrl = sliderVM.ImageUrl;
			existed.Discount = sliderVM.Discount;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id, Slider sliderToDelete)
		{
			if (id == null || id < 1) BadRequest();
			var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
			if (slider == null) return NotFound();
			_context.Sliders.Remove(slider);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
