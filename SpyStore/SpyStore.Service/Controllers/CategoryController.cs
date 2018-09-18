using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels.Base;

namespace SpyStore.Service.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
		private ICategoryRepo Repo { get; set; }
		private IProductRepo ProductRepo { get; set; }

		public CategoryController(ICategoryRepo repo, IProductRepo productRepo)
		{
			Repo = repo;
			ProductRepo = productRepo;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(Repo.GetAll());
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var item = Repo.Find(id);
			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[HttpGet("{categoryId}/products")]
		public IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int categoryId)
		{
			return ProductRepo.GetProductsForCategory(categoryId);
		}
    }
}