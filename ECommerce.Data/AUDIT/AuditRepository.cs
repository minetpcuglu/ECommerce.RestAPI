using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using ECommerce.Shared.CriteriaObjects.Audit;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Audit;
using Npgsql;
using Microsoft.Data.SqlClient;

namespace ECommerce.Data.AUDIT
{
    public class AuditRepository:  IAuditRepository
    {
        private readonly IConfiguration _configuration;

        public AuditRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task SaveAuditAsync(AuditDTO audit)
        {
            string sql = "INSERT INTO [dbo].[Audit]" +
                         "( [Guid], [UserName], [UserId], [MethodAdi], [Detay], [SubeAdi], [IpAdres], [Tarayici], [EklenmeZamani], [IslemSuresi], [SinifAdi], [Uri], [Host], [Protocol], [HttpMethod], [SirketGuid], [SubeGuid]) VALUES" +
                         " (@Guid, @UserName, @UserId, @MethodAdi, @Detay, @SubeAdi, @IpAdres, @Tarayici, @EklenmeZamani, @IslemSuresi, @SinifAdi, @Uri, @Host, @Protocol, @HttpMethod,@SirketGuid, @SubeGuid);";

            await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("ECommerceConnectionLog"));
            await connection.ExecuteAsync(sql, audit);
        }

        public async Task<SayfalamaDTO<AuditDTO>> GetAuditSayfalamaAsync(AuditCO co)
        {
            var sayfaNo = co.SayfaNo < 1 ? 1 : co.SayfaNo;
            var sayfaAdet = co.SayfaAdet < 1 ? 10 : co.SayfaAdet;

            co.Next = sayfaAdet;
            co.Offset = (sayfaNo - 1) * sayfaAdet;
            var where = "";

            if (co.Id.HasValue)
            {
                where += " AND (Id = @Id) ";
            }

            if (!string.IsNullOrEmpty(co.UserName))
            {
                where += " AND (UserName LIKE Concat('%',@UserName, '%')) ";
            }
            
            if (!string.IsNullOrEmpty(co.HttpMethod))
            {
                where += " AND (HttpMethod LIKE Concat('%',@HttpMethod, '%')) ";
            }            
            if (!string.IsNullOrEmpty(co.Tarayici))
            {
                where += " AND (Tarayici LIKE Concat('%',@Tarayici, '%')) ";
            }      
            if (!string.IsNullOrEmpty(co.Host))
            {
                where += " AND (Host LIKE Concat('%',@Host, '%')) ";
            }

            if (!string.IsNullOrEmpty(co.MethodAdi))
            {
                where += " AND (MethodAdi LIKE  Concat('%',@MethodAdi, '%')) ";
            }

            var sql = (@"SELECT * from [dbo].[Audit]
                        WHERE 1=1  " + where + @"
                      order by [Id] desc
                      OFFSET @Offset ROWS 
                      FETCH NEXT  @Next ROWS ONLY");

            await using var connection =
                new NpgsqlConnection(_configuration.GetConnectionString("ECommerceConnectionLog"));
            var results = (await connection.QueryAsync<AuditDTO>(sql, co)).ToList();


            int count = connection.ExecuteScalar<int>("select count(1) from [dbo].[Audit] WHERE 1=1  " + where + @"", co);

            var sayfalama = new SayfalamaDTO<AuditDTO>()
            {
                IlkSayfaMi = co.SayfaNo == 0,
                SonSayfaMi = co.SayfaNo == count / co.SayfaAdet,
                SayfaNo = co.SayfaNo,
                SayfaKayitSayisi = results.Count(),
                SayfaAdet = co.SayfaAdet,
                Siralama = "", //sorting,
                ToplamKayitSayisi = count,
                ToplamSayfaSayisi = (count / co.SayfaAdet + 1),
                Veri = results
            };

            return sayfalama;
        }

        public async Task<SayfalamaDTO<HataLogDTO>> GetHataLogSayfalamaAsync(HataLogCO co)
        {
            var sayfaNo = co.SayfaNo < 1 ? 1 : co.SayfaNo;
            var sayfaAdet = co.SayfaAdet < 1 ? 10 : co.SayfaAdet;

            co.Next = sayfaAdet;
            co.Offset = (sayfaNo - 1) * sayfaAdet;
            var where = "";

            if (co.Id.HasValue)
            {
                where += " AND (Id = @Id) ";
            }

            if (!string.IsNullOrEmpty(co.Username))
            {
                where += " AND (Username LIKE Concat('%',@Username, '%')) ";
            }

   
            if (!string.IsNullOrEmpty(co.HttpMethod))
            {
                where += " AND (HttpMethod LIKE Concat('%',@HttpMethod, '%')) ";
            }

   
            if (!string.IsNullOrEmpty(co.Host))
            {
                where += " AND (Host LIKE Concat('%',@Host, '%')) ";
            }

   
            if (!string.IsNullOrEmpty(co.Uri))
            {
                where += " AND (Uri LIKE Concat('%',@Uri, '%')) ";
            }

   
            if (!string.IsNullOrEmpty(co.UserAgent))
            {
                where += " AND (UserAgent LIKE Concat('%',@UserAgent, '%')) ";
            }
              
            if (!string.IsNullOrEmpty(co.IpAddress))
            {
                where += " AND (IpAddress LIKE Concat('%',@IpAddress, '%')) ";
            }

   
            if (!string.IsNullOrEmpty(co.Audit))
            {
                where += " AND (Audit LIKE Concat('%',@Audit, '%')) ";
            }

   

            var sql = (@"SELECT * from [dbo].[Logs]
                        WHERE 1=1  " + where + @"
                      order by [Id] desc
                      OFFSET @Offset ROWS 
                      FETCH NEXT  @Next ROWS ONLY");

            await using var connection =
                new NpgsqlConnection(_configuration.GetConnectionString("ECommerceConnectionLog"));
            var results = (await connection.QueryAsync<HataLogDTO>(sql, co)).ToList();


            int count = connection.ExecuteScalar<int>("select count(1) from [dbo].[Logs] WHERE 1=1  " + where + @"", co);

            var sayfalama = new SayfalamaDTO<HataLogDTO>()
            {
                IlkSayfaMi = co.SayfaNo == 0,
                SonSayfaMi = co.SayfaNo == count / co.SayfaAdet,
                SayfaNo = co.SayfaNo,
                SayfaKayitSayisi = results.Count(),
                SayfaAdet = co.SayfaAdet,
                Siralama = "", //sorting,
                ToplamKayitSayisi = count,
                ToplamSayfaSayisi = (count / co.SayfaAdet + 1),
                Veri = results
            };

            return sayfalama;
        }
    }
}
