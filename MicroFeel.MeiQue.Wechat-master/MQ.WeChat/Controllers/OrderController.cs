using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MQ.WeChat.Config;
using MQ.WeChat.Models;
using MQ.WeChat.Models.Ticket;
using MQ.WeChat.Utilities;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace MQ.WeChat.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IOptions<SenparcWeixinSetting> wxSettingOptions, IOptions<BaseConfig> setting)
        :base(wxSettingOptions, setting)
        {}

        public IActionResult OrderList(string code)
        {

            var util = new GetOpenIdUtil(WeixinSetting);
            var model = util.GetOpenId(code);

            //验证openId是否存在接口
            var url = $"{Setting.BaseUrl}WeChat/Openid?Openid={model.openid}";

            var task = url.GetStringAsync();
            var result = JsonConvert.DeserializeObject<AuthModel>(task.Result);

            if (result.result == 0)
            {
                ViewData["openId"] = model.openid;
                return View();
            }

            var authorizationModel = new AuthorizationModel { RedirectController = "Order", RedirectAction = "AuthOrderList", OpenId = model.openid };
            return RedirectToAction("Authorization", "Authorization", authorizationModel);

            //ViewData["openId"] = model.openid;
            //var authorizationModel = new AuthorizationModel { RedirectController = "Order", RedirectAction = "AuthOrderList" };
            //return View("~/Views/Authorization/Authorization.cshtml", authorizationModel);
        }

        public IActionResult AuthOrderList(string openId)
        {
            ViewData["openId"] = openId;
            return View("~/Views/Order/OrderList.cshtml");
        }

        public IActionResult OrderDetail(string billID)
        {
            ViewData["billID"] = billID;
            return View();
        }

        public IActionResult Delivery(string billID) {
            Ticket ticket= TicketController.GetTicket("https://meiquewechat.microfeel.net/Order/Delivery?billID="+billID);
            ViewData["billID"] = billID;
            ViewData["nonceStr"]= ticket.nonceStr;
            ViewData["signature"] = ticket.signature;
            ViewData["timestamp"] = ticket.timestamp;
            return View();
        }

        public IActionResult Install(string billID)
        {
            Ticket ticket = TicketController.GetTicket("https://meiquewechat.microfeel.net/Order/Install?billID=" + billID);
            ViewData["billID"] = billID;
            ViewData["nonceStr"] = ticket.nonceStr;
            ViewData["signature"] = ticket.signature;
            ViewData["timestamp"] = ticket.timestamp;
            return View();
        }
    }
}