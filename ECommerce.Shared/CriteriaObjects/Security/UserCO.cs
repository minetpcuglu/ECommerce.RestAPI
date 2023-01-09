using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CriteriaObjects.Security
{
    public class UserCO
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? UserName { get; set; }

        public int SayfaNo { get; set; }

        public int SayfaAdet { get; set; }

        public bool? IsActive { get; set; }
    }
}
