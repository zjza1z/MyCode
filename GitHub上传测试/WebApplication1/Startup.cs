using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Other;

namespace WebApplication1
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
            //自定义schema的handler
            //services.AddAuthenticationCore(options => options.AddScheme<MyHandler>("eScheme", "demo scheme"));
            #region   使用Scheme方式认证
            services.AddAuthentication(options =>   //设置服务器的认证方式为，使用Scheme进行Cookie认证
            {
                //string 授权的方式
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.ClaimsIssuer = "Cookie";   //这里改动好像没什么用
                options.LoginPath = "/UserTest/NotLoginIndex";   //指定默认的登陆页面
            });
            #endregion
            
            services.AddMvc(o =>
            {
                //o.Filters.Add(typeof(CustomAuthorizeAttribute));   //这里注册的必需是实现IFilterMetadata接口的，所以不能用Authorize
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseAuthentication();    //支持授权，登陆时读取Cookie信息判断,判断权限通过则继续向下执行，否则直接响应数据
            app.UseStaticFiles();

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }




    }
}
