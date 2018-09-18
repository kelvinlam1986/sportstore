using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;

namespace SpyStore.Service.Controllers
{
	[Route("api/[controller]/{customerId}")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
		private IOrderRepo Repo { get; set; }

		public OrdersController(IOrderRepo repo)
		{
			Repo = repo;
		}

		public IActionResult GetOrderHistory(int customerId)
		{
			var ordersWithTotal = Repo.GetOrderHistory(customerId);
			return ordersWithTotal == null ? (IActionResult)NotFound()
				: new ObjectResult(ordersWithTotal);
		}

		[HttpGet("{orderId}", Name = "GetOrderDetails")]
		public IActionResult GetOrderForCustomer(int customerId, int orderId)
		{
			var orderWithDetails = Repo.GetOneWithDetails(customerId, orderId);
			return orderWithDetails == null ? (IActionResult)NotFound()
				: new ObjectResult(orderWithDetails);
		}
    }
}