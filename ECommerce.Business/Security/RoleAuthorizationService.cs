using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Shared.Request.Security;
using ECommerce.Business.Aspects.AutoFac;
using ECommerce.Core.Utilities.Results;
using ECommerce.Data.EF.Security;
using ECommerce.Localization;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Business.Security
{
    [AuditAspect]
    public class RoleAuthorizationService: IRoleAuthorizationService
    {
        private readonly IRoleAuthorizationRepository _roleAuthorizationRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleAuthorizationService(IRoleAuthorizationRepository roleAuthorizationRepository, IRoleRepository roleRepository, IAuthorizationRepository authorizationRepository)
        {
            _roleAuthorizationRepository = roleAuthorizationRepository;
            _roleRepository = roleRepository;
            _authorizationRepository = authorizationRepository;
        }

        [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<RoleAuthorizationDTO>> GetByIdAsync(int id)
        {
            var dto = await _roleAuthorizationRepository.GetByIdAsync(id);
            return new SuccessDataResult<RoleAuthorizationDTO>(dto,@Resource.bilgilerBasariylaGonderilmistir);
        }

        [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<RoleAuthorizationDTO>> GetByGuidAsync(Guid guid)
        {
            var dto = await _roleAuthorizationRepository.GetByGuidAsync(guid);
            return new SuccessDataResult<RoleAuthorizationDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        [SecuredOperation("ROL_EKLE")]
        public async Task<DataResult<RoleAuthorizationDTO>> CreateAsync(RoleAuthorizationRequest request)
        {
            var rol = await _roleRepository.GetByGuidAsync(request.Role.Guid);
            var yetki = await _authorizationRepository.GetByGuidAsync(request.Authorization.Guid);
            var dto = new RoleAuthorizationDTO()
            {
                IsActive = true,
                CreationTime = DateTime.Now,
                RoleId = rol.Id,
                AuthorizationId = yetki.Code,
                Guid = Guid.NewGuid()
            };
            var id = await _roleAuthorizationRepository.CreateAsync(dto);
            dto.Id = id;
            if (id > 0)
            {
                return new SuccessDataResult<RoleAuthorizationDTO>(dto,@Resource.kayitBasariylaEklendi);
            }
            return new ErrorDataResult<RoleAuthorizationDTO>(dto, @Resource.kayitEklenemedi);
        }

        [SecuredOperation("ROL_SIL")]
        public async Task<IResult> DeleteAsync(Guid guid)
        {
            await _roleAuthorizationRepository.DeleteAsync(guid);
            return new SuccessResult(@Resource.kayitBasariylaSilindi);
        }

        [SecuredOperation("ROL_DUZENLE")]
        public async Task<DataResult<RoleAuthorizationDTO>> UpdateAsync(Guid rolYetkiGuid, RoleAuthorizationRequest request)
        {
            var dto = await _roleAuthorizationRepository.GetByGuidAsync(rolYetkiGuid);
            dto.ModificationTime = DateTime.Now;
            var id = await _roleAuthorizationRepository.UpdateAsync(dto); 
            if (id > 0)
            {
                return new SuccessDataResult<RoleAuthorizationDTO>(dto, @Resource.kayitBasariylaGuncellendi);
            }
            return new ErrorDataResult<RoleAuthorizationDTO>(dto, @Resource.kayitGuncellenemedi);
        }

        [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<List<RoleAuthorizationDTO>>> GetAllAuthorizationAsync(Guid rolGuid)
        {
            var dtos = await _roleAuthorizationRepository.GetAllAuthorizationAsync(rolGuid);
            return new SuccessDataResult<List<RoleAuthorizationDTO>>(dtos, @Resource.bilgilerBasariylaGonderilmistir);
        }


        [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<RoleAuthorizationDTO>> GetByRoleGuidAndAuthorizationGuidAsync(Guid rolGuid, Guid yetkiGuid)
        {
            var dtos = await _roleAuthorizationRepository.GetByRoleGuidAndAuthorizationGuidAsync(rolGuid, yetkiGuid);
            return new SuccessDataResult<RoleAuthorizationDTO>(dtos, @Resource.bilgilerBasariylaGonderilmistir);
        }


       [SecuredOperation("ROL_EKLE")]
        public async Task<IResult> RoleAuthorizationSaveAsync(List<RoleAuthorizationRequest> requests)
        {
            IResult result;
            try
            {

                foreach (var item in requests)
                {
                    var dto = new RoleAuthorizationDTO();
                    var varMi = await _roleAuthorizationRepository.GetByRoleGuidAndAuthorizationGuidAsync(item.Role.Guid, item.Authorization.Guid);
                    if (item.Secili)
                    {
                        if (varMi != null) continue;
                        var rol = await _roleRepository.GetByGuidAsync(item.Role.Guid);
                        dto.RoleId = rol.Id;
                        var yetki = await _authorizationRepository.GetByGuidAsync(item.Authorization.Guid);
                        dto.AuthorizationId = yetki.Code;
                        dto.IsActive= true;
                        dto.CreationTime = DateTime.Now;
                        dto.Guid = Guid.NewGuid();
                        await _roleAuthorizationRepository.CreateAsync(dto);
                    }
                    else
                    {
                        if (varMi != null)
                        {
                            await _roleAuthorizationRepository.DeleteAsync(varMi.Guid);
                        }
                    }
                }

                result = new SuccessResult(@Resource.kayitBasariylaKaydedildi);
            }
            catch (Exception e)
            {

                result = new ErrorResult(e?.Message);
            }

            return result;
        }
    }
}
