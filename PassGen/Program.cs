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
                Console.WriteLine(passwordGen.Generate(8,PasswordGen.PasswordPattern.AlphaNumericExtended, PasswordGen.PasswordCase.Mixed));
            }


#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }
    }
}
