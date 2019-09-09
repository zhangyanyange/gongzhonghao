using System;
using System.Collections.Generic;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MQ.WeChat.Config;
using MQ.WeChat.Models;
using MQ.WeChat.Models.Ticket;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;

namespace MQ.WeChat.Controllers
{
    public class FunctionController : BaseController
    {

        public FunctionController(IOptions<SenparcWeixinSetting> weixinSetting, IOptions<BaseConfig> setting, IMemoryCache memoryCache)
            : base(weixinSetting, setting, memoryCache)
        { }

        [Obsolete("使用 AccessTokenContainer.GetAccessToken 方法代替此方法", true)]
        public AccessToken GetToken()
        {
            var token = Cache.Get<AccessToken>(WeixinSetting.WeixinAppId);
            if (token != null && token.ExpDateTime >= DateTime.Now)
            {
                goto label_1;
            }
            var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={WeixinSetting.WeixinAppId}&secret={WeixinSetting.WeixinAppSecret}";
            var task = url.GetStringAsync();
            token = JsonConvert.DeserializeObject<AccessToken>(task.Result);
            token.ExpDateTime = DateTime.Now.AddMinutes(110);
            Cache.Set(WeixinSetting.WeixinAppId, token);
            label_1:
            return token;
        }

        public ErrorMsg CreateMenu()
        {
            var token = AccessTokenContainer.GetAccessToken(WeixinSetting.WeixinAppId);
            var url = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={token}";
            const string redirectUri = "https://meiquewechat.microfeel.net/order/OrderList";
            var oauth2Uri =
            $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={WeixinSetting.WeixinAppId}&redirect_uri={redirectUri}&response_type=code&scope=snsapi_base#wechat_redirect";
            var menuView = new MenuViewModel { name = "订单", url = oauth2Uri };
            var menuClick = new MenuClickModel { name = "解绑", key = "Unbind"};
            //var menuView2 = new MenuViewModel { name = "Install", url = "https://meiquewechat.microfeel.net/order/Install?billID=323a6836-6e67-4e82-8549-208c4a143a16" };
#pragma warning disable 1030
#warning 二级菜单：下面 sub_button 最多有5元素
#pragma warning restore 1030
            //var menu = new MenuModel {name = "parentMenu", sub_button = new List<MenuParentModel> { menuView, menuView }};
#pragma warning disable 1030
#warning 一级菜单：下面 list 最多有3元素
#pragma warning restore 1030
            var list = new List<object> { menuView, menuClick };
            var obj = new {button = list};
            var task = url.PostJsonAsync(obj).ReceiveJson<ErrorMsg>();
            return task.Result;
        }

    }
}