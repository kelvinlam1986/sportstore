using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;

namespace SpyStore.Service.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
		private ICustomerRepo Repo { get; set; }

		public CustomerController(ICustomerRepo repo)
		{
			Repo = repo;
		}

		[HttpGet]
		public IEnumerable<Customer> Get()
		{
			return Repo.GetAll();
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var item = Repo.Find(id);
			if (item == null)
			{
				return NotFound();
			}

			return new ObjectResult(item);
		}
    }
}