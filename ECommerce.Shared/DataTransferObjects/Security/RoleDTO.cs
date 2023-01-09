using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class RoleDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

