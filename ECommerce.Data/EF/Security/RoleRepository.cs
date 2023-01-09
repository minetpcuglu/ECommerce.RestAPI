using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data.EF.Mappings;
using ECommerce.Domain.Entities.Security;
using ECommerce.Domain.Infrastructure;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;
using ECommerce.Domain.Infrastructure.Contexts;

namespace ECommerce.Data.EF.Security
{
    public class RoleRepository : IRoleRepository
    {

        private readonly IRepository<Role> _roleRepository;
        private readonly DatabaseContext _context;

        public RoleRepository(IRepository<Role> roleRepository, DatabaseContext context)
        {
            _roleRepository = roleRepository;
            _context = context;
        }


        public async Task<RoleDTO> GetByIdAsync(int id)
        {
            var entity = await _roleRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Id == id);
            return ModelMapper.Mapper.Map<RoleDTO>(entity);
        }

        public async Task<RoleDTO> GetByGuidAsync(Guid guid)
        {
            var entity = await _roleRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Guid == guid);
            return ModelMapper.Mapper.Map<RoleDTO>(entity);
        }

        public async Task<int> CreateAsync(RoleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Role>(dto);
            entity.EntityState = EntityState.Added;
            return await _roleRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _roleRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            await _roleRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(Guid guid)
        {
            var entity = await _roleRepository.GetFirstOrDefaultAsync(x => x.Guid == guid);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            await _roleRepository.SaveAsync(entity);
        }

        public async Task<int> UpdateAsync(RoleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Role>(dto);
            entity.EntityState = EntityState.Modified;
            return await _roleRepository.SaveAsync(entity);
        }


        public async Task<SayfalamaDTO<RoleDTO>> GetRolePagingAsync(RoleCO co)
        {

            IQueryable<Role> list = _context.Roles.AsNoTracking().Where(x => x.Deleted == null);

            if (!string.IsNullOrEmpty(co.Name))
            {
                list = list.Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Name, "%" + co.Name + "%"));
            }


            if (!string.IsNullOrEmpty(co.Description))
            {
                list = list.Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Description, "%" + co.Description + "%"));
            }

            var page = co.SayfaNo;
            var itemsPerPage = co.SayfaAdet < 1 ? 20 : co.SayfaAdet;
            var skip = page * itemsPerPage;

            var tmpContent = await list.OrderByDescending(x => x.CreationTime).ThenBy(x => x.Name)
                .Skip(skip).Take(itemsPerPage).ToListAsync();
            var sayfaRoller = ModelMapper.Mapper.Map<IList<RoleDTO>>(tmpContent);


            var sayfalama = new SayfalamaDTO<RoleDTO>()
            {
                IlkSayfaMi = page == 0,
                SonSayfaMi = page == list.Count() / itemsPerPage,
                SayfaNo = page,
                SayfaKayitSayisi = tmpContent.Count(),
                SayfaAdet = itemsPerPage,
                Siralama = "",//sorting,
                ToplamKayitSayisi = list.Count(),
                ToplamSayfaSayisi = list.Count() / itemsPerPage + 1,
                Veri = sayfaRoller
            };
            return sayfalama;
        }
    }
}
