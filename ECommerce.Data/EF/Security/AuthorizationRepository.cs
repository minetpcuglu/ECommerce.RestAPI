using System;
using System.Threading.Tasks;
using ECommerce.Data.EF.Mappings;
using ECommerce.Domain.Entities.Security;
using ECommerce.Domain.Infrastructure;
using ECommerce.Shared.DataTransferObjects.Security;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.EF.Security
{
    public class AuthorizationRepository : IAuthorizationRepository
    {

        private readonly IRepository<Authorization> _authorizationRepository;
        //private readonly DatabaseContext _context;

        public AuthorizationRepository(IRepository<Authorization> authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
           
        }


        public async Task<AuthorizationDTO> GetByIdAsync(int kod)
        {
            var entity = await _authorizationRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Code == kod);
            return ModelMapper.Mapper.Map<AuthorizationDTO>(entity);
        }

        public async Task<AuthorizationDTO> GetByGuidAsync(Guid guid)
        {
            var entity = await _authorizationRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Guid == guid);
            return ModelMapper.Mapper.Map<AuthorizationDTO>(entity);
        }

        public async Task<int> CreateAsync(AuthorizationDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Authorization>(dto);
            entity.CreationTime = DateTime.Now;
            return await _authorizationRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(Guid guid)
        {
            var entity = await _authorizationRepository.GetFirstOrDefaultAsync(x => x.Guid == guid);
            entity.Deleted = Guid.NewGuid();
            entity.IsActive = false;
            entity.DeletionZamani = DateTime.Now;
            await _authorizationRepository.SaveAsync(entity);
        }

        public async Task<int> UpdateAsync(AuthorizationDTO dto)
        {

            var entity = ModelMapper.Mapper.Map<Authorization>(dto);
            entity.ModificationTime = DateTime.Now;
            return await _authorizationRepository.SaveAsync(entity);
        }

        public async Task<int[]> GetAuthorizationIdsByAuthorizationNameAsync(string[] authorizationName)
        {
            var entities = (await _authorizationRepository.GetListAsync(u => u.Deleted == null && authorizationName.Contains(u.Name))).ToList();
            var ids = entities.Select(f => f.Code).ToArray();
            return ids;
        }
    }
}
