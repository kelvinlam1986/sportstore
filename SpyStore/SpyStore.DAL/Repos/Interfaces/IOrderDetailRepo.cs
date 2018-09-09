using SpyStore.DAL.Repos.Base;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;
using System.Collections.Generic;

namespace SpyStore.DAL.Repos.Interfaces
{
    public interface IOrderDetailRepo : IRepo<OrderDetail>
    {
        IEnumerable<OrdeDetailWithProductInfo> GetCustomersOrdersWithDetails(int customerId);
        IEnumerable<OrderWithDetailsAndProductInfo> GetSingleOrderWithDetails(int orderId);
    }
}
