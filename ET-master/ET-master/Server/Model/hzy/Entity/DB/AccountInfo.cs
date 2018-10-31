using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    
    [BsonIgnoreExtraElements]
    public class AccountInfo:Entity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string account;
        /// <summary>
        /// 密码
        /// </summary>
        public string password;
        /// <summary>
        /// 玩家游戏ID
        /// </summary>
        public int pid;
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime registerTime;
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime lastLogin;
    }
}
