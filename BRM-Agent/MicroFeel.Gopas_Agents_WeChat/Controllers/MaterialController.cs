
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using MicroFeel.Pramy.Agent.Models;
using Flurl.Http;
using MicroFeel.Pramy.Agent.Utilities;
using System;
using 美雀同步DingUserDept.Utils;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class MaterialController : BaseController
    {
        
        public MaterialController(IOptions<SenparcWeixinSetting> weixinSetting)
        :base(weixinSetting)
        {
        }

        public ViewResult MaterialType(string code, string state)
        {
            try
            {
                var util = new GetOpenIdUtil(WeixinSetting);
                var model = util.GetOpenId(code);
                var url = $"{BaseUrl}Agent/ExistAgentByOpenID?openid={model.openid}";
               var task = url.GetStringAsync();

               //var openIdExist= HttpHelper.HttpGetRequest(url);
              var result = JsonConvert.DeserializeObject<AuthModel>(task.Result);
                //ViewData["e"] = openIdExist;

                //return View("~/Views/material/Index.cshtml");
                if (result.Code.Equals("200"))
                {
                    ViewData["openId"] = model.openid;
                    var materialModel = new Material { ID = 0, Level = 0, ProduceTypeId = 0 };
                    return View(materialModel);
                }
                var authModel = new AuthorizationModel { RedirectController = "Material", RedirectAction = "AuthMaterialType", OpenId = model.openid };
                return View("~/Views/Authorization/Authorization.cshtml", authModel);
            }
            catch (Exception e) {
                ViewData["e"] = e.Message;
                return View("~/Views/material/Index.cshtml");
            }
          
        }

        public ViewResult AuthMaterialType(string openId)
        {
            ViewData["openId"] = openId;
            var materialModel = new Material {ID = 0, Level = 0, ProduceTypeId = 0};
            return View("~/Views/Material/MaterialType.cshtml", materialModel);
        }

        public ViewResult NextMaterial(string openId, int ID, int Level, int ProduceTypeId)
        {
            ViewData["openId"] = openId;
            var materialModel = new Material {ID = ID, Level = Level, ProduceTypeId = ProduceTypeId };
            return View("~/Views/Material/MaterialType.cshtml", materialModel);
        }

        public ViewResult SettlementSheet(string openid, string interid) {
            ViewData["openid"] = openid;
            ViewData["interid"] = interid;
            ViewData["title"] = "结算单";
            return View();
        }
    }
}