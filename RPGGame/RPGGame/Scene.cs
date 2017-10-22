using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    /// <summary>
    /// 场景基类
    /// </summary>
    public abstract class Scene
    {
        public string name;
        public StateMachine mFSM;

        public Scene(StateMachine fsm)
        {
            string[] tmp = this.GetType().FullName.Split('.') ;
            name = tmp[1];// 获取类名
            Console.WriteLine("初始化{0}场景成功", name);
            mFSM = fsm;
        }

        /// <summary>
        /// 进入场景初始化界面
        /// </summary>
        public abstract void Init();

       /// <summary>
       /// 监听键盘, 返回false退出游戏
       /// </summary>
       /// <param name="key"></param>
       /// <returns>是否退出游戏</returns>
        public abstract bool Input(ConsoleKeyInfo key);

        /// <summary>
        /// 场景指令错误
        /// </summary>
        public virtual void OutputError()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("输入错误!!!!只能按上面出现的指令, 其他指令无效!!!");
            Console.WriteLine("请再重新输入指令!");
            Console.BackgroundColor = ConsoleColor.Black;


        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public virtual void Quit()
        {
            Console.Clear();
            Console.WriteLine("这游戏果然够无聊, 玩到这差不多了");

            Console.WriteLine();

            Console.WriteLine(
                "=========================================================\n" +
                "     *    *    *               *          |\n" +
                "     *    *    *                     ***********\n" +
                "     *    *    *              ****        |\n" +
                "**********************          *     *********\n" +
                "       *     *                  *     |   |   |\n" +
                "      *      *                  *     *********\n" +
                "     *       *      *           *       * | *\n" +
                "    *        *      *           *     *   |   *\n" +
                "  *          *      *           *  *      |     *\n" +
                "*            ********          ********************\n" +
                 "=========================================================\n"
                );
        }
    }

    public class Start : Scene
    {
        public Start(StateMachine fsm) : base(fsm)
        {
            //Console.WriteLine("初始化Start场景");
        }

        public override void Init()
        {
            Console.WriteLine("请输入勇士的名字开始游戏");
            Player mPlayer = new Player(Console.ReadLine());
            GameManager.GetSingle().mPlayer = mPlayer;

            Console.WriteLine("按任意键开始游戏...");
        }

        public override bool Input(ConsoleKeyInfo key)
        {
            // 无论按什么都直接切换到下一个场景
            return true;
        }
    }

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
                    return false;
                default:
                    // 输出错误
                    OutputError();
                    break;
            }
            return true;
        }
    }

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
