using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAOP.dynamicAOP
{
    /// <summary>
    /// 使用 Castle/DynamicProxy 实现动态代理
    /// </summary>
    public class CastleProxyAOP
    {

        public static void Show()
        {
            User user = new User()
            {
                Name = "Eleven",
                Password = "123456"
            };

            ProxyGenerator generator = new ProxyGenerator();
            MyInterceptor interceptor = new MyInterceptor();

            //对UserProcessor2类做了改动
            UserProcessor2 userP = generator.CreateClassProxy<UserProcessor2>(interceptor);  //为该类型添加注册规则
            userP.RegUser(user);
        }

    }


    public class MyInterceptor : IInterceptor
    {
        /// <summary>
        /// 实现接口,调用方法时进入的方法
        /// </summary>
        /// <param name="invocation"></param>
        void IInterceptor.Intercept(IInvocation invocation)
        {
            PreProceed(invocation);
            invocation.Proceed();  //执行真正的方法
            PostProceed(invocation);
        }


        public void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("方法执行前");
        }

        public void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("方法执行后");
        }
    }

    public interface IUserProcessor2
    {
        void RegUser(User user);
    }

    public class UserProcessor2 : IUserProcessor
    {
        /// <summary>
        /// 必须带上virtual 否则无效~
        /// </summary>
        /// <param name="user"></param>
        public virtual void RegUser(User user)
        {
            Console.WriteLine($"用户已注册。Name:{user.Name},PassWord:{user.Password}");
        }
    }


}
