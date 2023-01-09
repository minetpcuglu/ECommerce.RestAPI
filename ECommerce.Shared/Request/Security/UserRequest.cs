using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Request.Security
{
    public class UserRequest
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyusmamaktadır tekrar deneyiniz!")]
        public string PasswordAgain { get; set; }

    }
}
