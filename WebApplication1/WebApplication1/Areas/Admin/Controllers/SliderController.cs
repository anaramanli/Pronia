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
			var data =await _context.Sliders.Select(s => new GetSliderVM
			{
				Discount = s.Discount,
				Title = s.Title,
				ImageUrl = s.ImageUrl,
				Subtitle = s.Subtitle,
				Id = s.Id
				
			}).ToListAsync();	
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
	}
}
