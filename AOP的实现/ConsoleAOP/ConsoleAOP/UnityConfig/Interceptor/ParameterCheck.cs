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
    /// 四，方法执行前参数检查
    /// </summary>
    public class ParameterCheck : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("ParameterCheckBehavior");
            User user = input.Inputs[0] as User;//可以不写死类型，反射+特性完成数据有效性监测
            if (user.Password.Length < 5)
            {
                //返回一个包含异常的IMethodReturn
                return input.CreateExceptionMethodReturn(new Exception("密码长度不能小于10位"));
            }
            else
            {
                Console.WriteLine("参数检测无误");
                return getNext().Invoke(input, getNext);
            }
        }


    }
}
