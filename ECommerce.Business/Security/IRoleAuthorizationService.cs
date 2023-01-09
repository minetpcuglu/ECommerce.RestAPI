using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Core.Utilities.Results;
using ECommerce.Shared.DataTransferObjects.Security;
using ECommerce.Shared.Request;
using ECommerce.Shared.Request.Security;

namespace ECommerce.Business.Security
{
    public interface IRoleAuthorizationService
    {
        Task<DataResult<RoleAuthorizationDTO>> GetByIdAsync(int id);
        Task<DataResult<RoleAuthorizationDTO>> GetByGuidAsync(Guid guid);
        Task<DataResult<RoleAuthorizationDTO>> CreateAsync(RoleAuthorizationRequest dto);
        Task<IResult> DeleteAsync(Guid guid);
        Task<DataResult<RoleAuthorizationDTO>> UpdateAsync(Guid rolYetkiGuid, RoleAuthorizationRequest request);
        Task<DataResult<List<RoleAuthorizationDTO>>> GetAllAuthorizationAsync(Guid rolGuid);
        Task<DataResult<RoleAuthorizationDTO>> GetByRoleGuidAndAuthorizationGuidAsync(Guid rolGuid, Guid yetkiGuid);
        Task<IResult> RoleAuthorizationSaveAsync(List<RoleAuthorizationRequest> requests);
    }
}
