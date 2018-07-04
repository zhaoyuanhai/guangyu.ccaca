using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using CC = CCACAWebUI.Common;
using CCACAWebUI.Common;

namespace CCACAWebUI
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
            services.AddMvc();
            services.AddSession();
            services.AddAuthentication();
            services.AddDbContext<DB.DbEntityContext>();
            services.AddResponseCaching();

            services.AddAuthentication("MyCookieAuthenticationScheme")
                .AddCookie("MyCookieAuthenticationScheme", options =>
                {
                    options.AccessDeniedPath = CC.CookieAuthenticationDefaults.AccessDeniedPath;
                    options.LoginPath = CC.CookieAuthenticationDefaults.LoginPath;
                });

            DB.DbEntityContext.ConnectionString = Configuration.GetConnectionString("CCACAConnStr");
            var mailSetting = Configuration.GetSection("MailSetting");
            CC.MailHelper.UserName = mailSetting.GetValue<string>("UserName");
            CC.MailHelper.Passwrod = mailSetting.GetValue<string>("Password");
            CC.MailHelper.DisplayName = mailSetting.GetValue<string>("DisplayName");
            CC.MailHelper.ServerAddress = mailSetting.GetValue<string>("ServerAddress");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //添加自定义自定义虚拟文件夹
            var paths = Configuration.GetSection("VirtualPaths").GetChildren();
            foreach (var item in paths)
            {
                var dirs = item.Get<List<string>>();
                if (dirs == null && item.Value != null)
                {
                    dirs = new List<string>() { item.Value };
                }

                foreach (var dir in dirs)
                {
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(dir),
                        RequestPath = item.Key
                    });
                }
            }

            app.UseSession();
            app.UseAuthentication();
            app.UseResponseCaching();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{action=Index}/{id=0}/{languageId=1}/{controller=Home}");

                routes.MapRoute(
                    name: "mvc",
                    template: "{controller}/{action}");
            });
        }
    }
}
