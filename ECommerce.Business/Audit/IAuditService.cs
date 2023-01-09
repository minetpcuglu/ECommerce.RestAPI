using System.Threading.Tasks;
using ECommerce.Core.Utilities.Results;
using ECommerce.Shared.CriteriaObjects.Audit;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Audit;

namespace ECommerce.Business.Audit
{
    public interface IAuditService
    { 
        Task<IDataResult<SayfalamaDTO<AuditDTO>>> GetAuditSayfalamaAsync(AuditCO co);
        Task<IDataResult<SayfalamaDTO<HataLogDTO>>> GetHataLogSayfalamaAsync(HataLogCO co);
    }
}
