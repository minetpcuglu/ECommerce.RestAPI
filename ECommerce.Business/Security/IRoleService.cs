using System;
using System.Threading.Tasks;
using ECommerce.Shared.Request.Security;
using ECommerce.Core.Utilities.Results;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Business.Security
{
    public interface IRoleService
    {
        Task<DataResult<RoleDTO>> GetByIdAsync(int id);
        Task<DataResult<SayfalamaDTO<RoleDTO>>> GetRolePagingAsync(RoleCO co);
        Task<DataResult<RoleDTO>> GetByGuidAsync(Guid guid);
        Task<DataResult<RoleDTO>> CreateAsync(RoleRequest request);
        Task<IResult> DeleteAsync(Guid guid);
        Task<DataResult<RoleDTO>> UpdateAsync(Guid rolGuid, RoleRequest request);
    }
}
