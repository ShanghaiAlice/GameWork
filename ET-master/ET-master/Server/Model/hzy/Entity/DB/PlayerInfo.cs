using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [BsonIgnoreExtraElements]
   public class PlayerInfo:Entity
    {
        /// <summary>
        /// 玩家Id
        /// </summary>
        public int pid;
        /// <summary>
        /// 昵称
        /// </summary>
        public string name;
        /// <summary>
        /// 等级
        /// </summary>
        public int level;
        /// <summary>
        /// 经验
        /// </summary>
        public int exp;
        /// <summary>
        /// 队伍ID（不在队伍则为-1）
        /// </summary>
        public int teamId;
        /// <summary>
        /// 技能
        /// </summary>
        public List<Skill> skill;
        /// <summary>
        /// 天晶
        /// </summary>
        public int money;
        /// <summary>
        /// 初始武器，无武器:0
        /// </summary>
        public int weapon;
        /// <summary>
        /// 上次最后活跃时间
        /// </summary>
        public DateTime lastActive;
    }
}
