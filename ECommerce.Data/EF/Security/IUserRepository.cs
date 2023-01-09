using System.Threading.Tasks;
using ECommerce.Shared.CriteriaObjects;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public interface IUserRepository : ICrud<UserDTO>
    {
        Task<UserDTO> GetUserLoginAsync(string userName);
        Task<SayfalamaDTO<UserDTO>> GetUserPagingAsync(UserCO co);
    }
}
