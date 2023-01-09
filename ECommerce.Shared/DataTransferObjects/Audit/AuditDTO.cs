using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.DataTransferObjects.Audit
{
    public class AuditDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [MaxLength(63)]
        public string UserName { get; set; }
        public int? UserId { get; set; }

        [MaxLength(127)]
        public string SinifAdi { get; set; }

        [MaxLength(127)]
        public string MethodAdi { get; set; }

        public string Detay { get; set; }

        [MaxLength(31)]
        public string SubeAdi { get; set; }

        [MaxLength(63)]
        public string IpAdres { get; set; }

        [MaxLength(63)]
        public string Tarayici { get; set; }

        [MaxLength(1023)]
        public string Uri { get; set; }

        [MaxLength(127)]
        public string Host { get; set; }

        [MaxLength(15)]
        public string Protocol { get; set; }

        [MaxLength(31)]
        public string HttpMethod { get; set; }

        public Guid? SirketGuid { get; set; }
        public Guid? SubeGuid { get; set; }
        public decimal? IslemSuresi { get; set; }
        public DateTime EklenmeZamani { get; set; }
    }
}
