namespace ECommerce.Shared.CriteriaObjects.Audit
{
    public class HataLogCO
    {
        public int? Id { get; set; }

        public string Username { get; set; }
 
        public string HttpMethod { get; set; }
 
        public string Host { get; set; }
        public string Uri { get; set; } 
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
  
        public string Audit { get; set; }
        public int SayfaNo { get; set; }
        public int SayfaAdet { get; set; }

        public int Offset { get; set; }
        public int Next { get; set; }

    }
}
