using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccessLayers;

namespace WebApplication1.ViewComponents
{
	public class FooterViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ProniaContext _context)
		{
			return View();
		}
	}
}
