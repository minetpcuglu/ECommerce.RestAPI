using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Request.Security
{
    public class UserRoleRequest
    {
        public Guid UserGuid { get; set; }
        public Guid RoleGuid { get; set; }
        public bool IsActive { get; set; }
    }
}
