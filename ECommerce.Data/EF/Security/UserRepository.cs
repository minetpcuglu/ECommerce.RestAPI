using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.Extensions.Exceptions;
using ECommerce.Data.EF.Mappings;
using ECommerce.Domain.Entities.Security;
using ECommerce.Domain.Infrastructure;
using ECommerce.Localization;
using ECommerce.Shared.CriteriaObjects;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;
using ECommerce.Domain.Entities.Security;
using ECommerce.Domain.Infrastructure.Contexts;
using Efor.Core.Extensions.Exceptions;
using ECommerce.Shared.CriteriaObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;
        private readonly DatabaseContext _context;

        public UserRepository(IRepository<User> userRepository,
            DatabaseContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Id == id);
                return ModelMapper.Mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<int> CreateAsync(UserDTO dto)
        {
            try
            {
                var entity = ModelMapper.Mapper.Map<User>(dto);
                entity.EntityState = EntityState.Added;
                return await _userRepository.SaveAsync(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _userRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.IsActive = false;
            entity.EntityState = EntityState.Modified;
            await _userRepository.SaveAsync(entity);
        }

        public async Task DeleteAsync(Guid guid)
        {
            var entity = await _userRepository.GetFirstOrDefaultAsync(x => x.Guid == guid);
            entity.Deleted = Guid.NewGuid();
            entity.DeletionZamani = DateTime.Now;
            entity.IsActive=false;
            entity.EntityState = EntityState.Modified;
            await _userRepository.SaveAsync(entity);
        }

        public async Task<int> UpdateAsync(UserDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<User>(dto);
            entity.ModificationTime = DateTime.Now;
            entity.EntityState = EntityState.Modified;
            return await _userRepository.SaveAsync(entity);
        }


        public async Task<UserDTO> GetUserLoginAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {  
                throw new ECommerceBadRequestException(@Resource.kullaniciAdiBelirtiniz);
            }

            var user = await _userRepository.GetFirstOrDefaultAsync(
                x => x.UserName != null && x.UserName == userName);
            return ModelMapper.Mapper.Map<UserDTO>(user);
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<UserDTO> GetByGuidAsync(Guid guid)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Deleted == null && u.Guid == guid);
                   
          
            if (user == null)
            {
                throw new ECommerceNotFoundException(@Resource.kullaniciBulunamadi);
            }
            var dto = ModelMapper.Mapper.Map<UserDTO>(user);
            return dto;
        }

        public async Task<SayfalamaDTO<UserDTO>> GetUserPagingAsync(UserCO co)
        {
            var users = _context.Users.AsNoTracking().Where(x => x.Deleted == null && 
                                                              x.Name!= null && Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Name, "%" + co.Name + "%") &&
                                                              x.UserName != null && Microsoft.EntityFrameworkCore.EF.Functions.Like(x.UserName, "%" + co.UserName + "%") &&
                                                               x.Surname != null && Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Surname, "%" + co.Surname + "%"));

            if (co.IsActive.HasValue)
            {
                users = users.Where(x => x.IsActive == co.IsActive);
            }
           

            var page = co.SayfaNo;
            var itemsPerPage = 0;
            itemsPerPage = co.SayfaAdet < 1 ? 20 : co.SayfaAdet;
            var skip = page * itemsPerPage;

            var tmpContent = await users.OrderBy(x => x.Name).ThenBy(x => x.Surname).ThenBy(x => x.Name)
                .Skip(skip).Take(itemsPerPage).
             ToListAsync();
            var sayfausers = ModelMapper.Mapper.Map<IList<UserDTO>>(tmpContent);


            var sayfalama = new SayfalamaDTO<UserDTO>()
            {
                IlkSayfaMi = page == 0,
                SonSayfaMi = page == users.Count() / itemsPerPage,
                SayfaNo = page,
                SayfaKayitSayisi = tmpContent.Count(),
                SayfaAdet = itemsPerPage,
                Siralama = "",//sorting,
                ToplamKayitSayisi = users.Count(),
                ToplamSayfaSayisi = users.Count() / itemsPerPage + 1,
                Veri = sayfausers
            };
            return sayfalama;
        }

    }
}
