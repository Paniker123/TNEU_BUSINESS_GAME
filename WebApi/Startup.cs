using System;
using System.IO;
using System.Threading.Tasks;
using Common.Interfaces.Services;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Services.AccountService;
using Services.AnswerService;
using Services.AutoOptions;
using Services.GameService;
using Services.QuestionService;
using Services.TestService;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Helper;

namespace WebApi
{
    public class Startup
    {
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"EmailNotificationsLocalization.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddSingleton(_ => Configuration);

            services.AddAuthentication(p => p.SignInScheme = JwtBearerDefaults.AuthenticationScheme);

            ConfigureCustomServices(services);

            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Description = "TNEU QUIZ api",
                    Title = "TNEU QUIZ",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "WebApiDoc.xml");

                if (File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath);

                options.OperationFilter<FileUploadFilter>();
            });

            ConfigureMvc(services);
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            EnsureDataBaseReady(app.ApplicationServices.GetService<MSContext>());

            await RunCustomServices(app);

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AutoOptions.ISSUER,
                    
                    ValidateAudience = true,
                    ValidAudience = AutoOptions.AUDIENS,

                    ValidateLifetime = true,
                    
                    IssuerSigningKey = AutoOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                }
            });

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            loggerFactory.AddConsole();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api.doc";
                options.SwaggerEndpoint($"/{Configuration["ContentRoot"]}swagger/v1/swagger.json", "QUIZ TNEU  (v1)");
            });

           
            
            app.UseMvc();

            app.UseWebSockets();
          

            SetUpLogger(env, loggerFactory);
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddTransient(_ => Configuration);

            services.AddDbContext<MSContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("DataAccessLayer"));
                });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IGameService, GameService>();
        }

        private async Task RunCustomServices(IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<MSContext>();

         

        }
   

        private void EnsureDataBaseReady(MSContext context)
        {
            try
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
          
        }

        

        private void ConfigureMvc(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                    options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                });



        }

        private void SetUpLogger(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            var logPath = Path.Combine(hostingEnvironment.ContentRootPath, "Logs");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            Serilog.Core.Logger logger;

            logger = new LoggerConfiguration().WriteTo.Logger(l => l.Filter
                    .ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo
                    .RollingFile(@"Logs\Info-{Date}.log"))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo
                    .RollingFile(@"Logs\Debug-{Date}.log"))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo
                    .RollingFile(@"Logs\Warning-{Date}.log"))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo
                    .RollingFile(@"Logs\Error-{Date}.log"))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo
                    .RollingFile(@"Logs\Fatal-{Date}.log")).CreateLogger();

            loggerFactory.AddSerilog(logger);

        }

       

       

       
    }
}
