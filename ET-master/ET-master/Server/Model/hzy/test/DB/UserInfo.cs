using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    /// <summary>
    /// 用户账号信息
    /// </summary>
    [BsonIgnoreExtraElements]
    public class UserInfo: Entity
    {
        public string Account;

        public string PassWord;
    }
}
