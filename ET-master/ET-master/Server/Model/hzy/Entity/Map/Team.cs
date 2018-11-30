using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{

    public enum TeamState
    {
        /// <summary>
        /// 未准备
        /// </summary>
        Idle,
        /// <summary>
        /// 准备中
        /// </summary>
        Ready,
        /// <summary>
        /// 游戏中
        /// </summary>
        Game
    }

    public sealed class Team : Entity
    {
        public readonly Dictionary<long, int> seats = new Dictionary<long, int>();
        public readonly Gamer[] gamers = new Gamer[4];

        /// <summary>
        /// 队伍状态
        /// </summary>
        public TeamState State { get; set; } = TeamState.Idle;
        /// <summary>
        /// 玩家数量
        /// </summary>
        public int Count { get { return seats.Values.Count; } }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();
            seats.Clear();

            for (int i = 0; i < gamers.Length; i++)
            {
                if (gamers[i] != null)
                {
                    gamers[i].Dispose();
                    gamers[i] = null;
                }
            }
            State = TeamState.Idle;
        }

    }
}
