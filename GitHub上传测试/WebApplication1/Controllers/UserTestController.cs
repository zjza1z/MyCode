using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Other;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UserTestController : Controller
    {

        //当我注释掉app.UseAuthentication();时，发现访问404而且访问的地址也不对
        //由此可知app.UseAuthentication();这里是系统内置的对登陆用户的Cookie做出判断
        public IActionResult Index()
        {
            string sName = base.HttpContext.User?.Identity?.Name;
            return View();
        }


        [CusAllowAnonymous]
        public IActionResult NotLoginIndex()
        {
            return View();
        }


        /// <summary>
        /// Scheme - Cookie认证，在AspNetCore.Cookies这个cookie中追加了用户的内容，其中的value就是加密序列化后的用户信息
        /// 
        /// 1，登陆控制器使用Claim添加用户
        /// 2，Starup中的ConfigureServices添加认证授权方式
        /// 3，要访问的页面添加[Authorize]认证
        /// 
        /// </summary>
        /// <returns></returns>
        [CusAllowAnonymous]
        public IActionResult UserLogin()
        {
            //引用序列化组件
            CurrentUser currentUser = new UserService().FindUser("Eleven");

            //使用Cookie认证
            var claimIdentity = new ClaimsIdentity("Cookie");      //添加一个ClaimsIdentity属性,身份的验证类型,使用cookie作为身份验证
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, currentUser.Name));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Email, currentUser.Email));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, currentUser.Role));
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
            base.HttpContext.SignInAsync(claimsPrincipal).Wait();//不就是写到cookie
            return new ContentResult() { Content = "登陆成功" };
        }


        [CusAllowAnonymous]
        public IActionResult Logout()
        {
            base.HttpContext.SignOutAsync().Wait();
            return new ContentResult() { Content = "退出成功" };
            //return this.Redirect("~/Fourth/Login");
        }



    }
}