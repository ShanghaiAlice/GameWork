using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class UserAwakeSystem : AwakeSystem<User,long>
    {
        public override void Awake(User self, long id)
        {
            self.Awake(id);
        }
    }

    public sealed class User : Entity
    {
        /// <summary>
        /// 玩家ID
        /// </summary>
        public int pid { get; private set; }
        /// <summary>
        /// 是否在匹配中
        /// </summary>
        public bool isMatching { get; set; }
        /// <summary>
        /// Gate转发ActorID
        /// </summary>
        public long ActorID { get; set; }
        public void Awake(long id)
        {
            this.pid = (int)id;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();
            this.isMatching = false;
            this.ActorID = 0;
        }

    }

}
