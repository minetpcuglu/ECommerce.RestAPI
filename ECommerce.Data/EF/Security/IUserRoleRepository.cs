using ECommerce.Shared.DataTransferObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared.CriteriaObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public interface IUserRoleRepository : ICrud<UserRoleDTO>
    {
        Task<SayfalamaDTO<UserRoleDTO>> GetUserRolePagingAsync(UserRoleCO co);
        Task<bool> UserRoleControlAsync(int kullaniciId, int[] yetkiIds);
        Task<string[]> UserAuthorizeAsync(int kullaniciId);
        Task<IEnumerable<UserRoleDTO>> GetByUserRolesAsync(Guid kullaniciGuid);
        Task<IEnumerable<UserRoleDTO>> GetByUserIdRolesAsync(int kullaniciId);
        Task<UserRoleDTO> GetByUserIdAndRoleIdAsync(int kullaniciId, int rolId);
    }
}
