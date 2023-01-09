using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string TuzlamaDegeri { get; set; }

        public string EMail { get; set; }
        public string Telephone { get; set; }

        [JsonIgnore]
        [Compare("Password", ErrorMessage = "Şifreler uyusmamaktadır tekrar deneyiniz!")]
        public string PasswordAgain { get; set; }

    }
}
