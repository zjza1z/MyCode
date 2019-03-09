using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace ConsoleAOP.UnityConfig
{
    /// <summary>
    /// 一，抓取该行为的全局异常
    /// </summary>
    public class ExceptionLog : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// 主要实现
        /// </summary>
        /// <param name="input"></param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn methodReturn = getNext().Invoke(input, getNext);  //执行下一层过滤
            Console.WriteLine("ExceptionLogging");

            if (methodReturn.Exception == null)
                Console.WriteLine("无异常");
            else
                Console.WriteLine($"异常:{methodReturn.Exception.Message}");

            return methodReturn;
        }

    }
}
