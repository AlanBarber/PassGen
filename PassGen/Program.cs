using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassGen
{
    class Program
    {
        static void Main(string[] args)
        {
            PasswordGen passwordGen = new PasswordGen();

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(passwordGen.Generate(PasswordPreset.LudacrisSecure));
            }


#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }
    }
}
