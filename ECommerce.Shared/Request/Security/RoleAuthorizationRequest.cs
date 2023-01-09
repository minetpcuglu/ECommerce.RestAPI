using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Request.Security
{
    public class RoleAuthorizationRequest
    {
        public bool Secili { get; set; }
        public GuidRequest Role { get; set; }
        public GuidRequest Authorization { get; set; }
    }
}
