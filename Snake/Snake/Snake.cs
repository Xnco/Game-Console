using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCore
{
    // 方向
    enum Direction
    {
        上,下,左,右
    }

    class Snake
    {

        public Direction dir;   // 方向
        public Vector2 head;    // 头部坐标
        public Queue<Vector2> body; // 所有的身体坐标

        private int maxScore;
        private int score; // 得分

        public Thread inputThread;

        public int Score
        {
            get => score;
            set // 访问器
            {
                score = value;
                // 每次分数更改都要更新UI
                Console.SetCursorPosition(22, 0);
                Console.Write("得分：" + score);
                if (score > MaxScore)
                {
                    // 当分数 > 最高分时， 需要更新最高分
                    MaxScore = score;
                }
            }
        }

        public int MaxScore {
            get => maxScore;
            set
            {
                // 每次更新最高分都要更新UI
                maxScore = value;
                Console.SetCursorPosition(34, 0);
                Console.Write("最高分： " + maxScore);
            }
        }

        public Snake()
        {
            inputThread = new Thread(Input);
            inputThread.Start();  // 开始一个监听按键的线程
        }

        // 初始化蛇 - 一定要给头部坐标
        public void InitSanke(int x, int y)
        {
            body = new Queue<Vector2>();
            // 初始化头部
            InitHead(x, y);
            this.AddBody(0, 1);
            this.AddBody(0, 2);
            this.AddBody(1, 2);
            this.dir = Direction.下;
            Score = 0;
        }

        public void InitHead(int x, int y)
        {
            this.head = new Vector2(x, y);
            Program.Draw(this.head, "①");
        }

        public void AddBody(int x, int y)
        {
            Vector2 p1 = new Vector2(x, y);
            this.body.Enqueue(p1);
            Program.Draw(p1, "○"); // 蛇加入身体的时候顺便将身体画出
        }

        public void Input()
        {
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.A:
                        if (dir != Direction.右)
                            dir = Direction.左;
                        break;
                    case ConsoleKey.D:
                        if (dir != Direction.左)
                            dir = Direction.右;
                        break;
                    case ConsoleKey.S:
                        if (dir != Direction.上)
                            dir = Direction.下;
                        break;
                    case ConsoleKey.W:
                        if (dir != Direction.下)
                            dir = Direction.上;
                        break;
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.M:
                        Score++;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
