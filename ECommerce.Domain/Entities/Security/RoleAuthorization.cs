using ECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Security
{
    [Table("RoleAuthorization")]
    public class RoleAuthorization : BaseEntity
    {
        public int RoleId { get; set; }
        public int AuthorizationId { get; set; }

        public virtual Authorization Authorization { get; set; }
        public virtual Role Role { get; set; }
    }
}
