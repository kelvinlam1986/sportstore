using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels.Base;

namespace SpyStore.Service.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
		private IProductRepo Repo { get; set; }

		public ProductController(IProductRepo repo)
		{
			Repo = repo;
		}

		[HttpGet]
		public IEnumerable<ProductAndCategoryBase> Get()
		{
			return Repo.GetAllWithCategoryName().ToList();
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var item = Repo.GetOneWithCategoryName(id);
			if (item == null)
			{
				return NotFound();
			}

			return new ObjectResult(item);
		}

		[HttpGet("featured")]
		public IEnumerable<ProductAndCategoryBase> GetFeatured()
		{
			return Repo.GetFeaturedWithCategoryName().ToList();
		}
    }
}