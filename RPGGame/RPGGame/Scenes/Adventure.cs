using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Scenes
{
    public class Adventure : Scene
    {
        public Adventure(StateMachine fsm) : base(fsm)
        {
            //Console.WriteLine("初始化 Adventure 场景 ");
        }

        public override void Init()
        {
            Console.Clear();
            Console.WriteLine("你来到了野外探险, 你可以进行一下操作:");
            Console.WriteLine("a. 练级");
            Console.WriteLine("b. 挑战Boss");
            Console.WriteLine("c.  回家");
        }

        public override bool Input(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.A:
                    // 练级
                    break;
                case ConsoleKey.B:
                    // 挑战Boss
                    break;
                case ConsoleKey.C:
                    mFSM.ChangedScene("Home");
                    // 回家
                    break;
                //break;
                default:
                    // 输出错误
                    OutputError();
                    break;
            }
            return true;
        }
    }
}
