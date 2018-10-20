﻿using Newtonsoft.Json;
using SpyStore.Models.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace SpyStore.MVC.ViewModels.Base
{
    public class CartViewModelBase : ProductAndCategoryBase
    {
        public int? CustomerId { get; set; }
        [DataType(DataType.Currency), Display(Name = "Total")]
        public decimal LineItemTotal { get; set; }
        public string TimeStampString => Timestamp != null ? JsonConvert.SerializeObject(Timestamp).Replace("\"", "") : string.Empty;
    }
}
