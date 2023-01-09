using System;
using System.Threading.Tasks;
using ECommerce.Shared.Request.Security;
using ECommerce.Business.Aspects.AutoFac;
using ECommerce.Core.Utilities.Results;
using ECommerce.Data.EF.Security;
using ECommerce.Localization;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Business.Security
{

    [AuditAspect]
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

       [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<RoleDTO>> GetByIdAsync(int id)
        {
            var dto = await _roleRepository.GetByIdAsync(id);
            return new SuccessDataResult<RoleDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<RoleDTO>> GetByGuidAsync(Guid guid)
        {
            var dto = await _roleRepository.GetByGuidAsync(guid);
            return new SuccessDataResult<RoleDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

       [SecuredOperation("ROL_EKLE")]
        public async Task<DataResult<RoleDTO>> CreateAsync(RoleRequest request)
        {
            var dto = new RoleDTO()
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                CreationTime = DateTime.Now,
                Guid = Guid.NewGuid(),
            };
            var id = await _roleRepository.CreateAsync(dto);
            dto.Id = id;
            if (id > 0)
            {
                return new SuccessDataResult<RoleDTO>(dto, @Resource.kayitBasariylaEklendi);
            }
            return new ErrorDataResult<RoleDTO>(dto, @Resource.kayitEklenemedi);
        }
        
        [SecuredOperation("ROL_SIL")]
        public async Task<IResult> DeleteAsync(Guid guid)
        {
            await _roleRepository.DeleteAsync(guid);
            return new SuccessDataResult<RoleDTO>(@Resource.kayitBasariylaSilindi);
        }

        [SecuredOperation("ROL_DUZENLE")]
        public async Task<DataResult<RoleDTO>> UpdateAsync(Guid rolGuid, RoleRequest request)
        {
            var dto = await _roleRepository.GetByGuidAsync(rolGuid);
            dto.Description = request.Description;
            dto.Name = request.Name;
            dto.IsActive = request.IsActive;
            dto.ModificationTime = DateTime.Now;
            
            var id = await _roleRepository.UpdateAsync(dto);
            if (id > 0)
            {
                return new SuccessDataResult<RoleDTO>(dto, @Resource.kayitBasariylaGuncellendi);
            }
            return new ErrorDataResult<RoleDTO>(dto, @Resource.kayitGuncellenemedi);
        }


        [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<SayfalamaDTO<RoleDTO>>> GetRolePagingAsync(RoleCO co)
        {
            var dtos = await _roleRepository.GetRolePagingAsync(co);
            return new SuccessDataResult<SayfalamaDTO<RoleDTO>>(dtos, @Resource.bilgilerBasariylaGonderilmistir);
        }
    }
}
