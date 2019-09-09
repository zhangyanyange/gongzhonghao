using System;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using Flurl.Http;
namespace MQ.WeChat.Utilities
{
    public class GetOpenIdUtil
    {
        readonly string _appId;
        readonly string _appSercet;
        public GetOpenIdUtil(SenparcWeixinSetting senparcWeixinSetting)
        {        
            _appId = senparcWeixinSetting.WeixinAppId;
            _appSercet = senparcWeixinSetting.WeixinAppSecret;
           
        }


        public OpenId GetOpenId(string code)
        {
            var url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={_appId}&secret={_appSercet}&code={code}&grant_type=authorization_code";
            var result = url.GetStringAsync();
            var model = JsonConvert.DeserializeObject<OpenId>(result.Result);//反序列化
            return model;
        }
    }

    public class OpenId
    {
        public string openid { get; set; }
    }
}