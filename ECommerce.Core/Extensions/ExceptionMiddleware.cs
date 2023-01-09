using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.CrossCuttingConcerns.Logging.Log4Net;
using ECommerce.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ECommerce.Core.Extensions;
using ECommerce.Core.Extensions.Exceptions;
using ECommerce.Core.Localization;
using ECommerce.Core.Utilities.IoC;
using Efor.Core.Extensions.Exceptions;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;



namespace ECommerce.Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private LoggerServiceBase _loggerServiceBase;
        private Stopwatch _stopwatch;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            Type loggerService = typeof(DatabaseLogger);
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(@AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);

            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();

        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                _stopwatch.Start();
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                // await HandleExceptionAsync(httpContext, e, e.Message);
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "ValidationException");
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Error");
                //_loggerServiceBase.Error(ex.Message + " : " + ex.InnerException);

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
            catch (ECommerceBadRequestException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "BadRequest");
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Error");
                //_loggerServiceBase.Error(ex.Message + " : " + ex.InnerException);

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
            catch (ECommerceNotFoundException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "NotFound");
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Error");
                //_loggerServiceBase.Error(ex.Message + " : " + ex.InnerException);

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
            catch (ECommerceNotImplementedException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "NotImplemented");
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Error");
                //_loggerServiceBase.Error(ex.Message + " : " + ex.InnerException);

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
            catch (ECommerceRequestEntityTooLargeException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "RequestEntityTooLarge");
                httpContext.Response.StatusCode = (int)HttpStatusCode.RequestEntityTooLarge;

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Error");
                //_loggerServiceBase.Error(ex.Message + " : " + ex.InnerException);

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());

            }
            catch (ECommerceForbiddenException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Error");
                //_loggerServiceBase.Error(ex.Message + " : " + ex.InnerException);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "Forbidden");
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());

            }
            catch (ECommerceConstraintException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                _loggerServiceBase.Error(ex.Message + " : " + ex.InnerException?.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "BadRequest");
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                }.ToString());

            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                await Log4NetCustomFieldAsync(httpContext, ex.Message + " : " + ex.InnerException, "Fatal");



                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.Add("exception", "InternalServerError");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Internal Server Error. Lütfen hata detayı için LOG tablosuna bakınız!"
                }.ToString());
            }
        }


        private async Task Log4NetCustomFieldAsync(HttpContext httpContext, string message, string type)
        {
            //var httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            try
            {


                if (httpContext != null)
                {
                    var claimsIdentity = httpContext.User?.Identity as ClaimsIdentity ?? null;
                    var kullaniciAdi = claimsIdentity?.FindFirst("KullaniciAdi")?.Value;
                    var kullaniciId = Convert.ToInt32(claimsIdentity?.FindFirst("KullaniciId")?.Value);



                    var userAgent = httpContext?.Request?.Headers[HeaderNames.UserAgent].ToString();


                    var request = httpContext.Request;
                    UriBuilder uriBuilder = new UriBuilder();
                    if (request != null)
                    {
                        uriBuilder.Scheme = request.Scheme;
                        uriBuilder.Host = request.Host.Host;
                        uriBuilder.Path = request.Path.ToString();
                        uriBuilder.Query = request.QueryString.ToString();
                    }

                    string host = httpContext.Request?.Host.Value;
                    var url = httpContext?.Request?.Body;



                    Guid? companyGuid = null;
                    Guid? branchGuid = null;

                    try
                    {
                        if (httpContext.Request != null)
                        {
                            if (httpContext.Request.Headers.TryGetValue("SirketGuid",
                                out var traceValue))
                            {
                                companyGuid = Guid.Parse(traceValue);
                            }

                            if (httpContext.Request.Headers.TryGetValue("SubeGuid",
                                out var traceValue2))
                            {
                                branchGuid = Guid.Parse(traceValue2);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    if (httpContext?.Request?.Body != null)
                    {
                        string bodyAsText =
                            await new StreamReader(httpContext?.Request?.Body).ReadToEndAsync();
                        var injectedRequestStream = new MemoryStream();
                        var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                        injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                        injectedRequestStream.Seek(0, SeekOrigin.Begin);
                        httpContext.Request.Body = injectedRequestStream;


                        LogicalThreadContext.Properties["Body"] = bodyAsText;
                    }


                    if (kullaniciId != 0)
                    {
                        LogicalThreadContext.Properties["UserId"] = kullaniciId;
                    }

                    if (!string.IsNullOrEmpty(kullaniciAdi))
                    {
                        LogicalThreadContext.Properties["Username"] = kullaniciAdi;
                    }

                    if (companyGuid.HasValue)
                    {
                        LogicalThreadContext.Properties["CompanyGuid"] = companyGuid;
                    }

                    if (branchGuid.HasValue)
                    {
                        LogicalThreadContext.Properties["BranchGuid"] = branchGuid;
                    }

                    if (!string.IsNullOrEmpty(request?.Method))
                    {
                        LogicalThreadContext.Properties["HttpMethod"] = request?.Method;
                    }

                    if (!string.IsNullOrEmpty(request?.Protocol))
                    {
                        LogicalThreadContext.Properties["Protocol"] = request?.Protocol;
                    }

                    if (!string.IsNullOrEmpty(host))
                    {
                        LogicalThreadContext.Properties["Host"] = host;
                    }

                    if (!string.IsNullOrEmpty(uriBuilder.Uri.ToString()))
                    {
                        LogicalThreadContext.Properties["Uri"] = uriBuilder.Uri.ToString();
                    }


                    if (!string.IsNullOrEmpty(userAgent))
                    {
                        LogicalThreadContext.Properties["UserAgent"] = userAgent;
                    }


                    if (!string.IsNullOrEmpty(httpContext?.Connection?.RemoteIpAddress?.ToString()))
                    {
                        LogicalThreadContext.Properties["IpAddress"] =
                            httpContext?.Connection?.RemoteIpAddress?.ToString();
                    }
                }




                _stopwatch.Stop();
                LogicalThreadContext.Properties["ElapsedTime"] = Convert.ToDecimal(_stopwatch.Elapsed.TotalMilliseconds);
                _stopwatch.Reset();

                if (type == "Fatal")
                {
                    _loggerServiceBase.Fatal(message);
                }
                else if (type == "Error")
                {
                    _loggerServiceBase.Fatal(message);
                }
                else
                {
                    _loggerServiceBase.Warn(message);
                }
            }
            catch (Exception ex)
            {

                var e = ex;
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception, string message)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }

    }
}
