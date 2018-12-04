using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_Register_ReqHandler : AMRpcHandler<C2R_Register_Req,R2C_Register_Ack>
    {
        protected override async void Run(Session session, C2R_Register_Req message, Action<R2C_Register_Ack> reply)
        {
            R2C_Register_Ack response = new R2C_Register_Ack();
            try
            {
                //数据库操作对象
                DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

                List<ComponentWithId> result = await dBProxy.Query<AccountInfo>(_account => _account.account == message.Account);
                if (result.Count > 0)
                {
                    response.Error = ErrorCode.ERR_AccountAlreadyRegister;
                    reply?.Invoke(response);
                    return;
                }

                List<ComponentWithId> accountCount = await dBProxy.Query<PlayerInfos>(e=>e.pid != -1);
                var pid = accountCount.Count + 1;
                //新建账号
                AccountInfo newAccount = ComponentFactory.CreateWithId<AccountInfo>(IdGenerater.GenerateId());
                newAccount.account = message.Account;
                newAccount.password = message.Password;
                newAccount.pid = pid;
                newAccount.registerTime = DateTime.Now;

                Log.Info($"注册新账号: {MongoHelper.ToJson(newAccount)}");

                //新建用户信息
                var newPlayer = ComponentFactory.CreateWithId<PlayerInfos>(newAccount.Id);
                newPlayer.name = $"用户{message.Name}";
                newPlayer.level = 1;
                newPlayer.exp = 0;
                newPlayer.money = 1000;
                newPlayer.pid = pid;

                await dBProxy.Save(newAccount);
                await dBProxy.Save(newPlayer, false);

                reply?.Invoke(response);

            }
            catch (Exception ex)
            {
                ReplyError(response, ex, reply);
            }
        }

    }
}
