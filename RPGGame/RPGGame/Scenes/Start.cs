using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Scenes
{

    public class Start : Scene
    {
        public Start(StateMachine fsm) : base(fsm)
        {
            //Console.WriteLine("初始化Start场景");
        }

        public override void Init()
        {
            Console.Clear();
            Console.WriteLine("请输入勇士的名字开始游戏");
            Player mPlayer = new Player(Console.ReadLine());
            GameManager.GetSingle().mPlayer = mPlayer;

            Console.WriteLine("按任意键开始游戏...");
        }

        public override bool Input(ConsoleKeyInfo key)
        {
            // 无论按什么都直接切换到下一个场景
            mFSM.ChangedScene("Home");
            return true;
        }
    }

}
