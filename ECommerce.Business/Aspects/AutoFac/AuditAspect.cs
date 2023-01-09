using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using ECommerce.Core.CrossCuttingConcerns.Logging;
using ECommerce.Core.Utilities.Interceptors;
using ECommerce.Core.Utilities.IoC;
using ECommerce.Data.AUDIT;
using ECommerce.Shared.DataTransferObjects.Audit;

namespace ECommerce.Business.Aspects.AutoFac
{
    public class AuditAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        private readonly IAuditRepository _auditRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public AuditAspect()
        {
               _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _auditRepository = ServiceTool.ServiceProvider.GetService<IAuditRepository>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine(
                    $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --> {_stopwatch.Elapsed.TotalSeconds}");
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var jsonString = System.Text.Json.JsonSerializer.Serialize(GetLogDetail(invocation), options);
            var userId = 0;

            try
            {
                var claimsIdentity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;
                userId = Convert.ToInt32(claimsIdentity?.FindFirst("userId")?.Value);

            }
            catch (Exception e)
            { 
            }

            var userAgent = _httpContextAccessor?.HttpContext?.Request?.Headers[HeaderNames.UserAgent].ToString();


            var request = _httpContextAccessor?.HttpContext?.Request;
            UriBuilder uriBuilder = new UriBuilder();
            if (request != null)
            {
                uriBuilder.Scheme = request.Scheme;
                uriBuilder.Host = request.Host.Host;
                uriBuilder.Path = request.Path.ToString();
                uriBuilder.Query = request.QueryString.ToString();
            }
            string host = _httpContextAccessor?.HttpContext?.Request?.Host.Value;
            var url = _httpContextAccessor?.HttpContext?.Request?.Body;



            Guid? sirketGuid = null;
            Guid? subeGuid = null;

            try
            {
                if (_httpContextAccessor?.HttpContext?.Request != null)
                {
                    if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("SirketGuid", out var traceValue))
                    {
                        sirketGuid = Guid.Parse(traceValue);
                    }
                    if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("SubeGuid", out var traceValue2))
                    {
                        subeGuid = Guid.Parse(traceValue2);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }


            var dto = new AuditDTO()
            {
                Guid = Guid.NewGuid(),
                EklenmeZamani = DateTime.Now,
                UserName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Anonymous",
                IpAdres = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                IslemSuresi = Convert.ToDecimal(_stopwatch.Elapsed.TotalMilliseconds),
                Detay = jsonString,
                UserId = userId==0?new int?(): userId,
                MethodAdi = invocation.Method.Name,
                SinifAdi = invocation.TargetType.FullName,
                Tarayici = userAgent,
                Uri = uriBuilder.Uri.ToString(),
                Host = host,
                HttpMethod = request?.Method,
                Protocol = request?.Protocol,
                SirketGuid = sirketGuid,
                SubeGuid = subeGuid
            };
            _stopwatch.Reset();
            _auditRepository.SaveAuditAsync(dto); 

        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }

            var logDetail = new LogDetail()
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
