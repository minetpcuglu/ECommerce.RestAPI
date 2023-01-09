using System.Threading.Tasks;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public interface IRoleRepository : ICrud<RoleDTO>
    {
        Task<SayfalamaDTO<RoleDTO>> GetRolePagingAsync(RoleCO co);
    }
}
