using System.Linq;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Core.Extensions;
using ECommerce.Core.Extensions.Exceptions;
using ECommerce.Core.Localization;
using ECommerce.Core.Utilities.Interceptors;
using ECommerce.Core.Utilities.IoC;
using ECommerce.Data.EF.Security;
using System.Security.Claims;
using Autofac.Core.Lifetime;
using Bootstrapper = ECommerce.Business.DependencyResolvers.Autofac.Bootstrapper;
using Autofac;

namespace ECommerce.Business.Aspects.AutoFac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private string _rolesStr;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRoleRepository _userRoleRepository;
        private IAuthorizationRepository _authorizationRepository;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _rolesStr = roles;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //_yetkiRepository = ServiceTool.ServiceProvider.GetService<IYetkiRepository>();

            //_kullaniciRolRepository = ServiceTool.ServiceProvider.GetService<IKullaniciRolRepository>();
            _userRoleRepository = Bootstrapper.Container.Resolve<IUserRoleRepository>();

            _authorizationRepository = Bootstrapper.Container.Resolve<IAuthorizationRepository>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            //var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            //if (_roles.Any(role => roleClaims.Contains(role)))
            //{
            //    return;
            //}

            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var kullaniciId = Convert.ToInt32(claimsIdentity?.FindFirst("KullaniciId")?.Value);

            var yetki = _authorizationRepository.GetAuthorizationIdsByAuthorizationNameAsync(_roles);
            yetki.Wait(); // Blocks current thread until GetFooAsync task completes
            // For pedagogical use only: in general, don't do this!
            var result = yetki.Result;


            var kullaniciRol = _userRoleRepository.UserRoleControlAsync(kullaniciId, result);

            kullaniciRol.Wait(); // Blocks current thread until GetFooAsync task completes
            // For pedagogical use only: in general, don't do this!
            var kullaniciRolBool = kullaniciRol.Result;
            if (kullaniciRolBool)
            {
                return;
            }
            throw new ECommerceForbiddenException(@AspectMessages.AuthorizationDenied + " : " + _rolesStr);
        }
    }
}
