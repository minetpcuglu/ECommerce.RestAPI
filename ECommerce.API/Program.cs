using Autofac.Extensions.DependencyInjection;
using Autofac;
using ECommerce.Core.DependencyResolvers;
using ECommerce.Core.Extensions;
using ECommerce.Core.Utilities.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using ECommerce.Business.DependencyResolvers.Autofac;
using ECommerce.Data.AUDIT;
using ECommerce.API.Configuration;
using ECommerce.API.Schedulers;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(b =>
    {
        b.RegisterModule(new AutofacBusinessModule());

    });
Bootstrapper.InitializeContainer(builder.Services);

// Configure the HTTP request pipeline.

//x => x.AllowEmptyInputInBodyModelBinding = true
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
//builder.Services.AddMyDbContext(builder.Configuration);
builder.Services.AddMyCors(builder.Configuration);

//Jwt Authentication
builder.Services.AddMyJwtAuthentication(builder.Configuration);

//Swagger
//builder.Services.AddSwaggerGen();
builder.Services.AddMySwagger(builder.Configuration);

//Service
//builder.Services.AddMyServices();

builder.Services.AddSingleton<IAuditRepository, AuditRepository>();
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

//var bildirimSecenek = builder.Configuration.GetSection("Notification").Get<BildirimSecenek>();
//builder.Services.AddHttpClient("FcmClient", c => c.BaseAddress = new System.Uri(bildirimSecenek.FireBasePushNotificationsUrl));


//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(1800);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

// Add Quartz builder.Services
builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();


//// Add our job
//var jobIntervalInSecond = Convert.ToInt32(builder.Configuration["JobIntervalInSecond"] ?? "300");
//builder.Services.AddSingleton<EticaretJob>();
//builder.Services.AddSingleton(new JobSchedule(
//    jobType: typeof(EticaretJob),
//    intervalInSecond: jobIntervalInSecond
//));
// cronExpression: "0 0/10 * 1/1 * ? *"


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.UseRouting();
app.UseMyCors(builder.Configuration);

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMySwagger(builder.Configuration);


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseSession();

app.Run();

