using Microsoft.AspNetCore.Mvc;

namespace VegetableShop.WebApp.Controllers
{
	public class VegetableController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
