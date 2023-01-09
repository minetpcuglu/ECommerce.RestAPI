using   ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Data.EF.Security
{
    public interface IAuthorizationRepository : ICrud<AuthorizationDTO>
    {
        Task<int[]> GetAuthorizationIdsByAuthorizationNameAsync(string[] AuthorizationName);
    }
}
