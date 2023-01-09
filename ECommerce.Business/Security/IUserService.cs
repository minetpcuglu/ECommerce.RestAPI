using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Shared.Request.Security;
using ECommerce.Core.Utilities.Results;
using ECommerce.Shared.CriteriaObjects;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Business.Security
{
    public interface IUserService
    {
        Task<IDataResult<UserTokenDTO>> GetUserLoginAsync(LoginCO co);
        Task<IDataResult<SayfalamaDTO<UserDTO>>> GetUserPagingAsync(UserCO co);
        Task<IDataResult<UserDTO>> GetByGuid(Guid kullaniciGuid);
        Task<IDataResult<UserDTO>> GetById(int kullaniciId);
        Task<IDataResult<UserDTO>> AddUserAsync(UserRequest request);
        Task<IDataResult<UserDTO>> UpdateUserAsync(Guid kullaniciGuid, UserRequest request);
        Task<IResult> DeleteUserAsync(Guid kullaniciGuid);

        //Task<IResult> SifremiUnuttum(SifremiUnuttumCO co);
        //Task<IResult> SifreYenile(SifreYenileCO co);

 
    }
}
