using System;
using System.Net;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override void Run(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			RunAsync(session, message, reply).NoAwait();
		}

		private async ETVoid RunAsync(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			R2C_Login response = new R2C_Login();
			try
            {
                //if (message.Account != "hzy" || message.Password != "123")
                //{
                //    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                //    reply(response);
                //    return;
                //}
                DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();
                UserInfo newUserInfo = ComponentFactory.Create<UserInfo>();
                newUserInfo.Account = message.Account;
                newUserInfo.PassWord = message.Password;
                await dBProxy.Save(newUserInfo);
                try
                {
                    List<ComponentWithId> users = await dBProxy.Query<UserInfo>( t =>t.Account == message.Account);
                    if (users.Count > 0)
                    {
                        UserInfo user = await dBProxy.Query<UserInfo>(users[0].Id);
                        Log.Error("user的内容:" + JsonHelper.ToJson(user));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("查库错误");
                    throw;
                }

                // 随机分配一个Gate
                StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
				//Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
				IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
				Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

				// 向gate请求一个key,客户端可以拿着这个key连接gate
				G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey {Account = message.Account});

				string outerAddress = config.GetComponent<OuterConfig>().Address2;

				response.Address = outerAddress;
				response.Key = g2RGetLoginKey.Key;
                reply?.Invoke(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}