using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CatalogController : ControllerBase
	{
		public CatalogController() { }

		[HttpGet("[action]")]
		public IActionResult CatalogTypes()
		{

		}

		[HttpGet("[action]")]
		public IActionResult CatalogBrands()
		{

		}

		[HttpGet("[action]")]
		public IActionResult Items()
		{

		}
	}
}
