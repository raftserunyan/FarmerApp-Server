﻿using FarmerApp.Core.Models.Customer;
using FarmerApp.Core.Models.Product;
using FarmerApp.Core.Models.User;

namespace FarmerApp.Core.Models.Sale
{
    public class SaleModel : BaseModel
    {
        public double Weight { get; set; }
        public int PriceKG { get; set; }
        public int Paid { get; set; }
        public DateTime? Date { get; set; }

        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public int? UserId { get; set; }
        public UserModel User { get; set; }
    }
}

