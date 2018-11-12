using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeping
{
    class Box
    {
        public Vector2 position; // 坐标
        public int num; // 周围雷数
        public bool isMine; // 是否是雷
        public bool isOpen; // 是否翻开
        public bool isFlag; // 是否标记
    }
}
