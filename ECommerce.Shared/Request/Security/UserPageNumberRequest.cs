using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Request.Security
{
    public class UserPageNumberRequest
    {
        public Guid KodGuid { get; set; }
        public int SayfaAdet { get; set; }
    }
}
