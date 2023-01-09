
using System;
using System.Collections.Generic;
using System.Linq;
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

    //[AuditAspect]
    public class UserRoleService: IUserRoleService
    {
        private readonly IUserRoleRepository _UserRoleRepository;
       private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserRoleService(IUserRoleRepository UserRoleRepository ,IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _UserRoleRepository = UserRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        //[SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<UserRoleDTO>> GetByIdAsync(int id)
        {
            var dto = await _UserRoleRepository.GetByIdAsync(id);
            return new SuccessDataResult<UserRoleDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<UserRoleDTO>> GetByGuidAsync(Guid guid)
        {
            var dto = await _UserRoleRepository.GetByGuidAsync(guid);
            return new SuccessDataResult<UserRoleDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<List<UserRoleDTO>>> GetByUserRolesAsync(Guid kullaniciGuid)
        {
            var dtos = await _UserRoleRepository.GetByUserRolesAsync(kullaniciGuid);
            return new SuccessDataResult<List<UserRoleDTO>>(dtos.ToList(), @Resource.bilgilerBasariylaGonderilmistir);
        }
        public async Task<DataResult<List<UserRoleDTO>>> GetByUserIdRolesAsync(int kullaniciId)
        {
            var dtos = await _UserRoleRepository.GetByUserIdRolesAsync(kullaniciId);
            return new SuccessDataResult<List<UserRoleDTO>>(dtos.ToList(), @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<UserRoleDTO>> GetByUserIdAndRoleIdAsync(int kullaniciId, int rolId)
        {
            var dto = await _UserRoleRepository.GetByUserIdAndRoleIdAsync(kullaniciId, rolId);
            return new SuccessDataResult<UserRoleDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[SecuredOperation("ROL_EKLE")]
        public async Task<DataResult<UserRoleDTO>> CreateAsync(UserRoleRequest request)
        {
            var kullanici = await _userRepository.GetByGuidAsync(request.UserGuid);
            var rol = await _roleRepository.GetByGuidAsync(request.RoleGuid);
            var dto = new UserRoleDTO()
            {
                IsActive = request.IsActive,
                UserId = kullanici.Id,
                RoleId = rol.Id,
                CreationTime = DateTime.Now,
                Guid = Guid.NewGuid(),
            };
            var id = await _UserRoleRepository.CreateAsync(dto);
            dto.Id = id;
            if (id > 0)
            {
                return new SuccessDataResult<UserRoleDTO>(dto, @Resource.kayitBasariylaEklendi);
            }
            return new ErrorDataResult<UserRoleDTO>(dto, @Resource.kayitEklenemedi);
        }

        //[SecuredOperation("ROL_DUZENLE")]
        public async Task<DataResult<UserRoleDTO>> UpdateAsync(Guid UserRoleGuid, UserRoleRequest request)
        {
            var dto = await _UserRoleRepository.GetByGuidAsync(UserRoleGuid);

            dto.IsActive = request.IsActive;
            //dto.KullaniciId = request.KullaniciId;
            //dto.RolId = request.RolId;
            dto.ModificationTime = DateTime.Now;


            var id = await _UserRoleRepository.UpdateAsync(dto);
            if (id > 0)
            {
                return new SuccessDataResult<UserRoleDTO>(dto, @Resource.kayitBasariylaGuncellendi);
            }
            return new ErrorDataResult<UserRoleDTO>(dto, @Resource.kayitGuncellenemedi);
        }


        //[SecuredOperation("ROL_SIL")]
        public async Task<IResult> DeleteAsync(Guid guid)
        {
            await _UserRoleRepository.DeleteAsync(guid);
            return new SuccessResult(@Resource.kayitBasariylaSilindi);
        }

       [SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<bool>> UserRoleControlAsync(int kullaniciId, int[] yetkiIds)
        {
            var result = await _UserRoleRepository.UserRoleControlAsync(kullaniciId, yetkiIds);
            return new SuccessDataResult<bool>(result, @Resource.bilgilerBasariylaGonderilmistir);
        }
         
        /// <summary>
        /// Login olan rolsüz kullanıcılarda kullandığı için yetkilendirme yapılmadı
        /// </summary>
        /// <param name = "kullaniciId" ></ param >
        /// < returns ></ returns >
        public async Task<DataResult<string[]>> UserAuthorizationsAsync(int kullaniciId)
        {
            var result = await _UserRoleRepository.UserAuthorizeAsync(kullaniciId);
            return new SuccessDataResult<string[]>(result, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[SecuredOperation("ROL_LISTELE")]
        public async Task<DataResult<SayfalamaDTO<UserRoleDTO>>> GetUserRolePagingAsync(UserRoleCO co)
        {
            var result = await _UserRoleRepository.GetUserRolePagingAsync(co);
            return new SuccessDataResult<SayfalamaDTO<UserRoleDTO>>(result, @Resource.bilgilerBasariylaGonderilmistir);
        }
    }
}
