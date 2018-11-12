using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048
{
    class GameManager
    {
        public static Random r = new Random();

        public int[,] map;

        public void InitMap()
        {
            map = new int[4, 4];

            map[2, 2] = 2;

            // 初始化游戏
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    DrawNumber(i, j);
                }
            }
        }

        // 游戏主流程
        public void GameLogic()
        {
            while (true)
            {
                bool isMove = false;
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.S:
                        // 往下移动
                        isMove = MoveDown();
                        break;
                    case ConsoleKey.W:
                        // 往上移
                        isMove = MoveUp();
                        break;
                    case ConsoleKey.A:
                        // 往左移动
                        isMove = MoveLeft();
                        break;
                    case ConsoleKey.D:
                        // 往右移动
                        isMove = MoveRight();
                        break;
                    default:
                        break;
                }

                if (isMove)
                {
                    CreateNum();
                }

                // 合并完成之后，新合并的数字全部--， 顺便看下是否满格了
                int zeroNum = 0;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] % 2 != 0)
                        {
                            map[i, j]--;
                        }
                        else if(map[i, j] == 0)
                        {
                            zeroNum++;
                        }
                    }
                }
                if (zeroNum == 0)
                {
                    // 满格了， 需要判断是否游戏结束
                    bool isOver = false;
                    for (int i = 0; i < map.GetLength(0) - 1; i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            if (map[i, j] == map[i+1, j])
                            {
                                // 有相等的， 游戏没结束
                                isOver = true;
                            }
                            else if (j + 1 < map.GetLength(1) && map[i, j] == map[i, j + 1])
                            {
                                isOver = true;
                            }
                        }
                    }

                    if (!isOver)
                    {
                        // 没有相等的， 游戏结束
                        Console.SetCursorPosition(0, 40);
                        Console.Write("GameOver");
                        return;
                    }
                }
            }
        }

        // 往下移
        public bool MoveDown()
        {
            bool isMove = false;
            for (int i = map.GetLength(0) - 1; i >= 0 ; i--)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    // 不是0的都往下移动
                    if (map[i, j] != 0)
                    {
                        if (NumberDown(i, j))
                        {
                            isMove = true;
                        }
                    }
                }
            }
            return isMove;
        }

        public bool NumberDown(int x, int y)
        {
            bool isMove = false;
            while (true)
            {
                if (x + 1 < map.GetLength(0))
                {
                    if (map[x + 1, y] == 0)
                    {
                        // 下面是空的， 往下掉
                        map[x + 1, y] = map[x, y];
                        DrawNumber(x + 1, y);

                        map[x, y] = 0;
                        DrawNumber(x, y);

                        x++; // 往下移一格再判断
                        isMove = true;
                    }
                    else
                    {
                        // 下面不是空的， 判断是否相等
                        if (map[x + 1, y] == map[x , y])
                        {
                            // 相等就合并
                            map[x + 1, y] *= 2;
                            DrawNumber(x+1, y);
                            map[x + 1, y]++; // 新合并的数字 ++， 用于识别新合并的数字

                            map[x, y] = 0;
                            DrawNumber(x, y);

                            isMove = true;
                        }
                        break; // 下面不空结束判断
                    }
                }
                else
                {
                    // 越界也结束判断
                    break;
                }
            }
            return isMove;
        }

        // 往上移
        public bool MoveUp()
        {
            bool isMove = false;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    // 不是0的都往下移动
                    if (map[i, j] != 0)
                    {
                        if (NumberUp(i, j))
                        {
                            isMove = true;
                        }
                    }
                }
            }
            return isMove;
        }

        public bool NumberUp(int x, int y)
        {
            bool isMove = false;
            while (true)
            {
                if (x - 1 >= 0)
                {
                    if (map[x - 1, y] == 0)
                    {
                        // 下面是空的， 往下掉
                        map[x - 1, y] = map[x, y];
                        DrawNumber(x - 1, y);

                        map[x, y] = 0;
                        DrawNumber(x, y);

                        x--; // 往下移一格再判断
                        isMove = true;
                    }
                    else
                    {
                        // 下面不是空的， 判断是否相等
                        if (map[x - 1, y] == map[x, y])
                        {
                            // 相等就合并
                            map[x - 1, y] *= 2;
                            DrawNumber(x - 1, y);
                            map[x - 1, y]++; // 新合并的数字 ++， 用于识别新合并的数字

                            map[x, y] = 0;
                            DrawNumber(x, y);

                            isMove = true;
                        }
                        break; // 上面不空结束判断
                    }
                }
                else
                {
                    // 越界也结束判断
                    break;
                }
            }
            return isMove;
        }

        // 往右移
        public bool MoveRight()
        {
            bool isMove = false;
            for (int i = map.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = map.GetLength(1) - 1; j >= 0; j--)
                {
                    // 不是0的都往下移动
                    if (map[i, j] != 0)
                    {
                        if (NumberRight(i, j))
                        {
                            isMove = true;
                        }
                    }
                }
            }
            return isMove;
        }

        public bool NumberRight(int x, int y)
        {
            bool isMove = false;
            while (true)
            {
                if (y + 1 < map.GetLength(1))
                {
                    if (map[x, y + 1] == 0)
                    {
                        // 下面是空的， 往下掉
                        map[x, y + 1] = map[x, y];
                        DrawNumber(x, y + 1);

                        map[x, y] = 0;
                        DrawNumber(x, y);

                        y++; // 往下移一格再判断
                        isMove = true;
                    }
                    else
                    {
                        // 下面不是空的， 判断是否相等
                        if (map[x, y + 1] == map[x, y])
                        {
                            // 相等就合并
                            map[x, y + 1] *= 2;
                            DrawNumber(x , y + 1);
                            map[x, y + 1]++; // 新合并的数字 ++， 用于识别新合并的数字

                            map[x, y] = 0;
                            DrawNumber(x, y);

                            isMove = true;
                        }
                        break; // 下面不空结束判断
                    }
                }
                else
                {
                    // 越界也结束判断
                    break;
                }
            }
            return isMove;
        }

        // 往左移
        public bool MoveLeft()
        {
            bool isMove = false;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    // 不是0的都往下移动
                    if (map[i, j] != 0)
                    {
                        if (NumberLeft(i, j))
                        {
                            isMove = true;
                        }
                    }
                }
            }
            return isMove;
        }

        public bool NumberLeft(int x, int y)
        {
            bool isMove = false;
            while (true)
            {
                if (y - 1 >= 0)
                {
                    if (map[x, y - 1] == 0)
                    {
                        // 下面是空的， 往下掉
                        map[x, y - 1] = map[x, y];
                        DrawNumber(x, y - 1);

                        map[x, y] = 0;
                        DrawNumber(x, y);

                        y--; // 往下移一格再判断
                        isMove = true;
                    }
                    else
                    {
                        // 下面不是空的， 判断是否相等
                        if (map[x, y - 1] == map[x, y])
                        {
                            // 相等就合并
                            map[x, y - 1] *= 2;
                            DrawNumber(x, y - 1);
                            map[x, y - 1]++; // 新合并的数字 ++， 用于识别新合并的数字

                            map[x, y] = 0;
                            DrawNumber(x, y);

                            isMove = true;
                        }
                        break; // 上面不空结束判断
                    }
                }
                else
                {
                    // 越界也结束判断
                    break;
                }
            }
            return isMove;
        }

        // 随机创建数字
        public void CreateNum()
        {
            while (true)
            {
                int r_x = r.Next(0, map.GetLength(0));
                int r_y = r.Next(0, map.GetLength(1));

                if (map[r_x, r_y] == 0)
                {
                    // 不为零就随机一个 2 或者 4
                    map[r_x, r_y] = r.Next(1, 3) * 2;
                    DrawNumber(r_x, r_y);
                    break;
                }
            }
        }

        public void SetCurPosition(int x, int y)
        {
            Console.SetCursorPosition(y * 8, x* 4);
        }

        public void DrawNumber(int x, int y)
        {
            SetCurPosition(x, y);
            if (map[x, y] != 0)
            {
                Console.Write(map[x, y].ToString("0000"));
            }
            else
            {
                Console.Write("    ");
            }
           
        }
    }
}
