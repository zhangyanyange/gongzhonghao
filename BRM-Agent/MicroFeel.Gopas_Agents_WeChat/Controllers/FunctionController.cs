
using System.Collections.Generic;
using Flurl.Http;
using MicroFeel.Pramy.Agent.Models.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class FunctionController : BaseController
    {
        public FunctionController(IOptions<SenparcWeixinSetting> weixinSetting)
        :base(weixinSetting)
        {  
        }

        [HttpGet]
        public ErrorMsg CreateMenu()
        {
            var token = AccessTokenContainer.GetAccessToken(WeixinSetting.WeixinAppId);
            var url = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={token}";
            var redirectUri1 = "https://brmagent.gopas.com.cn/material/MaterialType";
            var redirectUri2 = "https://brmagent.gopas.com.cn/order/OrderList";
            var oauth2Uri1 =
                $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={WeixinSetting.WeixinAppId}&redirect_uri={redirectUri1}&response_type=code&scope=snsapi_base#wechat_redirect";
            var menuView1 = new MenuViewModel { name = "创建订单", url = oauth2Uri1 };
            var oauth2Uri2 =
                $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={WeixinSetting.WeixinAppId}&redirect_uri={redirectUri2}&response_type=code&scope=snsapi_base#wechat_redirect";
            var menuView2 = new MenuViewModel { name = "订单查询", url = oauth2Uri2 };
            var menuClick = new MenuClickModel { name = "解绑", key = "Unbind" };
#pragma warning disable 1030
#warning 二级菜单：下面 sub_button 最多有5元素
#pragma warning restore 1030
            //var menu = new MenuModel {name = "parentMenu", sub_button = new List<MenuParentModel> { menuView, menuView }};
#pragma warning disable 1030
#warning 一级菜单：下面 list 最多有3元素
#pragma warning restore 1030
            var list = new List<object> { menuView1, menuView2, menuClick };
            var obj = new { button = list };
            var task = url.PostJsonAsync(obj).ReceiveJson<ErrorMsg>();
            return task.Result;
        }
    }
}