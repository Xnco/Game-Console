using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeping
{
    class GameManager
    {
        public static Random r = new Random();
        public Box[,] map;
        public int mineNum;

        // 光标位置
        public int cur_x;
        public int cur_y; 

        // x * y 的地图， mine 个雷
        public void InitGame(int x, int y, int mine)
        {
            mineNum = mine;
            map = new Box[x, y];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = new Box();
                    map[i, j].position = new Vector2(i, j);
                }
            }

            // 随机雷，让周围的盒子数字数量 ++
            while (true)
            {
                int r_x = r.Next(0, map.GetLength(0));
                int r_y = r.Next(0, map.GetLength(1));
                Box point = map[r_x, r_y];

                if (!point.isMine)
                {
                    point.isMine = true; // 标记为雷

                    // 周围的格子数量 ++
                    List<Box> boxes = GetAroundBox(point);
                    foreach (var item in boxes)
                    {
                        item.num++;
                    }

                    // 所有雷随机完就结束
                    mine--;
                    if (mine == 0)
                    {
                        break;
                    }
                }
            }

            // 绘制地图
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                   
                    Box point = map[i, j];
                    // 测试初始化结果是否正确
                    /*
                    if (point.isMine)
                    {
                        Console.Write("⊕");
                    }
                    else
                    {
                        Console.Write(point.num + " ");
                    }
                    */
                    //Console.Write("█");
                    Draw(point.position, "█");
                }
                //Console.WriteLine();
            }

            // 初始化光标位置
            cur_x = 5;
            cur_y = 5;
            SetCurPoint(cur_x, cur_y);
        }

        // 游戏主流程
        public void GameLogic()
        {
            while (true)
            {
                bool isOver = false;
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.W:
                        if (cur_x - 1 >= 0)
                        {
                            cur_x--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (cur_x + 1 < map.GetLength(0))
                        {
                            cur_x++;
                        }
                        break;
                    case ConsoleKey.A:
                        if (cur_y - 1 >= 0)
                        {
                            cur_y--;
                        }
                        break;
                    case ConsoleKey.D:
                        if (cur_y + 1 < map.GetLength(1))
                        {
                            cur_y++;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        // 翻开
                        isOver = OpenBox(map[cur_x, cur_y]);
                        break;
                    case ConsoleKey.B:
                        // 标记
                        FlagBox(map[cur_x, cur_y]);
                        break;
                    default:
                        break;
                }

                SetCurPoint(cur_x, cur_y);

                if (isOver)
                {
                    // 翻到雷了
                    Draw(new Vector2(0, 15), "GameOver");
                    return;
                }
                else
                {
                    // 判断是否胜利 未翻开的格子和雷数相等，游戏胜利
                    int notOpen = 0;
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            if (!map[i, j].isOpen)
                            {
                                notOpen++;
                            }
                        }
                    }
                    if (notOpen == mineNum)
                    {
                        Draw(new Vector2(0, 15), "Win");
                        return;
                    }
                }
            }
        }

        // 获取周围的盒子
        public List<Box> GetAroundBox(Box box)
        {
            List<Box> result = new List<Box>();
            Vector2 position = box.position;

            if (position.x - 1 >= 0)
            {
                // 左上角
                if (position.y - 1 >= 0)
                {
                    Box leftTop = map[position.x - 1, position.y - 1];
                    result.Add(leftTop);
                }

                // 上
                Box left = map[position.x - 1, position.y];
                result.Add(left);

                // 右上
                if (position.y + 1 < map.GetLength(1))
                {
                    Box rightTop = map[position.x - 1, position.y + 1];
                    result.Add(rightTop);
                }
            }

            // 左
            if (position.y - 1 >= 0)
            {
                Box left = map[position.x, position.y - 1];
                result.Add(left);
            }

            // 右
            if (position.y + 1 < map.GetLength(1))
            {
                Box right = map[position.x, position.y + 1];
                result.Add(right);
            }

            if (position.x + 1 < map.GetLength(0))
            {
                // 左下
                if (position.y - 1 >= 0)
                {
                    Box leftBottom = map[position.x + 1, position.y - 1];
                    result.Add(leftBottom);
                }

                //下
                Box bottom = map[position.x + 1, position.y];
                result.Add(bottom);

                //右下
                if (position.y + 1 < map.GetLength(1))
                {
                    Box rightBottom = map[position.x + 1, position.y + 1];
                    result.Add(rightBottom);
                }
            }

            return result;
        }

        // 打开一个盒子 - false 为翻到雷了
        public bool OpenBox(Box box)
        {
            if (box.isOpen || box.isFlag)
            {
                return false;
            }
            box.isOpen = true;
            if (box.isMine)
            {
                // 翻到雷了
                Draw(box.position, "⊕");
                return true;
            }
            else if (box.num != 0)
            {
                Draw(box.position, box.num.ToString());
            }
            else if (box.num == 0)   
            {
                // 选中的是 0 ， 要扩散
                Draw(box.position, "  ");
                List<Box> around = GetAroundBox(box);
                foreach (var item in around)
                {
                    OpenBox(item);
                }
            }
            return false;
        }

        public void FlagBox(Box box)
        {
            if (box.isFlag)
            {
                // 取消标记
                box.isFlag = false;
                Draw(box.position, "█");
            }
            else
            {
                // 标记
                box.isFlag = true;
                Draw(box.position, "√");
            }
        }

        // 设置光标位置
        public void SetCurPoint(int x, int y)
        {
            Console.SetCursorPosition(y * 2, x);
        }

        public void Draw(Vector2 pos, string str)
        {
            SetCurPoint(pos.x, pos.y);
            Console.Write(str);
        }
    }
}
