using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpyStore.DAL.Repos
{
    public class ProductRepo : RepoBase<Product>, IProducRepo
    {
        public ProductRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public ProductRepo()
        {
        }

        public override IEnumerable<Product> GetAll()
        {
            return Table.OrderBy(x => x.ModelName);
        }

        public override IEnumerable<Product> GetRange(int skip, int take)
        {
            return GetRange(Table.OrderBy(x => x.ModelName), skip, take);
        }

        internal ProductAndCategoryBase GetRecord(Product product, Category category)
        {
            return new ProductAndCategoryBase
            {
                CategoryName = category.CategoryName,
                CategoryId = product.CategoryId,
                CurrentPrice = product.CurrentPrice,
                Description = product.Description,
                IsFeature = product.IsFeature,
                Id = product.Id,
                ModelName = product.ModelName,
                ModelNumber = product.ModelNumber,
                ProductImage = product.ProductImage,
                ProductImageLarge = product.ProductImageLarge,
                ProductImageThumb = product.ProductImageThumb,
                Timestamp = product.Timestamp,
                UnitCost = product.UnitCost,
                UnitsInStock = product.UnitsInStock
            };
        }

        public IEnumerable<ProductAndCategoryBase> GetAllWithCategoryName()
        {
            return Table
                    .Include(x => x.Category)
                    .Select(x => GetRecord(x, x.Category))
                    .OrderBy(x => x.ModelName);
        }

        public IEnumerable<ProductAndCategoryBase> GetFeaturedWithCategoryName()
        {
            return Table
                    .Where(x => x.IsFeature)
                    .Include(x => x.Category)
                    .Select(x => GetRecord(x, x.Category))
                    .OrderBy(x => x.ModelName);
        }

        public ProductAndCategoryBase GetOneWithCategoryName(int id)
        {
            return Table
                    .Where(x => x.Id == id)
                    .Include(x => x.Category)
                    .Select(x => GetRecord(x, x.Category))
                    .SingleOrDefault();
        }

        public IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int id)
        {
            return Table
                    .Where(x => x.CategoryId == id)
                    .Include(x => x.Category)
                    .Select(x => GetRecord(x, x.Category))
                    .OrderBy(x => x.ModelName);
        }

        public IEnumerable<ProductAndCategoryBase> Search(string searchString)
        {
            return Table
                    .Where(x => x.Description.ToLower().Contains(searchString.ToLower())
                        || x.ModelName.ToLower().Contains(searchString.ToLower()))
                    .Include(x => x.Category)
                    .Select(x => GetRecord(x, x.Category))
                    .OrderBy(x => x.ModelName);
        }
    }
}
