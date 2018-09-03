using SpyStore.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpyStore.Models.Entities
{
    [Table("Customers", Schema = "Store"]
    public class Customer : EntityBase
    {
    }
}
