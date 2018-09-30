using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels.Base;

namespace SpyStore.Service.Controllers
{
	[Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
		private IProductRepo Repo { get; set; }

		public SearchController(IProductRepo repo)
		{
			Repo = repo;
		}

		[HttpGet("{searchString}", Name = "SearchProduct")]
		public IEnumerable<ProductAndCategoryBase> Search(string searchString)
		{
			return Repo.Search(searchString);
		}
    }
}