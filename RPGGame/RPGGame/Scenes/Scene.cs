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
            name = tmp[tmp.Length-1];// 获取类名
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
            Console.WriteLine("按任意键继续");
            Console.ReadKey();
        }
    }

}
