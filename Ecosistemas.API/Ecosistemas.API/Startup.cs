using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business.Services.Api;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.API.Initial;
using Ecosistemas.Security.Manager;
using static Ecosistemas.Security.Manager.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace Ecosistemas.API
{
    public class Startup
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public Startup(IConfiguration configuration)
        {
            //Configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddDbContext<ApiDbContext>(options =>
            //        options.UseSqlServer(SegurancaService.Descriptografar(Configuration.GetConnectionString("DefaultConnection"))));

            services.AddDbContext<ApiDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //    services.AddDbContext<KlinikosDbContext>(options =>
            //options.UseSqlServer(SegurancaService.Descriptografar(Configuration.GetConnectionString("KlinikosConnection"))));

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            //services.AddEntityFrameworkNpgsql()
            //    .AddDbContext<CatalogoDbContext>(options =>
            //    options.UseNpgsql(SegurancaService.Descriptografar(Configuration.GetConnectionString("PostgreSQLDBConnection"))));


            services.AddDbContext<KlinikosDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("KlinikosConnection")));

            services.AddDbContext<DominioDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DominioConnection")));

            services.AddScoped<UserService>();
            services.AddScoped<AccessManager>();

            _signingConfigurations = new SigningConfigurations();
            services.AddSingleton(_signingConfigurations);

            _tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                        .Configure(_tokenConfigurations);
            services.AddSingleton(_tokenConfigurations);

            //Aciona a extensão que irá configurar o uso de
            //autenticação e autorização via tokens
            services.AddJwtSecurity(_signingConfigurations, _tokenConfigurations);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(o => o.AddPolicy("ApiPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

                
            }));

            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApiDbContext context, KlinikosDbContext klinikosDbContext, DominioDbContext DominioDbContext, IServiceProvider services)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            new IdentityInitializer(context, _signingConfigurations, _tokenConfigurations)
               .InitializeApi();

            new IdentityInitializer(klinikosDbContext, context, _signingConfigurations, _tokenConfigurations)
            .InitializeKlinikos();

            new IdentityInitializer(DominioDbContext, klinikosDbContext, context, _signingConfigurations, _tokenConfigurations)
            .InitializeDominio();

            new IdentityInitializer(DominioDbContext, klinikosDbContext, context, _signingConfigurations, _tokenConfigurations)
          .InitializeSigtap();

            app.UseCors("ApiPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }

      
    }


}
