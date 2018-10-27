using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public  class Player : Role
    {
        public Player(string name)
        {
            mName = name;
            string text = string.Join("~", name.ToCharArray());
            Console.WriteLine("孩子, 当你出生的时候, 洛丹伦的森林轻声唤出了你的名字, {0}~", text);
        }
    }
}
