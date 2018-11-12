using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCore
{
    class Map
    {

        public int[,] map;

        public Snake player1;
        public int speed;

        public static Random r = new Random();

        public Map()
        {
            
        }

        // 初始化地图 和 玩家(蛇)
        public void InitMap()
        {
            speed = 8;

            map = LoadMap(1);

            //Program.Draw(0, 0, "■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("             贪吃蛇");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■");
            for (int i = 0; i < map.GetLength(0); i++)
            {
                Console.Write("■");
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == 0)
                    {
                        Program.Draw(i, j, "  ");
                    }
                    else if (map[i, j] == 9)
                    {
                        Program.Draw(i, j, "■");
                    }
                    else if (map[i, j] == 10)
                    {
                        Program.Draw(i, j, "食");
                    }
                    else if (map[i, j ] == 8)
                    {
                        Program.Draw(i, j, "速");
                    }
                }
                Console.WriteLine("■");
            }
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■");

            if (player1 == null)
            {
                player1 = new Snake(); // 构造方法中 初始化头部 并顺便开始一个监听按键的线程
                player1.InitSanke(2, 2);
            }
            else
            {
                // 不是第一次开始游戏 - 重置蛇
                player1.InitSanke(2, 2);
            }

            LoadData(); // 初始化玩家之后， 加载一下最高分
        }

        // 更新地图（Logic）
        public void Update()
        {
            for (int i = 0; true; i++)
            {
                // 如果玩家1移动失败 -> 游戏结束
                if (!Move(player1))
                {
                    Console.SetCursorPosition(5, 23);
                    Console.WriteLine("玩家1的分数是" + player1.Score);
                    break;
                }

                // 隔一段时间生成一个食物
                if (i % 20 == 0)
                {
                    CreateFood();
                }

                Thread.Sleep(1000 / speed); // 走的频率 
            }

            // 一局结束的时候
            SaveData();
        }

        // 生成食物
        public void CreateFood()
        {
            int r_x = r.Next(0, map.GetLength(0));
            int r_y = r.Next(0, map.GetLength(1));
            // 确保随机的位置不是蛇的头部和身体， 在地图中是空地
            Vector2 point = new Vector2(r_x, r_y);
            if (!player1.body.Contains(point) &&
                (point.x != player1.head.x && point.y != player1.head.y) &&
                map[r_x, r_y] == 0
                )
            {
                map[r_x, r_y] = 10; // 地图中 10 代表食物
                Program.Draw(r_x, r_y, "食"); // 表现层
            }
        }

        // Move成功就返回 true, 移动失败(撞墙或吃自己或越界) false
        public bool Move(Snake snake)
        {
            // 记录下原来头部的位置
            Vector2 oldHead = snake.head;

            // 根据方向更新头部坐标
            // 判断是否越界， 没越界就更新头部位置
            switch (snake.dir)
            {
                case Direction.上:
                    if (snake.head.x >= 1)
                    {
                        snake.head.x--;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.下:
                    if (snake.head.x < map.GetLength(0) - 1)
                    {
                        snake.head.x++;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.左:
                    if (snake.head.y >= 1)
                    {
                        snake.head.y--; // 头部更新坐标
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.右:
                    if (snake.head.y < map.GetLength(1) - 1)
                    {
                        snake.head.y++; // 头部更新坐标
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }

            // 蛇走一格
            // 原来的头部变成身体

            switch (map[snake.head.x, snake.head.y])
            {
                case 10:
                    // 头部和食物重合 -> 吃到食物 -> 不掉尾巴
                    map[snake.head.x, snake.head.y] = 0;
                    snake.Score++;
                    break;
                case 9:
                    // 撞墙就失败
                    return false;
                case 8:
                    speed+=10;
                    map[snake.head.x, snake.head.y] = 0;
                    break;
                default:
                    // 撞到空地， 去掉尾巴，往前移动一格
                    Vector2 last = snake.body.Dequeue(); // 把尾巴去掉
                    Program.Draw(last, "  ");
                    break;
            }
            if (snake.body.Contains(snake.head))
            {
                return false;
            }

            snake.AddBody(oldHead.x, oldHead.y);
            // 重新画头部
            Program.Draw(snake.head, "①");

            return true;
        }

        public void LoadData()
        {
            // 读取最高分
            if (File.Exists("save1.txt"))
            {
                string text = File.ReadAllText("save1.txt");
                player1.MaxScore = int.Parse(text);
            }

        }

        public int[,] LoadMap(int num)
        {
            int[,] tmpMap = new int[20, 20];
            string path = "Maps/map" + num + ".txt";
            if (File.Exists(path))
            {
                // 地图存在就读取
                string[] map = File.ReadAllLines(path);

                for (int i = 0; i < map.Length; i++)
                {
                    string[] tmpLine = map[i].Split(',');
                    for (int j = 0; j < tmpLine.Length; j++)
                    {
                        int cell = int.Parse(tmpLine[j]);
                        tmpMap[i, j] = cell;
                    }
                }
            }
            return tmpMap;
        }

        public void SaveData()
        {
            // 保存最高分
            File.WriteAllText("save1.txt", player1.MaxScore.ToString());
        }
    }
}
