using ECommerce.Shared.CriteriaObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class RoleAuthorizationDTO : BaseDTO
    {
        public int RoleId { get; set; }
        public int AuthorizationId { get; set; }
        public bool Secili { get; set; }

        public RoleDTO Role { get; set; }
        public AuthorizationDTO Authorization { get; set; }
    }
}
