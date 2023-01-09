using System.Threading.Tasks;
using ECommerce.Shared.CriteriaObjects.Audit;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Audit;

namespace ECommerce.Data.AUDIT
{
    public interface IAuditRepository
    {
       Task SaveAuditAsync(AuditDTO audit);
       Task<SayfalamaDTO<AuditDTO>> GetAuditSayfalamaAsync(AuditCO co);
       Task<SayfalamaDTO<HataLogDTO>> GetHataLogSayfalamaAsync(HataLogCO co);
    }
}
