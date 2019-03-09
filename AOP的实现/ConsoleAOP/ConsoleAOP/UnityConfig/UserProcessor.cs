using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAOP.UnityConfig
{
    public class UserProcessor: IUserProcessor
    {

        public void RegUser(User user)
        {
            Console.WriteLine("用户已注册。");
        }

        public User GetUser(User user)
        {
            return user;
        }
    }


    public interface IUserProcessor
    {
        void RegUser(User user);
        User GetUser(User user);
    }


}
