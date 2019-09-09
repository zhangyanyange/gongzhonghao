
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Flurl.Http;
using MicroFeel.Pramy.Agent.Controllers;
using MicroFeel.Pramy.Agent.Models;
using Newtonsoft.Json;

namespace MicroFeel.Pramy.Agent.MessageHandles.CustomMessageHandle
{
    public partial class CustomMessageHandler
    {

        /// <summary>
        /// 打开网页事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            //说明：这条消息只作为接收，下面的responseMessage到达不了客户端，类似OnEvent_UnsubscribeRequest
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您点击了view按钮，将打开网页：" + requestMessage.EventKey;
            _log.Info("您点击了view按钮");
            return responseMessage;
        }

        

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            _log.Info("进入Subscribe事件");
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "你好，欢迎关注柏瑞美代理商公众号！";
            return responseMessage;
        }

        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "有空再来";
            return responseMessage;
        }
         /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage;
            //菜单点击，需要跟创建菜单时的Key匹配
            _log.Info("进入Click事件");
            _log.Info(requestMessage.EventKey);
            if (requestMessage.EventKey != "Unbind") return null;
            _log.Info("进入Unbind事件");
            var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
            reponseMessage = strongResponseMessage;

            var url = $"{BaseController.BaseUrl}Agent/ExistAgentByOpenID?openid={requestMessage.FromUserName}";
            var openIdTask = url.GetStringAsync();
            var content = openIdTask.Result;
            strongResponseMessage.Content = content;
            var result = JsonConvert.DeserializeObject<AuthModel>(content);
            if (result.Code.Equals("200"))
            {
                var unbind = new UnbindOpenId {OpenID = requestMessage.FromUserName};
                var url1 = $"{BaseController.BaseUrl}Agent/UnbindAgent";
                var task = url1.PostJsonAsync(unbind).ReceiveJson<AuthModel>();
                var unbindResult = task.Result;
                strongResponseMessage.Content = unbindResult.Code.Equals("200") ? "解绑成功" : "解绑失败";
            }
            else
            {
                strongResponseMessage.Content = "当前用户未绑定";
            }
            

            return reponseMessage;
        }
        /// <summary>
        /// 事件之发送模板消息返回结果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_TemplateSendJobFinishRequest(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            switch (requestMessage.Status)
            {
                case "success":
                    //发送成功

                    break;
                case "failed:user block":
                    //送达由于用户拒收（用户设置拒绝接收公众号消息）而失败
                    break;
                case "failed: system failed":
                    //送达由于其他原因失败
                    break;
                default:
                    throw new WeixinException("未知模板消息状态：" + requestMessage.Status);
            }

            //注意：此方法内不能再发送模板消息，否则会造成无限循环！

            
            return null;
        }

    }
}
