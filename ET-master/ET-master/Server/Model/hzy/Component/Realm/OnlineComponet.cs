using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class OnlineComponet : Component
    {
        private readonly Dictionary<long, int> dictionary = new Dictionary<long, int>();

        /// <summary>
        /// 添加在线玩家
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gateAppId"></param>
        public void Add(long userId, int gateAppId)
        {
            dictionary.Add(userId, gateAppId);
        }

        /// <summary>
        /// 获取在线玩家网关服务器ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Get(long userId)
        {
            int gateAppId;
            dictionary.TryGetValue(userId, out gateAppId);
            return gateAppId;
        }

        /// <summary>
        /// 移除玩家
        /// </summary>
        /// <param name="userId"></param>
        public void Remove(long userId)
        {
            dictionary.Remove(userId);
        }

    }
}
