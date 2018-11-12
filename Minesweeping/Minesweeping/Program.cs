using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeping
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();
            //初始化地图， 指定长宽和雷的个数
            game.InitGame(11,11,10);

            // 游戏开始
            game.GameLogic();
        }
    }
}
