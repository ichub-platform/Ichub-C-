using ICHUB_LIBRARY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginICHUB loginICHUB = new LoginICHUB();
            //   loginICHUB.Login("shusi", "dang2311")=
            var content = loginICHUB.Login("quoc", "123456789").Result;
            foreach (var i in content)
            {
                Console.WriteLine(i.KeyConnect);
            }
            Console.ReadKey();
        }
    }
}
