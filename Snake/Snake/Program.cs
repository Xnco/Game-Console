using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // ①②③④⑤⑥⑦⑧⑨⑩○

            Console.CursorVisible = false;

            Map map = new Map();
            map.InitMap();

            //Thread.Sleep(2000); // 初始化完成等待2s
           

            while (true)
            {
                Console.ReadKey(true); // 按任意键开始游戏
                map.Update();

                // 一局游戏结束
                if (Console.ReadKey(true).Key == ConsoleKey.R)
                {
                    Console.Clear();
                    map.InitMap();
                }
                else
                {
                    break;
                }
            }

            Console.SetCursorPosition(0, 24);
            Console.WriteLine("游戏结束， 按ESC退出");
        }

        public static void Draw(int x, int y, string str)
        {
            Console.SetCursorPosition(y * 2 + 2, x + 2);
            Console.Write(str);
        }

        // 画图 - 将数组中坐标转换屏幕坐标， 并打赢 str
        public static void Draw(Vector2 point, string str)
        {
            Draw(point.x, point.y, str);
        }
    }
}
