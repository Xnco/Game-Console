using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gomoku
{
    class Program
    {
        static Random r = new Random();

        // 程序的入口
        static void Main(string[] args)
        {
            ConsoleColor[] colors = {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Yellow,
                ConsoleColor.DarkBlue,
            };

            // 开始界面
            for (int i = 0; i <= 20; i++)
            {
                Console.ForegroundColor = colors[r.Next(0, colors.Length)];
                Console.SetCursorPosition(i, 5);
                Console.WriteLine("五子棋游戏");
                Thread.Sleep(100);
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(21, 8);
            Console.Write("开始游戏");
            Console.SetCursorPosition(21, 9);
            Console.Write("退出游戏");

            // 隐藏光标
            Console.CursorVisible = false;

            Console.SetCursorPosition(19, 8);
            Console.Write("→");
            int curPos = 8;
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                Console.SetCursorPosition(19, curPos);
                Console.Write("  ");
                if (info.Key == ConsoleKey.DownArrow)
                {
                    curPos++;
                }
                else if (info.Key == ConsoleKey.UpArrow)
                {
                    curPos--;
                }
                else if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (curPos == 8)
                    {
                        break;
                    }
                    else if (curPos == 9)
                    {
                        return;
                    }
                }

                if (curPos > 9)
                {
                    curPos = 9;
                }
                if (curPos < 8)
                {
                    curPos = 8;
                }

                Console.SetCursorPosition(19, curPos);
                Console.Write("→");
            }

            // 开始游戏， 激活光标
            Console.CursorVisible = true;

            GameManager game = new GameManager();
            game.InitMap();
            while (true)
            {
                // 每个回合
                if (game.Input_Player1())
                {
                    Console.SetCursorPosition(5, 12);
                    Console.WriteLine("游戏结束，按任意键重新开始， 按Q键退出游戏");
                    if (Console.ReadKey(true).Key == ConsoleKey.Q)
                    {
                        break;
                    }
                    else
                    {
                        // 重新开始，先将游戏结束的一行字给清除
                        Console.Clear();
                        // 刷新地图
                        game.InitMap();
                        continue;
                    }
                }
                if (game.Input_Player2())
                {
                    Console.SetCursorPosition(5, 12);
                    Console.WriteLine("游戏结束，按任意键重新开始， 按Q键退出游戏");
                    if (Console.ReadKey(true).Key == ConsoleKey.Q)
                    {
                        break;
                    }
                    else
                    {
                        // 重新开始，先将游戏结束的一行字给清除
                        Console.Clear();
                        // 刷新地图
                        game.InitMap();
                        continue;
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
