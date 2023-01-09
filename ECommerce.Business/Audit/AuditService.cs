using System.Threading.Tasks;
using ECommerce.Business.Aspects.AutoFac;
using ECommerce.Core.Utilities.Results;
using ECommerce.Data.AUDIT;
using ECommerce.Localization;
using ECommerce.Shared.CriteriaObjects.Audit;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Audit;

namespace ECommerce.Business.Audit
{

    [AuditAspect]
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }


        //[SecuredOperation(roles: "LOG_LISTELE")]
        public async Task<IDataResult<SayfalamaDTO<AuditDTO>>> GetAuditSayfalamaAsync(AuditCO co)
        {
            var list = await _auditRepository.GetAuditSayfalamaAsync(co);
            return new SuccessDataResult<SayfalamaDTO<AuditDTO>>(list, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[SecuredOperation(roles: "LOG_LISTELE")]
        public async Task<IDataResult<SayfalamaDTO<HataLogDTO>>> GetHataLogSayfalamaAsync(HataLogCO co)
        {
            var list = await _auditRepository.GetHataLogSayfalamaAsync(co);
            return new SuccessDataResult<SayfalamaDTO<HataLogDTO>>(list, @Resource.bilgilerBasariylaGonderilmistir);
        }
    }
}
