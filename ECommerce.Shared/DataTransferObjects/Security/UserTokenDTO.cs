using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class UserTokenDTO
    {
        public UserDTO UserDetail { get; set; }
        public AccessTokenDTO TokenDetail { get; set; }
    }
}
