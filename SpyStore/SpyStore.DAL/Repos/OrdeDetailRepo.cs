using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos
{
	public class OrdeDetailRepo : RepoBase<OrderDetail>, IOrderDetailRepo
	{
		public OrdeDetailRepo(DbContextOptions<StoreContext> options) : base(options)
		{
		}

		public OrdeDetailRepo() : base()
		{
		}

		public override IEnumerable<OrderDetail> GetAll()
		{
			return Table.OrderBy(x => x.Id);
		}

		public override IEnumerable<OrderDetail> GetRange(int skip, int take)
		{
			return GetRange(Table.OrderBy(x => x.Id), skip, take);
		}

		internal IEnumerable<OrderDetailWithProductInfo> GetRecords(IQueryable<OrderDetail> query)
		{
			return query.Include(x => x.Product).ThenInclude(x => x.Category)
						.Select(x => new OrderDetailWithProductInfo
						{
							OrderId = x.OrderId,
							ProductId = x.ProductId,
							Quantity = x.Quantity,
							UnitCost = x.UnitCost,
							LineItemTotal = x.LineItemTotal,
							Description = x.Product.Description,
							ModelName = x.Product.ModelName,
							ProductImage = x.Product.ProductImage,
							ProductImageLarge = x.Product.ProductImageLarge,
							ProductImageThumb = x.Product.ProductImageThumb,
							ModelNumber = x.Product.ModelNumber,
							CategoryName = x.Product.Category.CategoryName
						})
						.OrderBy(x => x.ModelName);
		}

		public IEnumerable<OrderDetailWithProductInfo> GetCustomersOrdersWithDetails(int customerId)
		{
			return GetRecords(Table.Where(x => x.Order.CustomerId == customerId));
		}

		public IEnumerable<OrderDetailWithProductInfo> GetSingleOrderWithDetails(int orderId)
		{
			return GetRecords(Table.Where(x => x.OrderId == orderId));
		}
	}
}
