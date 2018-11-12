using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCore
{
    struct Vector2
    {
        public int x;
        public int y;

        // 不管写不写构造方法，无参的一定会存在
        // 结构体的构造函数一定要有参数
        // 结构体的构造函数一定要给所有成员赋值
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
