using Sigo.WebApi.DataProvider;
using Sigo.WebApi.Hubs;
using Sigo.WebApi.Middlewares;
using Sigo.WebApi.Services;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Sigo.WebApi
{
    /// <summary>
    /// 配置服务和应用的请求管道
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// <see cref="IConfiguration"/>配置文件对象
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 构造<see cref="Startup"/>对象
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/>配置文件对象</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置应用服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //注册log4net服务
            services.AddSingleton((s) =>
            {
                return LogManager.GetLogger(SigoConst.RepositoryName, SigoConst.LogName);
            });

            //注册JWT认证服务
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    Configuration.Bind("JwtSetting", options);
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(10),
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtSetting:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("JwtSetting:Audience"),
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSetting:SecurityKey")))
                    };
                });

            //注册WebSocket客户端管理服务
            services.AddWebSocketClientManagerServices();
            //注册数据库交互服务
            services.AddSqlServerDataProvider(Configuration.GetConnectionString("Sigo"));
            //注册资源文件相关服务
            services.AddResourceServices();
            //注册业务服务
            services.AddBizServices();

            services.AddControllers();

            services.AddMvc();

            //添加跨域策略
            services.AddCors(options =>
            {
                options.AddPolicy(SigoConst.PolicyName, builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
                options.AddPolicy(SigoConst.SignalRPolicyName, builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(Configuration.GetSection("SignalR:Origins").Get<string[]>());
                });
            });

            //注册Swagger文档服务
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "ASP.NET Core WebApi Demo", Version = "v1", Description = "ASP.NET Core WebApi Demo接口" });
                options.SwaggerDoc("v2", new OpenApiInfo() { Title = "ASP.NET Core WebApi Demo", Version = "v2", Description = "ASP.NET Core WebApi Demo接口" });

                //开启Swagger身份认证
                options.OperationFilter<SecurityRequirementsOperationFilter>(JwtBearerDefaults.AuthenticationScheme);
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT授权，在下框中输入 Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            });

            //添加SignalR服务
            services.AddSignalR();

            services.AddLogging();
        }

        /// <summary>
        /// 配置应用的HTTP处理请求管道
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <param name="env"><see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("Run in develoment environment.");
                app.UseExceptionMiddleware();
            }
            else
            {
                app.UseExceptionMiddleware();
            }

            //启用WebSocket监听
            var webSocketOptions = BuildWebSocketOptions();
            app.UseWebSockets();
            app.UseWebSocketMiddleware();

            //启用默认文档设置
            var defaultFileOptions = new DefaultFilesOptions();
            defaultFileOptions.DefaultFileNames.Add("test.html");
            app.UseDefaultFiles(defaultFileOptions);

            //启用静态文件设置
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(t =>
            {
                t.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core WebApi Demo API V1");
                t.SwaggerEndpoint("/swagger/v2/swagger.json", "ASP.NET Core WebApi Demo API V2");
            });

            app.UseRouting();

            //app.UseCors(SigoConst.PolicyName);
            app.UseCors(SigoConst.SignalRPolicyName);

            //启用身份认证服务
            app.UseAuthentication();
            //启用权限认证服务
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ExecOrdersHub>(Configuration.GetValue("SignalR:Hubs:ExecOrders:HubName", "/execOrdersHub"));
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 根据配置构建<see cref="WebSocketOptions"/>对象
        /// </summary>
        /// <returns><see cref="WebSocketOptions"/>对象</returns>
        private WebSocketOptions BuildWebSocketOptions()
        {
            var buffer = Configuration.GetValue("WebSocket:Options:ReceiveBufferSize", 4);
            var interval = Configuration.GetValue("WebSocket:Options:KeepAliveInterval", 2);
            return new WebSocketOptions() { ReceiveBufferSize = buffer * 1024, KeepAliveInterval = TimeSpan.FromMinutes(interval) };
        }
    }
}
