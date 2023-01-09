using ECommerce.Data.EF.Mappings;
using ECommerce.Domain.Entities.Security;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Data.EF.Security;
using ECommerce.Data.EF;
using ECommerce.Domain.Infrastructure.Contexts;

namespace ECommerce.Data.EF.Security
{
    public class UserRoleRepository : IUserRoleRepository
    {

        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<RoleAuthorization> _roleAuthorizationRepository;
        private readonly IRepository<Authorization> _authorizationRepository;
        private readonly DatabaseContext _context;

        public UserRoleRepository(IRepository<UserRole> userRoleRepository, DatabaseContext context,IRepository<RoleAuthorization> roleAuthorizationRepository, IRepository<Authorization> authorizationRepository)
        {
            _userRoleRepository = userRoleRepository;
            _context = context;
            _roleAuthorizationRepository = roleAuthorizationRepository;
            _authorizationRepository = authorizationRepository;
        }


        public async Task<UserRoleDTO> GetByIdAsync(int id)
        {
            var entity = await _userRoleRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Id == id);
            return ModelMapper.Mapper.Map<UserRoleDTO>(entity);
        }

        public async Task<UserRoleDTO> GetByGuidAsync(Guid guid)
        {
            var entity = await _userRoleRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Guid == guid);
            return ModelMapper.Mapper.Map<UserRoleDTO>(entity);
        }

        public async Task<int> CreateAsync(UserRoleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<UserRole>(dto);
            entity.EntityState = EntityState.Added;
            return await _userRoleRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _userRoleRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            await _userRoleRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(Guid guid)
        {
            var entity = await _userRoleRepository.GetFirstOrDefaultAsync(x => x.Guid == guid);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            await _userRoleRepository.SaveAsync(entity);
        }

        public async Task<int> UpdateAsync(UserRoleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<UserRole>(dto);
            entity.EntityState = EntityState.Modified;
            return await _userRoleRepository.SaveAsync(entity);
        }


        public async Task<SayfalamaDTO<UserRoleDTO>> GetUserRolePagingAsync(UserRoleCO co)
        {

            IQueryable<UserRole> list = _context.UserRoles.AsNoTracking();
                //.Where(x => x.Deleted == null && x.Rol.Guid == co.RolGuid
                //            && Microsoft.EntityFrameworkCore.EF.Functions.Like(x.User.Ad, "%" + co.Ad + "%")
                //            && Microsoft.EntityFrameworkCore.EF.Functions.Like(x.User.Soyad, "%" + co.Soyad + "%")
                //            && Microsoft.EntityFrameworkCore.EF.Functions.Like(x.User.UserAdi, "%" + co.UserAdi + "%"));

            var page = co.SayfaNo;
            var itemsPerPage = co.SayfaAdet < 1 ? 20 : co.SayfaAdet;
            var skip = page * itemsPerPage;

            var tmpContent = await list.OrderBy(x => x.User.Name).ThenBy(x => x.User.Surname)
                .Include(k => k.User)
                .Include(x=>x.Role)
                .Skip(skip).Take(itemsPerPage)
                .ToListAsync();
            var sayfaUserRoleler = ModelMapper.Mapper.Map<IList<UserRoleDTO>>(tmpContent);


            var sayfalama = new SayfalamaDTO<UserRoleDTO>()
            {
                IlkSayfaMi = page == 0,
                SonSayfaMi = page == list.Count() / itemsPerPage,
                SayfaNo = page,
                SayfaKayitSayisi = tmpContent.Count(),
                SayfaAdet = itemsPerPage,
                Siralama = "",//sorting,
                ToplamKayitSayisi = list.Count(),
                ToplamSayfaSayisi = (list.Count() / itemsPerPage + 1),
                Veri = sayfaUserRoleler
            };
            return sayfalama;
        }

        public async Task<bool> UserRoleControlAsync(int userId, int[] yetkiIds)
        {
            var UserRoleler = await GetUserRoleAsync(userId);
            if (UserRoleler == null)
                return false;
            var yetkiler = await GetAuthorizeAsync(UserRoleler);
            return yetkiler != null && yetkiler.Any(yetkiIds.Contains);
        }

        public async Task<string[]> UserAuthorizeAsync(int userId)
        {
            var UserRoleler = await GetUserRoleAsync(userId);
            if (UserRoleler == null)
                return new string[] { };
            var yetkiler = await GetAuthorizeAsync(UserRoleler);
            if (yetkiler == null)
            {
                return new string[] { };
            }

            return await GetAuthorizeNamesAsync(yetkiler);
        }

        public async Task<IEnumerable<UserRoleDTO>> GetByUserRolesAsync(Guid UserGuid)
        {
            var UserRoleleri = await _userRoleRepository.GetListAsync(
                x => x.Deleted == null && x.User.Guid == UserGuid,
                include: y => y.Include(c => c.User)
                    .Include(b => b.Role));
            var dtos = ModelMapper.Mapper.Map<List<UserRoleDTO>>(UserRoleleri);
            return dtos;
        }
        public async Task<IEnumerable<UserRoleDTO>> GetByUserIdRolesAsync(int userId)
        {
            var UserRoleleri = await _userRoleRepository.GetListAsync(
                x => x.Deleted == null && x.User.Id == userId,
                include: y => y.Include(c => c.User)
                    .Include(b => b.Role));
            var dtos = ModelMapper.Mapper.Map<List<UserRoleDTO>>(UserRoleleri);
            return dtos;
        }

        public async Task<UserRoleDTO> GetByUserIdAndRoleIdAsync(int userId, int RoleId)
        {
            var entity = await _userRoleRepository.GetFirstOrDefaultAsync(x => x.Deleted == null && x.UserId == userId && x.RoleId == RoleId);
            return ModelMapper.Mapper.Map<UserRoleDTO>(entity);
        }


        private async Task<int[]> GetUserRoleAsync(int userId)
        {
            var list = await _userRoleRepository.GetListAsync(x => x.UserId == userId && x.Deleted == null);
            if (list.Any())
            {
                return list.Select(x => x.RoleId).ToArray();
            }

            return null;
        }

        private async Task<int[]> GetAuthorizeAsync(int[] roles)
        {
            if (roles == null || roles.Length <= 0) return null;
            var yetkiler = await _roleAuthorizationRepository.GetListAsync(x => roles.Contains(x.RoleId) && x.Deleted == null);
            return yetkiler.Any() ? yetkiler.Select(x => x.AuthorizationId).ToArray() : null;
        }

        private async Task<string[]> GetAuthorizeNamesAsync(int[] yetkiler)
        {
            if (yetkiler == null || yetkiler.Length <= 0) return new string[] { };
            var yetkiList = await _authorizationRepository.GetListAsync(x => yetkiler.Contains(x.Code));
            return yetkiList.Any() ? yetkiList.Select(x => x.Name).Distinct().ToArray() : new string[] { };
        }
    }
}
