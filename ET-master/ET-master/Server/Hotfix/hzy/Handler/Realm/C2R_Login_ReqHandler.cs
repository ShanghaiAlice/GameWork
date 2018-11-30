using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public class C2R_Login_ReqHandler : AMRpcHandler<C2R_Login_Req, R2C_Login_Ack>
    {
        protected override async void Run(Session session, C2R_Login_Req message, Action<R2C_Login_Ack> reply)
        {
            R2C_Login_Ack response = new R2C_Login_Ack();

            try
            {
                DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();
                Log.Info($"登录请求：{{Account:'{message.Account}',Password:'{message.Password}'}}");

                List<ComponentWithId> result = await dBProxy.Query<AccountInfo>(_ac => _ac.account == message.Account && _ac.password == message.Password);
                if (result.Count == 0)
                {
                    response.Error = ErrorCode.ERR_LoginError;
                    reply?.Invoke(response);
                    return;
                }

                AccountInfo account = result[0] as AccountInfo;
                Log.Info($"登陆成功{MongoHelper.ToJson(account)}");

                await RealmHelper.KickOutPlayer(account.Id);
            }
    }
}
