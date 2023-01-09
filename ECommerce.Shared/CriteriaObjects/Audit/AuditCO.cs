namespace ECommerce.Shared.CriteriaObjects.Audit

{
    public class AuditCO
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string MethodAdi { get; set; }
        public string HttpMethod { get; set; }

        public string Tarayici { get; set; }
        public string Host { get; set; }
        public int SayfaNo { get; set; }
        public int SayfaAdet { get; set; }

        public int Offset { get; set; }
        public int Next { get; set; }

 
    }
}
