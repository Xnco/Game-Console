using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class GameManager
    {
        private static GameManager instance;

        public static GameManager GetSingle()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        private GameManager()
        {
            InitGame();
        }

        public Player mPlayer;

        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void InitGame()
        {
            // 加载本地文件

        }

        /// <summary>
        /// 家
        /// </summary>
        public void Home()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("欢迎回家, 你可以进行一下操作:");
                Console.WriteLine("a. 查看个人信息");
                Console.WriteLine("b. 野外探险");
                Console.WriteLine("c. 前往商店");
                Console.WriteLine("d. 保存游戏(根据你的游戏名字保存)");
                Console.WriteLine("Q. 退出游戏(注意: 这里不会保存游戏))");
                ConsoleKeyInfo tmp = Console.ReadKey(true);
                Console.WriteLine();;
                switch (tmp.Key)
                {
                    case ConsoleKey.A:
                        // 个人信息
                        break;
                    case ConsoleKey.B:
                        // 野外
                        Adventure();
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
                        return;
                    default:
                        // 输出错误
                        InputError();
                        //tmp = Console.ReadKey(true);
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// 探险
        /// </summary>
        public void Adventure()
        {
            Console.Clear();
            Console.WriteLine("你来到了野外探险, 你可以进行一下操作:");
            Console.WriteLine("a. 练级");
            Console.WriteLine("b. 挑战Boss");
            Console.WriteLine("c.  回家");
            ConsoleKeyInfo tmp = Console.ReadKey(true);
            Console.WriteLine();
            bool inputOK;
            do
            {
                inputOK = true;
                switch (tmp.Key)
                {
                    case ConsoleKey.A:
                        // 练级
                        break;
                    case ConsoleKey.B:
                        // 挑战Boss
                        break;
                    case ConsoleKey.C:
                        // 回家
                        return;
                        //break;
                    default:
                        // 输出错误
                        inputOK = false;
                        InputError();
                        //tmp = Console.ReadKey(true);
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// 报错
        /// </summary>
        public void InputError()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("输入错误!!!!只能按上面出现的指令, 其他指令无效!!!");
            Console.WriteLine("按回车继续后请再重新输入指令!");
            Console.ReadKey();
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public void Quit()
        {
            Console.Clear();
            Console.WriteLine("这游戏果然够无聊, 玩到这差不多了");

            Console.WriteLine();

            Console.WriteLine(
                "=========================================================\n"+
                "     *    *    *               *          |\n" +
                "     *    *    *                     ***********\n" +
                "     *    *    *              ****        |\n" +
                "**********************          *     *********\n" +
                "       *     *                  *     |   |   |\n" +
                "      *      *                  *     *********\n" +
                "     *       *      *           *       * | *\n" +
                "    *        *      *           *     *   |   *\n" +
                "  *          *      *           *  *      |     *\n" +
                "*            ********          ********************\n"+
                 "=========================================================\n"
                );
        }
    }
}
