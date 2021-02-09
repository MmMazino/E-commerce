using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.db
{
    public partial class TblShippingDetail
    {
        public int ShippingDetailId { get; set; }
        public int? MemberId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public long? OrderId { get; set; }
        public decimal? AmountPaid { get; set; }
        public string PaymentType { get; set; }

        public virtual TblMember Member { get; set; }
    }
}
