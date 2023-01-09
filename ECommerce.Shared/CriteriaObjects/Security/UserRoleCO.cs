using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CriteriaObjects.Security
{
    public class UserRoleCO
    {
        //[Required]
        public string? UserName { get; set; }
        ////[Required]
        //public Guid? RolGuid { get; set; }
        public string? Role { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int SayfaNo { get; set; }

        public int SayfaAdet { get; set; }
    }
}
