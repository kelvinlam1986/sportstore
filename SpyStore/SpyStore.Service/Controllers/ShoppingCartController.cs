using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.Service.Controllers
{
	[Route("api/[controller]/{cutomerId}")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
		private IShoppingCartRepo Repo { get; set; }

		public ShoppingCartController(IShoppingCartRepo repo)
		{
			Repo = repo;
		}

		[HttpGet("{productId}")]
		public CartRecordWithProductInfo GetShoppingCartRecord(int customerId, int productId)
		{
			return Repo.GetShoppingCartRecord(customerId, productId);
		}

		[HttpGet(Name = "GetShoppingCart")]
		public IEnumerable<CartRecordWithProductInfo> GetShoppingCart(int customerId)
		{
			return Repo.GetShoppingCartRecords(customerId);
		}

		[HttpPost]
		public IActionResult Create(int customerId, [FromBody] ShoppingCartRecord item)
		{
			if (item == null || !ModelState.IsValid)
			{
				return BadRequest();
			}

			item.DateCreated = DateTime.Now;
			item.CustomerId = customerId;
			Repo.Add(item);
			return CreatedAtRoute("GetShoppingCart", new { controler = "ShoppingCart", customerId = customerId });
		}


		[HttpPut("{shoppingCartRecordId}")]
		public IActionResult Update(int customerId, int shoppingCartRecordId, [FromBody] ShoppingCartRecord item)
		{
			if (item == null || item.Id != shoppingCartRecordId || !ModelState.IsValid)
			{
				return BadRequest();
			}

			item.DateCreated = DateTime.Now;
			Repo.Update(item);

			return CreatedAtRoute("GetShoppingCart", new { customerId = customerId });
		}

		[HttpDelete("{shoppingCardRecordId}")]
		public IActionResult Delete(int customerId, int shoppingCartRecordId, string timestamp)
		{
			if (!timestamp.StartsWith("\""))
			{
				timestamp = $"\"{timestamp}\"";
			}

			var ts = JsonConvert.DeserializeObject<byte[]>(timestamp);
			Repo.Delete(shoppingCartRecordId, ts);
			return NoContent();
		}

		public IActionResult Purchase(int customerId, [FromBody] Customer customer)
		{
			if (customer == null || customer.Id != customerId || !ModelState.IsValid)
			{
				return BadRequest();
			}

			int orderId;
			orderId = Repo.Purchase(customerId);
			return CreatedAtRoute("GetOrderDetails", routeValues: new { customerId = customerId, orderId = orderId }, value: orderId);
		}

    }
}