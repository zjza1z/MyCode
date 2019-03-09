using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAOP.dynamicAOP
{
    /// <summary>
    /// .net Core 中不支持
    /// 
    /// 使用.Net Remoting/RealProxy 服务调用技术，实现动态代理
    /// --局限在业务类必须是继承自MarshalByRefObject类型
    /// </summary>
    public class RealProxyAOP
    {
        public static void Show()
        {
            User user = new User()
            {
                Name = "Eleven",
                Password = "123456"
            };

            //普通的调用
            UserProcessor processor = new UserProcessor();
            processor.RegUser(user);

            //对UserProcessor类做了改动
            UserProcessor userProcessor = TransparentProxy.Create<UserProcessor>();
            userProcessor.RegUser(user);
        }

    }

    /// <summary>
    /// 真实代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyRealProxy<T> : RealProxy
    {
        private T tTarget;

        /// <summary>
        /// 同时调用父类带参数的构造函数
        /// </summary>
        /// <param name="target"></param>
        public MyRealProxy(T target):base(typeof(T))
        {
            this.tTarget = target;
        }

        /// <summary>
        /// 重写该方法，在调用时为原类增加功能
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            BeforeProceede(msg);

            IMethodCallMessage callMessage = (IMethodCallMessage)msg;  //msg中包含参数，方法名称等信息

            object returnValue = callMessage.MethodBase.Invoke(this.tTarget, callMessage.Args); //(用哪个对象来调用，调用的参数)
            AfterProceede(msg);

            return new ReturnMessage(returnValue, new object[0], 0, null, callMessage);
        }


        public void BeforeProceede(IMessage msg)
        {
            Console.WriteLine("方法执行前可以加入的逻辑");
        }

        public void AfterProceede(IMessage msg)
        {
            Console.WriteLine("方法执行后可以加入的逻辑");
        }
    }

    /// <summary>
    /// 透明代理
    /// </summary>
    public static class TransparentProxy
    {
        public static T Create<T>()
        {
            T instance = Activator.CreateInstance<T>();
            MyRealProxy<T> realProxy = new MyRealProxy<T>(instance);  //传入一个实例化好的T类型
            T transparentProxy = (T)realProxy.GetTransparentProxy();  //返回一个加工好的T类型
            return transparentProxy;
        }
    }


    public interface IUserProcessor
    {
        void RegUser(User user);
    }


    /// <summary>
    /// 调用的对象
    /// 注意： 必须继承自MarshalByRefObject父类，否则无法生成
    /// </summary>
    public class UserProcessor : MarshalByRefObject, IUserProcessor
    {
        public void RegUser(User user)
        {
            Console.WriteLine("用户已注册。用户名称{0} Password{1}", user.Name, user.Password);
        }
    }


}
