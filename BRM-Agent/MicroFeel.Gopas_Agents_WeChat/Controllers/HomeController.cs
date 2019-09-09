using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using CustomMessageHandler = MicroFeel.Pramy.Agent.MessageHandles.CustomMessageHandle.CustomMessageHandler;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<SenparcWeixinSetting> weixinSetting)
        :base(weixinSetting)
        {
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：https://v.gopas.com.cn/brmagent
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, WeixinSetting.Token))
                return Content("failed:" + postModel.Signature + "," +
                               CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce,
                                   WeixinSetting.Token) + "。" +
                               "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            Log.Info(echostr);
            return Content(echostr); //返回随机字符串则表示验证通过

        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// PS：此方法为简化方法，效果与OldPost一致。
        /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel/*,[FromBody]string requestXml*/)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, WeixinSetting.Token))
            {
                return Content("参数错误！");
            }

            #region 打包 PostModel 信息

            postModel.Token = WeixinSetting.Token;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = WeixinSetting.EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = WeixinSetting.WeixinAppId;//根据自己后台的设置保持一致

            #endregion
            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var body = new StreamReader(Request.Body).ReadToEnd();
            var requestData = Encoding.UTF8.GetBytes(body);
            Stream inputStream = new MemoryStream(requestData);

            var messageHandler = new CustomMessageHandler(WeixinSetting, inputStream, postModel, Log, 10);
            
            try
            {
                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;
                //执行微信处理过程
                messageHandler.Execute();
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
            }
            catch (Exception ex)
            {
                #region 异常处理
                Log.Error($"MessageHandler错误：{ex.Message}");
                return Content("");
                #endregion
            }
        }

        /// <summary>
        /// 最简化的处理流程（不加密）
        /// </summary>
        [HttpPost]
        [ActionName("MiniPost")]
        public ActionResult MiniPost(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, WeixinSetting.Token))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            postModel.Token = WeixinSetting.Token;
            postModel.EncodingAESKey = WeixinSetting.EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = WeixinSetting.WeixinAppId;//根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(WeixinSetting, Request.Body, postModel, Log, 10);

            messageHandler.Execute();//执行微信处理过程

            return new FixWeixinBugWeixinResult(messageHandler);//v0.8+
        }


    }
}
