using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroFeel.Pramy.Agent.CommonServices;
using MicroFeel.Pramy.Agent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroFeel.Pramy.Agent.Controllers
{
   
    public class MessageTempController : BaseController
    {
        public MessageTempController(IOptions<SenparcWeixinSetting> weixinSetting)
        :base(weixinSetting)
        {
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            
            return View();
            
        }

        public IActionResult GetMessage() {
        
            return View();
        }
        [HttpPost]
        //[ActionName("ReceiveMessage")] [FromBody] 
        public IActionResult ReceiveMessage([FromBody] TextModel model) {


            //TextModel model = JsonConvert.DeserializeObject<TextModel>(message);//反序列化
            var templateData = new WeixinTemplate_PaySuccess(
                "https://brmagent.gopas.com.cn/order/OrderDetail?finterid=" + model.finterid + "&FBillNo=" +
                model.array[0] + "&State=新订单&FDate=" + DateTime.Now,
                "7QwjDkatTivuRTB9SIEYX8kEu-cUWhXD9jiBFAMWaFM", "新订单")
            {
                keyword1 = new TemplateDataItem(model.array[0]), keyword2 = new TemplateDataItem(model.array[1])
            };


            try
            {
                foreach (var t in model.openIds)
                {
                    Send(templateData, t);
                }
            }
            catch (Exception e) {
                return Content(e.ToString());
            };
            return Content("https://brmagent.gopas.com.cn/order/OrderDetail?finterid=" + model.finterid + "&FBillNo" + model.array[0] + "&State=新订单&FDate=" + DateTime.Now);
        }

        [HttpPost]
        [ActionName("BillOfSettlement")]
        public IActionResult BillOfSettlement([FromBody] TextModel model)
        {
            var results = new List<SendTemplateMessageResult>();
            try
            {
                foreach (var t in model.openIds)
                {
                    var templateData = new WeixinTemplate_PaySuccess(
                       model.sheeturl,
                        "TibdpHeQ2tRibjn6smyVQsTeGhabrb-uHUDY_3sGRZI", "结算单提醒")
                    {
                        first = new TemplateDataItem(model.array[0]),
                        keyword1 = new TemplateDataItem(model.array[1]),
                        keyword2 = new TemplateDataItem(model.array[2]),
                        keyword3 = new TemplateDataItem(model.array[3]),
                        keyword4 = new TemplateDataItem(model.array[4]),
                        remark = new TemplateDataItem(model.array[5]),
                    };
                    var result = Send(templateData, t);
                    if (result.Result.ErrorCodeValue != 0)
                    {
                        results.Add(result.Result);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            };
            return Ok(new {code = results.Count == 0 ? 0 : 1, errorList = results});
        }

        private Task<SendTemplateMessageResult> Send(WeixinTemplate_PaySuccess templateData, string t) => TemplateApi.SendTemplateMessageAsync(WeixinSetting.WeixinAppId, t, templateData);
    }
}
