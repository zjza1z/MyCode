using ConsoleAOP.dynamicAOP;
using ConsoleAOP.UnityConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAOP
{
    /// <summary>
    /// AOP简介
    /// 
    /// AOP：面向切面编程  编程思想  
    ///      就是解决类的内部变化问题
    ///      能做到让开发者动态的修改一个静态的面向对象模型，
    ///      在不破坏封装的前提下，去增加各种功能：非业务逻辑，是一些公共逻辑
    ///      是对OOP的有效补充
    /// 
    /// 类似于  MVC 中增加过滤器
    ///  IOC注入时也可以实现AOP功能
    ///  特性 + 反射 实现 AOP
    /// </summary>
    class Program
    {
        /// <summary>
        /// 1 AOP面向切面编程
        /// 2 动态实现AOP
        /// 3 Unity、MVC中的AOP
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //使用.Net Remoting实现动态代理
            //RealProxyAOP.Show();

            //使用 Castle\DynamicProxy 实现动态代理
            //CastleProxyAOP.Show();


            //使用Unity容器实现AOP
            UnityConfigAOP.Show();

        }



    }
}
