using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class UserRoleDTO : BaseDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
    }
}
