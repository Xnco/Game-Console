using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Enemy
{
    class EnemyFactory
    {
        public static Enemy Create(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Slem:
                    //return new Enemy();
                default:
                    return null;
            }
        }
    }
}
