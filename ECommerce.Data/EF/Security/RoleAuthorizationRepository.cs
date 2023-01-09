using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data.EF.Mappings;
using ECommerce.Domain.Entities.Security;
using ECommerce.Domain.Infrastructure.Contexts;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public class RoleAuthorizationRepository : IRoleAuthorizationRepository
    {

        private readonly IRepository<RoleAuthorization> _roleAuthorizationRepository;
        private readonly IRepository<Authorization> _authorizationRepository;
        private readonly DatabaseContext _context;

        public RoleAuthorizationRepository(IRepository<RoleAuthorization> roleAuthorizationRepository, DatabaseContext context, IRepository<Authorization> authorizationRepository)
        {
            _roleAuthorizationRepository = roleAuthorizationRepository;
            _context = context;
            _authorizationRepository = authorizationRepository;
        }


        public async Task<RoleAuthorizationDTO> GetByIdAsync(int id)
        {
            var entity = await _roleAuthorizationRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Id == id);
            return ModelMapper.Mapper.Map<RoleAuthorizationDTO>(entity);
        }

        public async Task<RoleAuthorizationDTO> GetByGuidAsync(Guid guid)
        {
            var entity = await _roleAuthorizationRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Guid == guid);
            return ModelMapper.Mapper.Map<RoleAuthorizationDTO>(entity);
        }

        public async Task<int> CreateAsync(RoleAuthorizationDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<RoleAuthorization>(dto);
            entity.EntityState = EntityState.Added;
            return await _roleAuthorizationRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _roleAuthorizationRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            await _roleAuthorizationRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(Guid guid)
        {
            var entity = await _roleAuthorizationRepository.GetFirstOrDefaultAsync(x => x.Guid == guid);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            await _roleAuthorizationRepository.SaveAsync(entity);
        }

        public async Task<int> UpdateAsync(RoleAuthorizationDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<RoleAuthorization>(dto);
            entity.EntityState = EntityState.Modified;
            return await _roleAuthorizationRepository.SaveAsync(entity);
        }

        public async Task<List<RoleAuthorizationDTO>> GetAllAuthorizationAsync(Guid rolGuid)
        {
            List<Authorization> yetkiler = await _authorizationRepository.GetListAsync(x => x.Deleted == null,
                orderBy: o => o.OrderBy(b => b.Descripton));
            List<RoleAuthorization> rolYetkiler = await _roleAuthorizationRepository.GetListAsync(x => x.Deleted == null && x.Role.Guid == rolGuid,
                include: y => y.Include(c => c.Role)
                    .Include(b => b.Authorization));

            var list = new List<RoleAuthorizationDTO>();
            foreach (var item in yetkiler)
            {
                var varMi = rolYetkiler.FirstOrDefault(x => x.AuthorizationId== item.Code);
                if (varMi == null)
                {
                    list.Add(new RoleAuthorizationDTO()
                    {
                        Role = new RoleDTO()
                        {
                            Guid = rolGuid
                        },
                        AuthorizationId = item.Code,
                        Authorization = new AuthorizationDTO()
                        {
                            Guid = item.Guid,
                            Code = item.Code,
                            Descripton = item.Descripton,
                            Name = item.Name,
                        }
                    });
                }
                else
                {
                    var dto = ModelMapper.Mapper.Map<RoleAuthorizationDTO>(varMi);
                    dto.Secili = true;
                    list.Add(dto);
                }

            }

            return list;
        }
        public async Task<RoleAuthorizationDTO> GetByRoleGuidAndAuthorizationGuidAsync(Guid rolGuid, Guid yetkiGuid)
        {
            var entity = await _roleAuthorizationRepository.GetFirstOrDefaultAsync(x => x.Deleted == null && x.Role.Guid == rolGuid && x.Authorization.Guid == yetkiGuid,
                include: i => i.Include(v => v.Role).Include(s => s.Authorization));
            return ModelMapper.Mapper.Map<RoleAuthorizationDTO>(entity);
        }

    }
}
