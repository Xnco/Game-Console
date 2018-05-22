using System;


namespace RPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager mManager = GameManager.GetSingle();
            // 界面初始化完成后进入 初始界面
            mManager.ChangedScene(AppConst.InitScene);

            // 游戏运行后不停接受指令即可
            bool isPlaying = true;
            while (isPlaying)
            {
                isPlaying = mManager.InputScnee(Console.ReadKey(false));
            }
        }
    }
}
