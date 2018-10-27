using System;
using RPGGame.Scenes;

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
            // 加载本地文件

            // 初始化场景状态机
            InitFSM();
        }

        // 主角
        public Player mPlayer;

        private StateMachine mState;

        void InitFSM()
        {
            // 初始化需要的界面
            mState = new StateMachine();
           
            mState.Add(new Start(mState)); // 开始界面
            mState.Add(new Home(mState)); // Home界面
            mState.Add(new Adventure(mState)); // 探险界面

            Console.WriteLine("...");
            Console.WriteLine("所有界面初始化完成, 按任意键继续...");
            Console.ReadKey(true);
        }

        public bool InputScnee(ConsoleKeyInfo key)
        {
             return mState.InputScene(key);
        }

        public void ChangedScene(string name)
        {
            mState.ChangedScene(name);
        }
    }
}
