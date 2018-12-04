using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_PlayerOffline_ReqHandler : AMRpcHandler<G2R_PlayerOffline_Req, R2G_PlayerOffline_Ack>
    {
        protected override async void Run(Session session, G2R_PlayerOffline_Req message, Action<R2G_PlayerOffline_Ack> reply)
        {
            R2G_PlayerOffline_Ack response = new R2G_PlayerOffline_Ack();
            try
            {
                PlayerInfos userInfo = await Game.Scene.GetComponent<DBProxyComponent>().Query<PlayerInfos>(message.UserID, false);
                userInfo.lastActive = DateTime.Now;
                Game.Scene.GetComponent<OnlineComponet>().Remove(message.UserID);
                Log.Info($"玩家{message.UserID}下线");
                DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();
                await dBProxy.Save(userInfo, false);
            }
            catch (Exception ex)
            {
                ReplyError(response, ex, reply);
            }
        }
    }
}
