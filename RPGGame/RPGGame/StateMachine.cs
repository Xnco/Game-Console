using System;
using System.Collections.Generic;

namespace RPGGame
{
    public class StateMachine
    {
        /// <summary>
        /// 保存所有场景
        /// </summary>
        Dictionary<string, Scene> allScene;
        /// <summary>
        /// 当前所在场景
        /// </summary>
        Scene CurScene;

        public StateMachine()
        {
            allScene = new Dictionary<string, Scene>();
            CurScene = null;
        }

        // 添加Scene
        public void Add(Scene scene)
        {
            if (scene == null)
            {
                return;
            }

            if (!allScene.ContainsKey(scene.name))
            {
                allScene.Add(scene.name, scene);
            }
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
                OutputError("当前不可以输入指令, 当前场景错误");
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
