using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            gm.InitMap();

            gm.Start();
        }
    }
}
