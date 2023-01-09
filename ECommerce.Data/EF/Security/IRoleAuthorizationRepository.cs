using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Data.EF;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public interface IRoleAuthorizationRepository : ICrud<RoleAuthorizationDTO>
    {
        Task<List<RoleAuthorizationDTO>> GetAllAuthorizationAsync(Guid rolGuid);
        Task<RoleAuthorizationDTO> GetByRoleGuidAndAuthorizationGuidAsync(Guid rolGuid, Guid yetkiGuid);
    }
}
