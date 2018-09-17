using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos
{
	public class OrderRepo : RepoBase<Order>, IOrderRepo
	{
		private readonly IOrderDetailRepo orderDetailRepo;

		public OrderRepo(DbContextOptions<StoreContext> options, IOrderDetailRepo orderDetailRepo) : base(options)
		{
			this.orderDetailRepo = orderDetailRepo;
		}

		public OrderRepo()
		{
		}

		public override IEnumerable<Order> GetAll()
		{
			return Table.OrderByDescending(x => x.OrderDate);
		}

		public override IEnumerable<Order> GetRange(int skip, int take)
		{
			return GetRange(Table.OrderByDescending(x => x.OrderDate), skip, take);
		}


		public OrderWithDetailsAndProductInfo GetOneWithDetails(int customerId, int orderId)
		{
			return Table
					.Include(x => x.OrderDetails)
					.Where(x => x.CustomerId == customerId && x.Id == orderId)
					.Select(x => new OrderWithDetailsAndProductInfo
					{
						Id = x.Id,
						CustomerId = customerId,
						OrderDate = x.OrderDate,
						OrderTotal = x.OrderTotal,
						ShipDate = x.ShipDate,
						OrderDetails = orderDetailRepo.GetSingleOrderWithDetails(orderId).ToList()
					}).FirstOrDefault();
		}

		public IEnumerable<Order> GetOrderHistory(int customerId)
		{
			return Table.Where(x => x.CustomerId == customerId)
					.Select(x => new Order
					{
						Id = x.Id,
						Timestamp = x.Timestamp,
						CustomerId = customerId,
						OrderDate = x.OrderDate,
						OrderTotal = x.OrderTotal,
						ShipDate = x.ShipDate
					});
		}
	}
}
