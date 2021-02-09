using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.db
{
    public partial class TblMemberRole
    {
        public int MemberRoleId { get; set; }
        public int? MemberId { get; set; }
        public int? RoleId { get; set; }
    }
}
