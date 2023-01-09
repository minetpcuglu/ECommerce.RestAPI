using System;
using System.Threading.Tasks;
using ECommerce.Core.Utilities.Results;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Business.Security
{
    public interface IAuthorizationService
    {
        Task<IDataResult<AuthorizationDTO>> GetByIdAsync(int id);
        Task<IDataResult<AuthorizationDTO>> GetByGuidAsync(Guid guid);
        Task<IDataResult<AuthorizationDTO>> UpdateAsync(AuthorizationDTO dto);

    }
}
