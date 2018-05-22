using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Scenes
{

    public class Home : Scene
    {
        public Home(StateMachine fsm) : base(fsm)
        {
            //Console.WriteLine("初始化Home场景");
        }

        public override void Init()
        {
            Console.Clear();
            Console.WriteLine("欢迎回家, 你可以进行一下操作:");
            Console.WriteLine("a. 查看个人信息");
            Console.WriteLine("b. 野外探险");
            Console.WriteLine("c. 前往商店");
            Console.WriteLine("d. 保存游戏(根据你的游戏名字保存)");
            Console.WriteLine("Q. 退出游戏(注意: 这里不会保存游戏))");
        }

        public override bool Input(ConsoleKeyInfo key)
        {
            Console.WriteLine(); ;
            switch (key.Key)
            {
                case ConsoleKey.A:
                    // 个人信息
                    break;
                case ConsoleKey.B:
                    // 野外
                    mFSM.ChangedScene("Adventure");
                    break;
                case ConsoleKey.C:
                    // 商店
                    break;
                case ConsoleKey.D:
                    // 保存游戏
                    break;
                case ConsoleKey.Q:
                    // 退出游戏
                    Quit();
                    return false;
                default:
                    // 输出错误
                    OutputError();
                    break;
            }
            return true;
        }
    }
}
