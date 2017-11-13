using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ericore.Services;
using Microsoft.Extensions.Configuration;
using Ericore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ericore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<EricoreDbContext>(options => options.UseSqlServer(Configuration["database:connectionString"]));

            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IDBTag, DBTag>();
            services.AddScoped<ICommentData, SqlCommentData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IDBTag dBTag)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //app.UseFileServer(); // = app.UseDefaultFiles() + app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                var dbTag = dBTag.GetDBTag();
                await context.Response.WriteAsync(dbTag);
            });
        }
    }
}
