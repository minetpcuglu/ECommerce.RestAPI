using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class AccessTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public long ExpirationTime { get; set; }
    }
}
