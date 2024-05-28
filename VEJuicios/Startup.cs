using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PdfSharp;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;
using VEJuicios.Domain;
using VEJuicios.Domain.Model;
using VEJuicios.Infrastructure;
using VEJuicios.Model;
using VEJuicios.Modules;

namespace VEJuicios
{
    public class Startup
    {
        private bool _useSwagger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            _useSwagger = Configuration.GetValue<bool>("Swagger");

            services.AddUseCases();
            services.AddPresenters();
            services.AddSQLServer(this.Configuration);
            services.AddControllers();

            var allowOrigins = Configuration.GetSection("AllowOrigins").Get<AllowOrigins>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  policy =>
                                  {
                                      policy.WithOrigins(allowOrigins.Origins)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
            services.AddControllers();

            services.Configure<WorkerSettings>(Configuration.GetSection("WorkerSettings"));

            services.Configure<AfipConnection>(Configuration.GetSection("AfipConnection"));

            services.Configure<CredentialNetworkAfip>(Configuration.GetSection("CredentialNetworkAfip"));

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<VE_AfipContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);

            services.AddMvc();
            if (_useSwagger)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VEJuicios", Version = "v1" });
                    var securitySchema = new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    };
                    c.AddSecurityDefinition("Bearer", securitySchema
                    );
                    var securityRequirement = new OpenApiSecurityRequirement
                    {
                        { securitySchema, new[] { "Bearer" } }
                    };
                    c.AddSecurityRequirement(securityRequirement);
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            if (_useSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VEJuicios");
                    c.InjectStylesheet("/css/swagger.css");
                });
            }
            if (Capabilities.Build.IsCoreBuild)
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();
        }
    }
    class AllowOrigins
    {
        public string[] Origins { get; set; }

    }

}
