using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGGame
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("请输入勇士的名字开始游戏");
            //Player mPlayer = new Player(Console.ReadLine());
            //GameManager.GetSingle().mPlayer = mPlayer;

            //Console.Write("按任意键开始游戏...");
            //Console.ReadKey();
            GameManager.GetSingle().Home();

            //Thread t = new Thread(Input);
            //t.Start();
        }
        
        //static void Input()
        //{
        //    while (true)
        //    {
        //        GameManager.GetSingle().Home(Console.ReadKey());
        //    }
        //}
    }
}
