using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Audit
{
    public class HataLogDTO
    {
        public int Id { get; set; }

        public string Detail { get; set; }

        public DateTime Date { get; set; }
        public string Audit { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public Guid? CompanyGuid { get; set; }
        public Guid? BranchGuid { get; set; }
        public string HttpMethod { get; set; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        public string Uri { get; set; }
        public decimal? ElapsedTime { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public string Body { get; set; }
    }
}
