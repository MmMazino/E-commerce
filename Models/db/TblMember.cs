using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.db
{
    public partial class TblMember
    {
        public TblMember()
        {
            TblShippingDetails = new HashSet<TblShippingDetail>();
        }

        public int MemberId { get; set; }
        public string Username { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<TblShippingDetail> TblShippingDetails { get; set; }
    }
}
