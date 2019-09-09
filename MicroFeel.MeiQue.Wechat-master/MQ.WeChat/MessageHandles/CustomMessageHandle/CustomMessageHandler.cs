/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：CustomMessageHandler.cs
    文件功能描述：微信公众号自定义MessageHandler


    创建标识：Senparc - 20150312

    修改标识：Senparc - 20171027
    修改描述：v14.8.3 添加OnUnknownTypeRequest()方法Demo

----------------------------------------------------------------*/

using System.IO;
using MQ.WeChat.Config;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.Entities;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;

namespace MQ.WeChat.MessageHandles.CustomMessageHandle
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        private string _appId;
        private SenparcWeixinSetting _senparcWeixinSetting;
        private BaseConfig _settingConfig;
        public CustomMessageHandler(SenparcWeixinSetting senparcWeixinSetting, BaseConfig settingConfig, Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            
            _senparcWeixinSetting = senparcWeixinSetting;
            _settingConfig = settingConfig;
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            GlobalMessageContext.ExpireMinutes = 3;
            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                _appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }

            //在指定条件下，不使用消息去重
            OmitRepeatedMessageFunc = requestMessage => !(requestMessage is RequestMessageText textRequestMessage) || textRequestMessage.Content != "容错";
        }

        public sealed override GlobalMessageContext<CustomMessageContext, IRequestMessageBase, IResponseMessageBase> GlobalMessageContext => base.GlobalMessageContext;

        public CustomMessageHandler(RequestMessageBase requestMessage)
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

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            return new SuccessResponseMessage();
        }
    }
}
