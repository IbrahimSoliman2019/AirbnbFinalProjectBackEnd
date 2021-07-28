using Api.ApplictionExtentions;
using Api.DTOS;
using Api.MiddleWares;
using Domain.IdentityEntities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            

<<<<<<< HEAD
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();

                                  });
            });

            services.AddDbContext<ApplicationContext>(opt=>{
=======

            services.AddDbContext<ApplicationContext>(opt =>
            {
>>>>>>> 487988618a9ffbbab3f6f0f0c31468901b88af76
                opt.UseSqlServer(Configuration.GetConnectionString("Default")).EnableSensitiveDataLogging();
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationContext>();
            services.AddCors(Opt =>
            {
                Opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:4200");
                });
            });
            services.AddControllers();
            services.AddApplicationServices();
            services.AddSwaggerSetting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();


            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);


            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.UseSwaggerSettings();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
