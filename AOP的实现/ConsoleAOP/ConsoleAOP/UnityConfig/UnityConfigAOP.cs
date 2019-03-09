using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ConsoleAOP.UnityConfig
{
    /// <summary>
    /// 注册类中的所有方法都会添加前缀方法
    /// 通过配置文件增加或减少前缀方法
    /// </summary>
    public class UnityConfigAOP
    {
        /// <summary>
        /// 将委托的实例直接调用的两种方法
        /// getNext().Invoke(input, getNext);
        /// getNext()(input, getNext); 
        /// </summary>
        public static void Show()
        {
            User user = new User()
            {
                Name = "张三",
                Password = "123456789"
            };

            
            { //Unity容器的基本使用
                //IUnityContainer container = new UnityContainer();
                //container.RegisterType<IUserProcessor, UserProcessor>();
                //IUserProcessor Processor = container.Resolve<IUserProcessor>();
                //Processor.RegUser(user);
            }


            {  //Unity容器实现AOP
                //注意 ： XML配置文件，右键选择属性，始终复制，可以将文件生成到bin目录下
                IUnityContainer container = new UnityContainer();
                //
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "config\\Unity.Config");
                //
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
                configSection.Configure(container, "aopContainer");

                IUserProcessor processor = container.Resolve<IUserProcessor>();
                processor.RegUser(user);  //模拟用户注册

                User userNew1 = processor.GetUser(user);
                User userNew2 = processor.GetUser(user);

            }



        }



    }
}
