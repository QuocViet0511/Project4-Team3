using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryLayer;
using ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project4_Nhom3
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
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDbContext<DataDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBaiVietService, BaiVietService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<IBinhLuanService, BinhLuanService>();
            services.AddTransient<IDanhMucSanPhamService, DanhMucSanPhamService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IGioHangService, GioHangService>();
            services.AddTransient<IGioHangDTOService, GioHangDTOService>();
            services.AddTransient<IKeySPService, KeySPService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ISanPhamService, SanPhamService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSession(options =>
            {
                options.Cookie.Name = "Project4";
                options.IdleTimeout = new TimeSpan(0,30,0);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
