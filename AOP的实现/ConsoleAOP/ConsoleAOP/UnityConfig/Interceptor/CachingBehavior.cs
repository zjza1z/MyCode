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
    /// 二，使用缓存
    /// </summary>
    public class CachingBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        private static Dictionary<string, object> CachingBehaviorDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">包含传递的参数</param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("CachingBehavior");
            //StringBuilder sbu = new StringBuilder();
            //foreach (object item in input.Inputs)
            //{
            //    sbu.Append(item.ToString());
            //}
            //sbu.ToString();
            IMethodReturn imReturn = null;

            string key = $"{input.MethodBase.Name}_{Newtonsoft.Json.JsonConvert.SerializeObject(input.Inputs)}";
            if (CachingBehaviorDictionary.ContainsKey(key))  //直接返回结果，不再向下执行
            {
                //根据结果创建一个IMethodReturn
                imReturn = input.CreateMethodReturn(CachingBehaviorDictionary[key]);  
            }
            else
            {
                imReturn = getNext().Invoke(input, getNext);
                if (imReturn.ReturnValue != null)
                    CachingBehaviorDictionary.Add(key, imReturn.ReturnValue);
            }
            return imReturn;
        }


    }
}
