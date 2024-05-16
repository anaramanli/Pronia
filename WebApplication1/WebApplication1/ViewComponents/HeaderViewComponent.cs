using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccessLayers;

namespace WebApplication1.ViewComponents
{
	public class HeaderViewComponent(ProniaContext _context) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}	
}

