using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.Exceptions;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos
{
	public class ShoppingCartRepo : RepoBase<ShoppingCartRecord>, IShoppingCartRepo
	{
		private readonly IProductRepo productRepo;

		public ShoppingCartRepo(DbContextOptions<StoreContext> options, IProductRepo productRepo) : base(options)
		{
			this.productRepo = productRepo;
		}

		public ShoppingCartRepo(IProductRepo productRepo): base()
		{
			this.productRepo = productRepo;
		}

		public override IEnumerable<ShoppingCartRecord> GetAll()
		{
			return Table.OrderByDescending(x => x.DateCreated);
		}

		public override IEnumerable<ShoppingCartRecord> GetRange(int skip, int take)
		{
			return GetRange(Table.OrderByDescending(x => x.DateCreated), skip, take);
		}

		public int Add(ShoppingCartRecord entity, int? quantityInStock, bool persist = true)
		{
			var item = Find(entity.CustomerId, entity.ProductId);
			if (item == null)
			{
				if (quantityInStock != null && entity.Quantity > quantityInStock.Value)
				{
					throw new InvalidQuantityException("Can't add more product more than available in stock");
				}

				return base.Add(entity, persist);
			}

			item.Quantity += entity.Quantity;
			return item.Quantity <= 0 ? Delete(item, persist) : Update(item, quantityInStock, persist);
		}

		public override int Add(ShoppingCartRecord entity, bool persist = true)
		{
			return Add(entity, productRepo.Find(entity.ProductId)?.UnitsInStock, persist);
		}

		public ShoppingCartRecord Find(int customerId, int productId)
		{
			return Table.FirstOrDefault(x => x.CustomerId == customerId && x.ProductId == productId);
		}

		internal CartRecordWithProductInfo GetRecord(int customerId, ShoppingCartRecord scr, Product p, Category c)
		{
			return new CartRecordWithProductInfo
			{
				Id = scr.Id,
				DateCreated = scr.DateCreated,
				CustomerId = customerId,
				Quantity = scr.Quantity,
				ProductId = scr.ProductId,
				Description = p.Description,
				ModelName = p.ModelName,
				ModelNumber = p.ModelNumber,
				ProductImage = p.ProductImage,
				ProductImageLarge = p.ProductImageLarge,
				ProductImageThumb = p.ProductImageThumb,
				CurrentPrice = p.CurrentPrice,
				UnitsInStock = p.UnitsInStock,
				CategoryName = c.CategoryName,
				LineItemTotal = scr.Quantity * p.CurrentPrice,
				Timestamp = scr.Timestamp
			};
		}

		public CartRecordWithProductInfo GetShoppingCartRecord(int customerId, int productId)
		{
			return Table
					.Where(x => x.CustomerId == customerId && x.ProductId == productId)
					.Include(x => x.Product)
					.ThenInclude(x => x.Category)
					.Select(x => GetRecord(customerId, x, x.Product, x.Product.Category))
					.FirstOrDefault();
		}

		public IEnumerable<CartRecordWithProductInfo> GetShoppingCartRecords(int customerId)
		{
			return Table
					.Where(x => x.CustomerId == customerId)
					.Include(x => x.Product)
					.ThenInclude(x => x.Category)
					.Select(x => GetRecord(customerId, x, x.Product, x.Product.Category))
					.OrderBy(x => x.ModelName);
		}

		public int Purchase(int customerId)
		{
			var customerIdParam = new SqlParameter("@customerId", System.Data.SqlDbType.Int)
			{
				Direction = System.Data.ParameterDirection.Input,
				Value = customerId
			};

			var orderIdParam = new SqlParameter("@orderId", System.Data.SqlDbType.Int)
			{
				Direction = System.Data.ParameterDirection.Output
			};

			try
			{
				Context.Database.ExecuteSqlCommand("EXEC [Store].[PurchaseItemsInCart] @customerId, @orderId out", customerIdParam, orderIdParam);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return -1;
			}

			return (int)orderIdParam.Value;
		}

		public override int Update(ShoppingCartRecord entity, bool persist = true)
		{
			return Update(entity, productRepo.Find(entity.ProductId)?.UnitsInStock, persist);
		}

		public int Update(ShoppingCartRecord entity, int? quantityInStock, bool persist = true)
		{
			if (entity.Quantity <= 0)
			{
				return Delete(entity, persist);
			}

			if (entity.Quantity > quantityInStock)
			{
				throw new InvalidQuantityException("Can't add more product than available in stock");
			}

			return base.Update(entity, persist);
		}
	}
}
