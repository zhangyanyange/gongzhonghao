/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：CustomMessageHandler.cs
    文件功能描述：微信公众号自定义MessageHandler


    创建标识：Senparc - 20150312

    修改标识：Senparc - 20171027
    修改描述：v14.8.3 添加OnUnknownTypeRequest()方法Demo

----------------------------------------------------------------*/

using System.IO;
using log4net;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace MicroFeel.Pramy.Agent.MessageHandles.CustomMessageHandle
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        private ILog _log;
        public CustomMessageHandler(SenparcWeixinSetting weixinSetting, Stream inputStream, PostModel postModel, ILog log, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            _log = log;
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
            //在指定条件下，不使用消息去重
            OmitRepeatedMessageFunc = requestMessage => !(requestMessage is RequestMessageText textRequestMessage) || textRequestMessage.Content != "容错";
        }

        public CustomMessageHandler(Senparc.Weixin.MP.Entities.RequestMessageBase requestMessage)
            : base(requestMessage)
        {
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }

        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            _log.Info("bbbb");
            return new SuccessResponseMessage();
        }

        public override Senparc.Weixin.MP.Entities.IResponseMessageBase DefaultResponseMessage(Senparc.Weixin.MP.Entities.IRequestMessageBase requestMessage)
        {

            _log.Info("aaaa");
            return new SuccessResponseMessage();
        }
    }
}
