using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gomoku
{
    class GameManager
    {
        // 数据层 -> 逻辑
        int[,] map;

        // 记录光标当前所在的位置
        int left;
        int top;

        // 初始化地图
        public void InitMap()
        {
            map = new int[11, 11];

            // 表现层 -> 玩家看的

            Console.SetCursorPosition(22, 0);
            Console.Write("五子棋");

            Console.SetCursorPosition(4, 1);
            Console.Write("玩家1");

            Console.SetCursorPosition(40, 1);
            Console.Write("玩家2");

            for (int i = 0; i < map.GetLength(0); i++)
            {
                // 每一行开始之前都先输出14个空格
                Console.SetCursorPosition(14, i + 1);

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0)
                    {
                        Console.Write("十");
                        Thread.Sleep(10);
                    }
                }
            }

            left = map.GetLength(0) / 2;
            top = map.GetLength(1) / 2;
            UpdateCursor(); // 地图初始化完之后更新光标的位置
        }

        // 更新光标位置 - 根据坐标
        public void UpdateCursor()
        {
            // 更新光标位置
            Console.SetCursorPosition(left * 2 + 14, top + 1); // 初始化光标位置在左上角
        }

        // 输入：监听按键，如果游戏胜利/结束就返回true, 否则返回false
        // 玩家1操作， 直到下棋/ESC为止
        public bool Input_Player1()
        {
            Console.SetCursorPosition(2, 1);
            Console.Write("→");
            UpdateCursor(); // 光标回归棋盘中

            while (true)
            {
                // 监听键盘的按键
                ConsoleKeyInfo info = Console.ReadKey(true);
                //Console.WriteLine("你按下了 {0} 键", info.Key);
                switch (info.Key)
                {
                    case ConsoleKey.A:
                        if (left - 1 >= 0)
                        {
                            left--; // 确保 -1 之后不越界才能操作
                        }
                        break;
                    case ConsoleKey.D:
                        // 光标往右边移动一格
                        if (left + 1 < map.GetLength(1))
                        {
                            left++;
                        }
                        break;
                    case ConsoleKey.W:
                        if (top - 1 >= 0)
                        {
                            top--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (top + 1 < map.GetLength(0))
                        {
                            top++;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return true;
                    case ConsoleKey.Q:
                        // 玩家1下棋
                        if (map[top, left] == 0) // 当前数组中的位置已经有棋子了， 就不能下棋
                        {
                            // 先下棋，
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("①");
                            Console.ForegroundColor = ConsoleColor.White;
                            // 再取消箭头
                            Console.SetCursorPosition(2, 1);
                            Console.Write("  ");

                            UpdateCursor(); // 将光标移回当前为止
                            // 下棋的时候改变数组的值，数组坐标和光标坐标是反的
                            map[top, left] = 1;

                            // 下棋成功 => 判断游戏是否结束(胜利)
                            return Win(top, left);
                        }
                        break;
                    default:
                        break;
                }

                UpdateCursor();
            }
        }

        // 玩家2下棋， 直到下棋/ESC为止
        public bool Input_Player2()
        {
            Console.SetCursorPosition(38, 1);
            Console.Write("→");
            UpdateCursor();

            while (true)
            {
                // 监听键盘的按键
                ConsoleKeyInfo info = Console.ReadKey(true);
                //Console.WriteLine("你按下了 {0} 键", info.Key);
                switch (info.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (left - 1 >= 0)
                        {
                            left--; // 确保 -1 之后不越界才能操作
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        // 光标往右边移动一格
                        if (left + 1 < map.GetLength(1))
                        {
                            left++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (top - 1 >= 0)
                        {
                            top--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (top + 1 < map.GetLength(0))
                        {
                            top++;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return true;
                    case ConsoleKey.P:
                        // 玩家2下棋
                        if (map[top, left] == 0) // 当前数组中的位置已经有棋子了， 就不能下棋
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("②");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.SetCursorPosition(38, 1);
                            Console.Write("  ");

                            UpdateCursor(); // 将光标移回当前为止

                            // 下棋的时候改变数组的值，数组坐标和光标坐标是反的
                            map[top, left] = 2;

                            // 下棋成功 => 判断游戏是否结束(胜利)
                            return Win(top, left);
                        }
                        break;
                    default:
                        break;
                }

                UpdateCursor();
            }
        }

        // 游戏胜利就返回 true
        public bool Win(int x, int y)
        {
            #region 横向逻辑
            //当前下棋的坐标 - 横向判断 x 轴不变
            // 往右判断 - 直到右边和我不相等为止
            int offset = 1; // 右边的偏移量
            int num = 1; // 记录相连棋子的个数
            while (true)
            {
                // x || y => 当x为true, 就返回x, 当x为false, 就返回y
                if (y + offset >= map.GetLength(1) || map[x, y] != map[x, y + offset])
                {
                    // 越界和不相等
                    break;
                }
                else
                {
                    offset++;
                    num++;
                }
            }

            offset = 1; // 重置偏移量
            // 往左判断 - 直到左边和我不相等为止
            while (true)
            {
                if (y - offset < 0 || map[x, y] != map[x, y - offset])
                {
                    break;
                }
                else
                {
                    offset++;
                    num++;
                }
            }
            if (num >= 5)
            {
                // 横向胜利
                return true;
            }
            #endregion

            #region 纵向逻辑
            // 竖直逻辑
            // 往上判断 - 直到和我不相等为止
            int num2 = 1; // 连续的棋子的个数
            int offset2 = 1;
            while (true)
            {
                if (x - offset2 < 0 || map[x, y] != map[x - offset2, y])
                {
                    break;
                }
                else
                {
                    offset2++;
                    num2++;
                }
            }

            // 往下判断
            offset2 = 1;
            while (true)
            {
                if (x + offset2 >= map.GetLength(0) || map[x, y] != map[x + offset2, y])
                {
                    break;
                }
                else
                {
                    offset2++;
                    num2++;
                }
            }

            if (num2 >= 5)
            {
                return true;
            }

            #endregion

            #region 右上方和左下方 斜线逻辑
            // 右上方
            int num3 = 1;
            int offset3 = 1;
            while (true)
            {
                if (x - offset3 < 0 || y + offset3 >= map.GetLength(1) || map[x, y] != map[x - offset3, y + offset3])
                {
                    break;
                }
                else
                {
                    offset3++;
                    num3++;
                }
            }

            //左下方
            offset3 = 1; // 重置偏移量
            while (true)
            {
                if (x + offset3 >= map.GetLength(0) || y - offset3 < 0 || map[x, y] != map[x + offset3, y - offset3])
                {
                    break;
                }
                else
                {
                    offset3++;
                    num3++;
                }
            }

            if (num3 >= 5)
            {
                return true;
            }
            #endregion

            #region 左上方和右下方 斜线逻辑
            int offset4 = 1;
            int num4 = 1;
            // 左上方
            while (true)
            {
                if (x - offset4 < 0 || y - offset4 < 0 || map[x, y] != map[x - offset4, y - offset4])
                {
                    break;
                }
                else
                {
                    offset4++;
                    num4++;
                }
            }

            // 右下方
            offset4 = 1;
            while (true)
            {
                if (x + offset4 >= map.GetLength(0) || y + offset4 >= map.GetLength(1) || map[x, y] != map[x + offset4, y + offset4])
                {
                    break;
                }
                else
                {
                    offset4++;
                    num4++;
                }
            }

            if (num4 >= 5)
            {
                return true;
            }
            #endregion

            return false;
        }
    }
}
