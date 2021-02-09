using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.db
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblCarts = new HashSet<TblCart>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Description { get; set; }
        public string ProductImage { get; set; }
        public bool? IsFeatured { get; set; }
        public int? Quantity { get; set; }
        public double Price { get; set; }

        public virtual TblCategory Category { get; set; }
        public virtual ICollection<TblCart> TblCarts { get; set; }

		public Product ToProduct()
        {
            return new Product
            {
                Id = ProductId,
                Name = ProductName,
                Photo = ProductImage,
                Price = Price
            };
        }

        internal static void RemoveAll(List<TblProduct> cart)
        {
            throw new NotImplementedException();
        }
    }
}
