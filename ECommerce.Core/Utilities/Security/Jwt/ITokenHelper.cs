using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessTokenDTO TokenOlustur(UserDTO kullanici,string[] yetkiler);
    }
}
