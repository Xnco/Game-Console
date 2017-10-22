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
            Init();
        }

        public Player mPlayer;

        /// <summary>
        /// 保存所有场景
        /// </summary>
        Dictionary<string, Scene> allScene;
        /// <summary>
        /// 当前所在场景
        /// </summary>
        Scene CurScene;

        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void Init()
        {
            // 加载本地文件

            // 初始化所有场景
            InitScene();
        }

        public void InitScene()
        {
            // 初始化需要的界面
            allScene = new Dictionary<string, Scene>();
            allScene.Add("Start", new Start()); // 开始界面
            allScene.Add("Home", new Home()); // Home界面
            allScene.Add("Adventure", new Adventure()); // 探险界面
            Console.WriteLine("...");
            Console.WriteLine("所有界面初始化完成, 按任意键继续...");
            Console.ReadKey(true);

            // 界面初始化完成后进入 初始界面
            ChangedScene(AppConst.InitScene);
        }

        // 切换界面
        public void ChangedScene(string varName)
        {
            Scene scene;
            if (allScene.TryGetValue(varName, out scene))
            {
                CurScene = scene;
                CurScene.Init();
            }
            else
            {
                OutputError("切换场景失败, 没有这个场景");
            }
        }

        // 界面接受指令
        public bool InputScene(ConsoleKeyInfo key)
        {
            if (CurScene != null)
            {
                return CurScene.Input(key);
            }
            else
            {
                OutputError("接受失败, 当前场景错误");
                return true;
            }
        }

        public void OutputError(string error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
