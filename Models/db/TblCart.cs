using Ecommerce.ViewModels;
using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.db
{
    public partial class TblCart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int? MemberId { get; set; }
        public int? CartStatusId { get; set; }
        public int Quantity { get; set; }

        public long? OrderId { get; set; }

        public virtual TblProduct Product { get; set; }

        public TblProduct GetProduct()
        {
            ProductModel product = new ProductModel();
            return product.find(this.ProductId);
        }
    }
}
