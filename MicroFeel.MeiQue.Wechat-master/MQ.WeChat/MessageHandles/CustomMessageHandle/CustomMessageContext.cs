﻿using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;


namespace MQ.WeChat.MessageHandles.CustomMessageHandle
{
    public class CustomMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
    {
        public CustomMessageContext()
        {
            base.MessageContextRemoved += CustomMessageContext_MessageContextRemoved;
        }

        protected virtual void CustomMessageContext_MessageContextRemoved(object sender, WeixinContextRemovedEventArgs<IRequestMessageBase, IResponseMessageBase> e)
        {
            /* 注意，这个事件不是实时触发的（当然你也可以专门写一个线程监控）
             * 为了提高效率，根据WeixinContext中的算法，这里的过期消息会在过期后下一条请求执行之前被清除
             */

            if (!(e.MessageContext is CustomMessageContext))
            {
            }

            //TODO:这里根据需要执行消息过期时候的逻辑，下面的代码仅供参考

            //Log.InfoFormat("{0}的消息上下文已过期",e.OpenId);
            //api.SendMessage(e.OpenId, "由于长时间未搭理客服，您的客服状态已退出！");
        }
    }
}
