using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;
using SpyStore.Models.ViewModels.Base;
using SpyStore.MVC.Configuration;

namespace SpyStore.MVC.WebServiceAccess.Base
{
    public class WebApiCalls : WebApiCallsBase, IWebApiCalls
    {
        public WebApiCalls(IWebServiceLocator settings) : base(settings)
        {

        }

        public async Task<string> AddToCartAsync(int customerId, int productId, int quantity)
        {
            string uri = $"{CartBaseUri}{customerId}";
            string json = $"{{\"ProductId\":{productId},\"Quantity\":{quantity}}}";
            return await SubmitPostRequestAsync(uri, json);
        }

        public async Task<IList<CartRecordWithProductInfo>> GetCartAsync(int customerId)
        {
            return await GetItemListAsync<CartRecordWithProductInfo>($"{CartBaseUri}{customerId}");
        }

        public async Task<CartRecordWithProductInfo> GetCartRecordAsync(int customerId, int productId)
        {
            return await GetItemAsync<CartRecordWithProductInfo>($"{CartBaseUri}{customerId}/{productId}");
        }

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            return await GetItemListAsync<Category>(CategoryBaseUri);
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await GetItemAsync<Category>($"{CategoryBaseUri}{id}");
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await GetItemAsync<Customer>($"{CustomerBaseUri}{id}");
        }

        public async Task<IList<Customer>> GetCustomersAsync()
        {
            return await GetItemListAsync<Customer>(CustomerBaseUri);
        }

        public async Task<IList<ProductAndCategoryBase>> GetFeaturedProductsAsync()
        {
            return await GetItemListAsync<ProductAndCategoryBase>($"{ProductBaseUri}featured");
        }

        public async Task<ProductAndCategoryBase> GetOneProductAsync(int productId)
        {
            return await GetItemAsync<ProductAndCategoryBase>($"{ProductBaseUri}{productId}");
        }

        public async Task<OrderWithDetailsAndProductInfo> GetOrderDetailsAsync(int customerId, int orderId)
        {
            var uri = $"{OrdersBaseUri}{customerId}/{orderId}";
            return await GetItemAsync<OrderWithDetailsAndProductInfo>(uri);
        }

        public async Task<IList<Order>> GetOrdersAsync(int customerId)
        {
            return await GetItemListAsync<Order>($"{OrdersBaseUri}{customerId}");
        }

        public async Task<IList<ProductAndCategoryBase>> GetProductsForACategoryAsync(int categoryId)
        {
            var uri = $"{CategoryBaseUri}{categoryId}/products";
            return await GetItemListAsync<ProductAndCategoryBase>(uri);
        }

        public async Task<int> PurchaseCartAsync(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var uri = $"{CartBaseUri}{customer.Id}/buy";
            return int.Parse(await SubmitPostRequestAsync(uri, json));
        }

        public async Task RemoveCartItemAsync(int customerId, int shoppingCartRecordId, byte[] timeStamp)
        {
            var timeStampString = JsonConvert.SerializeObject(timeStamp);
            var uri = $"{CartBaseUri}{customerId}/{shoppingCartRecordId}/{timeStampString}";
            await SubmitDeleteRequestAsync(uri);
        }

        public async Task<IList<ProductAndCategoryBase>> SearchAsync(string searchTerm)
        {
            var uri = $"{ServiceAddress}api/search/{searchTerm}";
            return await GetItemListAsync<ProductAndCategoryBase>(uri);
        }

        public async Task<string> UpdateCartItemAsync(ShoppingCartRecord item)
        {
            string uri = $"{CartBaseUri}{item.CustomerId}/{item.Id}";
            var json = JsonConvert.SerializeObject(item);
            return await SubmitPutRequestAsync(uri, json);
        }
    }
}
