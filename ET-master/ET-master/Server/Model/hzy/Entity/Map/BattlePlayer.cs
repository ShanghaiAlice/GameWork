using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [Serializable]
    public partial class BattlePlayer:PlayerBattleInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id;
        /// <summary>
        /// 昵称
        /// </summary>
        public string name;
        /// <summary>
        /// 等级
        /// </summary>
        public int level;
        /// <summary>
        /// 主武器
        /// </summary>
        public Weapon mainWeapon;
        /// <summary>
        /// 副武器
        /// </summary>
        public Weapon secondWeapon;
        /// <summary>
        /// 星之辉
        /// </summary>
        public int money;
    }

    [Serializable]
    public class Weapon
    {
        /// <summary>
        /// 武器ID
        /// </summary>
        public int weaponId;
        /// <summary>
        /// 武器名称
        /// </summary>
        public string weaponName;
        /// <summary>
        /// 攻击力
        /// </summary>
        public int atk;
        /// <summary>
        /// 类型
        /// </summary>
        public int type;
        /// <summary>
        /// 剩余子弹数
        /// </summary>
        public int bullet;
        /// <summary>
        /// 最大子弹数
        /// </summary>
        public int bulletMax;
        /// <summary>
        /// 属性
        /// </summary>
        public Nature nature;
        /// <summary>
        /// 品阶
        /// </summary>
        public Quality quality;
    }

    [Serializable]
    public class PlayerBattleInfo
    {
        /// <summary>
        /// 属性
        /// </summary>
        public Nature nature;
        /// <summary>
        /// 攻击
        /// </summary>
        public int atk;
        /// <summary>
        /// 生命
        /// </summary>
        public int hp;
        /// <summary>
        /// 速度
        /// </summary>
        public int spd;
        /// <summary>
        /// 防御
        /// </summary>
        public int def;
        /// <summary>
        /// 闪避 
        /// </summary>
        public int dr;
        /// <summary>
        /// 暴击
        /// </summary>
        public int crd;
        /// <summary>
        /// 暴击率
        /// </summary>
        public int crdPer;
        /// <summary>
        /// 护盾
        /// </summary>
        public int shield;
    }

}
