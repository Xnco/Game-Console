using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    class GameManager
    {
        // 0 - 空地,  红车 - 11 黑马 - 22
         int[,] map;

        public int cur_x; // 光标的x
        public int cur_y; // 光标的y

        int selectChess; // 选中的棋子
        int selectX;
        int selectY;

        public GameManager()
        {

        }

        public void InitMap()
        {
            map = new int[10, 9];

            map[9, 0] = 11;
            map[0, 1] = 22; 

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0)
                    {
                        Draw(i, j, "十");
                    }
                    else if (map[i, j] == 11)
                    {
                        Draw(i, j, "车");
                    }
                    else if (map[i, j] == 22)
                    {
                        Draw(i, j, "马");
                    }
                }
            }
        }

        public void Start()
        {
            SetCurPos(cur_x, cur_y);

            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.W:
                        cur_x--;
                        break;
                    case ConsoleKey.S:
                        cur_x++;
                        break;
                    case ConsoleKey.A:
                        cur_y--;
                        break;
                    case ConsoleKey.D:
                        cur_y++;
                        break;
                    case ConsoleKey.Q:
                        if (selectChess != 0)
                        {
                            // 在已经选中棋子的情况下, 再按Q -> 移动棋子
                            MoveChess();
                        }
                        else
                        {
                            // 现在没有选中棋子 -> 去选棋子
                            SelectChess();
                        }
                        break;
                    default:
                        break;
                }

                // 坐标变化之后, 更新光标位置
                SetCurPos(cur_x, cur_y);
            }
         }

        // 选中一个棋
        public void SelectChess()
        {
            if (map[cur_x, cur_y] == 0)
            {
                // 没选中
                return;
            }

            // 记录 Q 中的棋子
            selectChess = map[cur_x, cur_y];
            // 记录下 选中的棋子的 位置
            selectX = cur_x;
            selectY = cur_y;
        }

        public void MoveChess()
        {
            if (map[cur_x, cur_y] != 0)
            {
                // 判断是否同阵营, 同阵营就return, 取消选中
                if (false)
                {
                    selectChess = 0;
                    return;
                }
            }
            // 当前的位置没有棋 -> 移动的逻辑
            // 记录我目的地的坐标
            int move_x = cur_x;
            int move_y = cur_y;

            // 将某个棋子 从 (selectX, selectY) 移动到 (move_x, move_y)
            if (selectChess == 11)
            {
                Car_Move(selectX, selectY, move_x, move_y);
            }
            else if (selectChess == 22)
            {
                // 马移动
                Horse_Move(selectX, selectY, move_x, move_y);
            }

            selectChess = 0; // 不再选中棋子
        }

        public void Car_Move(int before_x, int before_y, int now_x, int now_y)
        {
            // 车的逻辑
            if (before_x == now_x && before_y != now_y)
            {
                // 横着走
                // 两点中间不能有有其他棋子
                int min = 0;
                int max = 0;
                if (now_y > before_y)
                {
                    // 往右边走
                    min = before_y;
                    max = now_y;
                }
                else
                {
                    // 往左走
                    min = now_y;
                    max = before_y;
                }

                for (int i = min + 1; i < max; i++)
                {
                    if (map[before_x, i] != 0)
                    {
                        // 说明有其他棋
                        return;
                    }
                }

                // 可以移动
                // 原来的地方变成空地
                Draw(before_x, before_y, "十");
                map[before_x, before_y] = 0;

                // 现在的地方变成车
                Draw(now_x, now_y, "车");
                map[now_x, now_y] = 11;
            }
            if (before_x != now_x && before_y == now_y)
            {

                // 竖着走
                int min = 0;
                int max = 0;
                if (now_x > before_x)
                {
                    // 往右边走
                    min = before_x;
                    max = now_x;
                }
                else
                {
                    // 往左走
                    min = now_x;
                    max = before_x;
                }

                for (int i = min + 1; i < max; i++)
                {
                    if (map[i, before_y] != 0)
                    {
                        // 说明有其他棋
                        return;
                    }
                }

                Draw(before_x, before_y, "十");
                map[before_x, before_y] = 0;

                // 现在的地方变成车
                Draw(now_x, now_y, "车");
                map[now_x, now_y] = 11;
            }
        }

        public void Horse_Move(int before_x, int before_y, int now_x, int now_y)
        {
            if (before_x - now_x == 2 && before_y - now_y == 1)
            {
                // 可以移动  
                // 原来的地方变成空地
                Draw(before_x, before_y, "十");
                map[before_x, before_y] = 0;

                // 现在的地方变成车
                Draw(now_x, now_y, "马");
                map[now_x, now_y] = 22;
            }
            else if (before_x + 2 == now_x && before_y + 1 == now_y)
            {
                // 可以移动  
                // 原来的地方变成空地
                Draw(before_x, before_y, "十");
                map[before_x, before_y] = 0;

                // 现在的地方变成车
                Draw(now_x, now_y, "马");
                map[now_x, now_y] = 22;
            }
        }

        public void SetCurPos(int x, int y)
        {
            Console.SetCursorPosition(y * 2, x);
        }

        public void Draw(int x, int y, string str)
        {
            Console.SetCursorPosition(y * 2, x);
            Console.Write(str);
        }
    }
}
