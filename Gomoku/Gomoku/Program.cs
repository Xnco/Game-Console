using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // ○ ●

            string[] Graphics = { "十", "○", "●" };

            int[,] all = new int[5, 5];


            all[2, 4] = 1;
            all[3, 1] = 2;

            for (int i = 0; i < all.GetLength(0); i++)
            {
                for (int j = 0; j < all.GetLength(1); j++)
                {
                    Console.Write(Graphics[all[i, j]]);
                }

                Console.WriteLine();
            }

            int x = 0;
            int y = 0;
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.A:
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
