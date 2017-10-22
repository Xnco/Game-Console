using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager mManager = GameManager.GetSingle();

            // 游戏运行后不停接受指令即可
            bool isPlaying = true;
            while (isPlaying)
            {
                isPlaying = mManager.InputScnee(Console.ReadKey(true));
            }
        }
    }
}
