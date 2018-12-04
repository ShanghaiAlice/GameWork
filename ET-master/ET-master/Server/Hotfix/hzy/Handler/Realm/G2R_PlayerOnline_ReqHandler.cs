using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public class G2R_PlayerOnline_ReqHandler : AMRpcHandler<G2R_PlayerOnline_Req,R2G_PlayerOnline_Ack>
    {
        protected override async void Run(Session session, G2R_PlayerOnline_Req message, Action<R2G_PlayerOnline_Ack> reply)
        {
            R2G_PlayerOnline_Ack response = new R2G_PlayerOnline_Ack();

            try
            {
                OnlineComponet onlineComponet = Game.Scene.GetComponent<OnlineComponet>();

                //将玩家踢下线
                await RealmHelper.KickOutPlayer(message.UserID);

                onlineComponet.Add(message.UserID, message.GateAppID);
                Log.Info($"玩家{message.UserID}上线");
                reply?.Invoke(response);
            }
            catch (Exception ex)
            {
                ReplyError(response, ex, reply);
            }
        }
    }
}
