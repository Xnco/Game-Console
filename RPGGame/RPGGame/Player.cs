using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class Player
    {
        public Player(string name)
        {
            mName = name;

            Console.WriteLine("孩子, 当你出生的时候, 洛丹伦的森林轻声唤出了你的名字, {0}~", name);
        }

        private string mName;
        private int mLevel;
        private int mExp;
        private float mHP;
        private float mMaxHP;
        private float mMP;
        private float mMaxMP;

        public string pName { get { return mName; } }

        public int pLevel
        {
            get
            {
                return mLevel;
            }
            set
            {
                mLevel = value;
            }
        }

        public int pExp
        {
            get
            {
                return mExp;
            }

            set
            {
                mExp = value;
            }
        }

        public float pHP
        {
            get
            {
                return mHP;
            }

            set
            {
                mHP = value;
            }
        }

        public float pMaxHP
        {
            get
            {
                return mMaxHP;
            }

            set
            {
                mMaxHP = value;
            }
        }

        public float pMP
        {
            get
            {
                return mMP;
            }

            set
            {
                mMP = value;
            }
        }

        public float pMaxMP
        {
            get
            {
                return mMaxMP;
            }

            set
            {
                mMaxMP = value;
            }
        }

    }
}
