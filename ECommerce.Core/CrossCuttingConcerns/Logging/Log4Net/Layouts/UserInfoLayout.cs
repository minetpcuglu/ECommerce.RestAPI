using System.IO;
using System.Security.Claims;
using log4net.Core;
using log4net.Layout;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Core.Utilities.IoC;

namespace ECommerce.Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    //public class UserInfoLayout : LayoutSkeleton
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;

    //    public UserInfoLayout()
    //    {
    //        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    //    }
    //    public override void ActivateOptions()
    //    {
            
    //    }

    //    public override void Format(TextWriter writer, LoggingEvent loggingEvent)
    //    {
    //        var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
    //        var kullaniciAdi = claimsIdentity?.FindFirst("KullaniciAdi")?.Value;
    //        //var json = JsonConvert.SerializeObject(kullaniciAdi, Formatting.Indented);
    //        writer.WriteLine(kullaniciAdi);
    //    }
    //}
}
