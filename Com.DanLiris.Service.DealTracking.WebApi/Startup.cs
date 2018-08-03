using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using Com.DanLiris.Service.DealTracking.Lib;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Newtonsoft.Json.Serialization;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Implementation;

namespace Com.DanLiris.Service.DealTracking
{
    public class Startup
    {
        /* Hard Code */
        private string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private string DEAL_TRACKING_POLICY = "DealTrackingPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Register

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<CompanyFacade>()
                .AddTransient<ContactFacade>()
                .AddTransient<ReasonFacade>()
                .AddTransient<BoardFacade>()
                .AddTransient<StageFacade>()
                .AddTransient<DealFacade>()
                .AddTransient<ActivityFacade>();
        }

        private void RegisterLogic(IServiceCollection services)
        {
            services
                .AddTransient<CompanyLogic>()
                .AddTransient<ContactLogic>()
                .AddTransient<ReasonLogic>()
                .AddTransient<BoardLogic>()
                .AddTransient<StageLogic>()
                .AddTransient<DealLogic>()
                .AddTransient<ActivityLogic>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<AzureStorageService>()
                .AddScoped<IdentityService>()
                .AddScoped<ValidateService>();
        }

        #endregion Register

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];

            /* Register */
            services.AddDbContext<DealTrackingDbContext>(options => options.UseSqlServer(connectionString));
            RegisterFacades(services);
            RegisterLogic(services);
            RegisterServices(services);
            services.AddAutoMapper();

            /* Versioning */
            services.AddApiVersioning(options => { options.DefaultApiVersion = new ApiVersion(1, 0); });

            /* Authentication */
            string Secret = Configuration.GetValue<string>("Secret") ?? Configuration["Secret"];
            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = Key
                    };
                });

            /* CORS */
            services.AddCors(options => options.AddPolicy(DEAL_TRACKING_POLICY, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(EXPOSED_HEADERS);
            }));

            /* API */
            services
               .AddMvcCore()
               .AddAuthorization()
               .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
               .AddJsonFormatters();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Update Database */
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DealTrackingDbContext context = serviceScope.ServiceProvider.GetService<DealTrackingDbContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseCors(DEAL_TRACKING_POLICY);
            app.UseMvc();
        }
    }
}
