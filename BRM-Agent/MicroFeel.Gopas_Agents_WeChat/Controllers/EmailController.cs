using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using MicroFeel.Pramy.Agent.Models;
using MicroFeel.Pramy.Agent.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class EmailController : BaseController
    {
        public EmailController(IOptions<SenparcWeixinSetting> weixinSetting)
       : base(weixinSetting)
        {
        }
        public IActionResult SendEmail(string code, string state)
        {
            var util = new GetOpenIdUtil(WeixinSetting);
            var model = util.GetOpenId(code);
            var url = $"http://localhost:8001/apiAgent/ExistAgentByOpenID?openid={model.openid}";
            //var url = $"{BaseUrl}Agent/ExistAgentByOpenID?openid={model.openid}";
            var task = url.GetStringAsync();
            var result = JsonConvert.DeserializeObject<AuthModel>(task.Result);

            if (result.Code.Equals("200"))
            {
                ViewData["openId"] = model.openid;
                return View();
            }

            var authModel = new AuthorizationModel { RedirectController = "Email", RedirectAction = "AuthSendEmail", OpenId = model.openid };
            return RedirectToAction("Authorization", "Authorization", authModel);

        }

        public ViewResult AuthSendEmail(string openId)
        {
            ViewData["openId"] = openId;
            return View("~/Views/Email/SendEmail.cshtml");
        }
    }
}