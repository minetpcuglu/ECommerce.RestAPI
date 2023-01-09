using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Shared.Request.Security;
using ECommerce.Core.Utilities.Results;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Business.Security
{
    public interface IUserRoleService
    {
        Task<DataResult<UserRoleDTO>> GetByIdAsync(int id);
        Task<DataResult<UserRoleDTO>> GetByGuidAsync(Guid guid);
        Task<DataResult<List<UserRoleDTO>>> GetByUserRolesAsync(Guid kullaniciGuid);
        Task<DataResult<List<UserRoleDTO>>> GetByUserIdRolesAsync(int kullaniciId);
        Task<DataResult<UserRoleDTO>> GetByUserIdAndRoleIdAsync(int kullaniciId, int rolId);
        Task<DataResult<UserRoleDTO>> CreateAsync(UserRoleRequest request);
        Task<IResult> DeleteAsync(Guid guid);
        Task<DataResult<UserRoleDTO>> UpdateAsync(Guid UserRoleGuid, UserRoleRequest request);
        Task<DataResult<bool>> UserRoleControlAsync(int kullaniciId, int[] yetkiIds);
        Task<DataResult<string[]>> UserAuthorizationsAsync(int kullaniciId);
        Task<DataResult<SayfalamaDTO<UserRoleDTO>>> GetUserRolePagingAsync(UserRoleCO co);
    }
}
