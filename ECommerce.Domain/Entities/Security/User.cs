using ECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Security
{
    [Table("User")]
    public class User : BaseEntity
    {
        [MaxLength(63)]
        [Required]
        public string Name { get; set; }

        [MaxLength(63)]
        [Required]
        public string Surname { get; set; }

        [MaxLength(127)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(63)]
        public string Password { get; set; }

        [MaxLength(63)]
        [Compare("Password", ErrorMessage = "Şifreler uyusmamaktadır tekrar deneyiniz!")]
        public string PasswordAgain { get; set; }

        [MaxLength(63)]
        public string TuzlamaDegeri { get; set; }

        [MaxLength(127)]
        public string EMail { get; set; }

        [MaxLength(31)]
        public string Telephone { get; set; }

        ///// <summary>
        ///// Şifremi unuttum kod değeri
        ///// </summary>
        //public string Kod { get; set; }

        ///// <summary>
        ///// Şifremi unuttum deneme sayısıdır
        ///// </summary>
        //public int? KodGirisSayisi { get; set; }

        ///// <summary>
        ///// şifremi unuttum Kod son geçerlilik tarihi
        ///// </summary>
        //public DateTime? KodSonGirisTarihi { get; set; }

    }
}
