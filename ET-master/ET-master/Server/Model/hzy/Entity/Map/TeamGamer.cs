
namespace ETModel
{
    public class GamerAwakeSystem : AwakeSystem<Gamer, long>
    {
        public override void Awake(Gamer self, long id)
        {
            self.Awake(id);
        }

    }

    public sealed class Gamer : Entity
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
        /// 队伍ID
        /// </summary>
        public long teamId;
        /// <summary>
        /// 是否准备
        /// </summary>
        public bool isReady;
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool isOnline;

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

            this.pid = 0;
            this.gid = 0;
            this.teamId = 0;
            this.isReady = false;
            this.isOnline = false;
        }
    }
}
