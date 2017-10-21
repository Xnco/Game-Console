using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class GameManager
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

        private GameManager() { }

        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void InitGame()
        {
            // 加载本地文件

        }
    }
}
