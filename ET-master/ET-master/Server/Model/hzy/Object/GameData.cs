using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 属性
    /// </summary>
    public enum Nature
    {
        无属性 = 0,
        风 = 1,
        土 = 2,
        水 = 3,
        火 = 4,
        光 = 5,
        暗 = 6,
    }

    /// <summary>
    /// 品阶
    /// </summary>
    public enum Quality
    {
        N = 0,
        R = 1,
        SR = 2,
        SSR = 3,
    }

    /// <summary>
    /// 技能
    /// </summary>
    [Serializable]
    public class Skill
    {
        /// <summary>
        /// 技能ID
        /// </summary>
        public int id;
        /// <summary>
        /// 等级
        /// </summary>
        public int level;
        /// <summary>
        /// 上次使用时间 
        /// </summary>
        public DateTime lastTime;
        /// <summary>
        /// cd时间
        /// </summary>
        public int cd;
    }


    /// <summary>
	/// 道具
	/// </summary>
	[Serializable()]
    public class ItemData
    {
        public int type;
        public int count;
        public ItemData() { }
        public ItemData(int type, int count)
        {
            this.type = type;
            this.count = count;
        }
        public static ItemData Parse(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            string[] args = str.Split(';');
            return new ItemData() { type = int.Parse(args[0]), count = int.Parse(args[1]) };
        }
    }
}
