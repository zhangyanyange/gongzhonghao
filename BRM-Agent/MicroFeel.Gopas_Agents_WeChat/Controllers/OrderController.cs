using Flurl.Http;
using MicroFeel.Pramy.Agent.Models;
using MicroFeel.Pramy.Agent.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IOptions<SenparcWeixinSetting> weixinSetting)
        :base(weixinSetting)
        {
        }
        public ActionResult OrderList(string code, string state)
        {
            var util = new GetOpenIdUtil(WeixinSetting);
            var model = util.GetOpenId(code);
            var url = $"{BaseUrl}Agent/ExistAgentByOpenID?openid={model.openid}";
            var task = url.GetStringAsync();
            Log.Info(task.Result);
            var result = JsonConvert.DeserializeObject<AuthModel>(task.Result);
                
            if (result.Code.Equals("200"))
            {
                ViewData["openId"] = model.openid;
                return View();
            }

            var authModel = new AuthorizationModel { RedirectController = "Order", RedirectAction = "AuthOrderList", OpenId = model.openid };
            return RedirectToAction("Authorization", "Authorization", authModel);

        }

        public ViewResult AuthOrderList(string openId)
        {
            ViewData["openId"] = openId;
            return View("~/Views/Order/OrderList.cshtml");
        }

        public ViewResult OrderDetail(string openid, string finterid, string FBillNo, string State, string FDate, string FCustom, string StateMsg, string FStatus)
        {
            if (!StateMsg.Equals("已出库"))
            {
                ViewData["finterid"] = finterid;
                var order = new Order
                {
                    openid = openid,
                    FBillNo = FBillNo,
                    State = State,
                    FCustom = FCustom,
                    FDate = (FDate != null && FDate.Length > 10) ? FDate.Substring(0, 10) : FDate ?? "",
                    StateMsg = StateMsg,
                    FStatus = FStatus
                };
                return View(order);
            }
            ViewData["openid"] = openid;
            ViewData["interid"] = finterid;
            ViewData["title"] = "出库详情";
            Log.Info(openid);
            return View("~/Views/Material/SettlementSheet.cshtml");
        }
    }
}