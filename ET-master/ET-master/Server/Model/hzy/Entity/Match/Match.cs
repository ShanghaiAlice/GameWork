using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class MatcherAwakeSystem : AwakeSystem<Matcher, long>
    {
        public override void Awake(Matcher self, long id)
        {
            self.Awake(id);
        }
    }

    public sealed class Matcher : Entity
    {
        /// <summary>
        /// 玩家ID
        /// </summary>
        public int pid;
        /// <summary>
        /// 玩家gateID
        /// </summary>
        public long gid;
        /// <summary>
        /// 客户端与网关服务器的sessionID
        /// </summary>
        public long gateSessionID;

        public void Awake(long id)
        {
            this.pid = (int)id;
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();

            this.pid = 0;
            this.gid = 0;
            this.gateSessionID = 0;
        }
    }
}
