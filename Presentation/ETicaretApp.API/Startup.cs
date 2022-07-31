using ETicaretApp.Application;
using ETicaretApp.Application.Validators.Products;
using ETicaretApp.Infrastructure;
using ETicaretApp.Infrastructure.Filters;
using ETicaretApp.Infrastructure.Services.Storage.Azure;
using ETicaretApp.Infrastructure.Services.Storage.Local;
using ETicaretApp.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistenceServices(Configuration);
            services.AddInfrastructureServices();
            services.AddApplicationServices();

            //services.AddStorage<LocalStorage>();
            services.AddStorage<AzureStorage>();
            //services.AddStorage(StorageType.Azure);

            services.AddCors(options => options.AddDefaultPolicy(policy =>
                policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
            ));

            services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ETicaretApp.API", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                 {
                     options.TokenValidationParameters = new()
                     {
                         ValidateAudience = true, //Oluþturulacak token deðerini kimlerin hangi originlerin/sitelerin kullanacaðýný belirlediðimiz deðerdir.
                         ValidateIssuer = true, //Oluþturulacak token deðerini kimin daðýttýðýný ifade edeceðimiz alandýr.
                         ValidateLifetime = true, //Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
                         ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr.

                         ValidAudience = Configuration["Token:Audience"],
                         ValidIssuer = Configuration["Token:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                         LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
                     };
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ETicaretApp.API v1"));
            }

            app.UseStaticFiles();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
