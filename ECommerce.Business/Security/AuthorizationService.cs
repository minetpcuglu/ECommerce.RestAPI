using System;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using ECommerce.Business.Aspects.AutoFac;
using ECommerce.Core.Utilities.Results;
using ECommerce.Data.EF.Security;
using ECommerce.Localization;
using ECommerce.Shared.DataTransferObjects.Security;
//using ECommerce.Shared.Request;

namespace ECommerce.Business.Security
{

    [AuditAspect]
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationService(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        [SecuredOperation("ROL_LISTELE")]
        public async Task<IDataResult<AuthorizationDTO>> GetByGuidAsync(Guid guid)
        {
            var result = await _authorizationRepository.GetByGuidAsync(guid);
            return new SuccessDataResult<AuthorizationDTO>(result,@Resource.bilgilerBasariylaGonderilmistir);
        }

        [SecuredOperation("ROL_LISTELE")]
        public async Task<IDataResult<AuthorizationDTO>> GetByIdAsync(int id)
        {
            var result = await _authorizationRepository.GetByIdAsync(id);
            return new SuccessDataResult<AuthorizationDTO>(result,@Resource.bilgilerBasariylaGonderilmistir);
        }

        [SecuredOperation("ROL_DUZENLE")]
        public async Task<IDataResult<AuthorizationDTO>> UpdateAsync(AuthorizationDTO dto)
        {
            var id = await _authorizationRepository.UpdateAsync(dto);
            return new SuccessDataResult<AuthorizationDTO>(dto, @Resource.kayitBasariylaGuncellendi);
        }

    }
}
