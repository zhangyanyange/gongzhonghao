using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using MicroFeel.Pramy.Agent.Models.Template;
using MicroFeel.Pramy.Agent.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class TemplateMessageController : BaseController
    {
        public TemplateMessageController(IOptions<SenparcWeixinSetting> wxSettingOptions)
            : base(wxSettingOptions)
        {
        }

        [HttpGet]
        [ActionName("Templates")]
        public async Task<JsonResult> Templates()
        {
            var token = AccessTokenContainer.GetAccessToken(WeixinSetting.WeixinAppId);
            var url = $"https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token={token}";
            var task = await url.GetStringAsync();
            var template = JsonConvert.DeserializeObject<Models.Template.Template>(task);
            return Json(template);
        }

        [HttpPost]
        public async Task<List<SendTemplateMessageResult>> Index([FromBody]TemplateMessage templateMessage)
        {
            var template = new WeixinTemplate(templateMessage.Url, templateMessage.TemplateId,
                "");
            var count = templateMessage.Contents.Count;
            template.first = !string.IsNullOrEmpty(templateMessage.First)
                ? new TemplateDataItem(templateMessage.First)
                : null;
            template.remark = !string.IsNullOrEmpty(templateMessage.Remark)
                ? new TemplateDataItem(templateMessage.Remark)
                : null;
            //反射赋值 WeixinTemplate 的属性，如：keyword1 等。
            for (var i = 0; i < count; i++)
            {
                var property = template.GetType().GetProperty($"keyword{i + 1}");
                property.SetValue(template, new TemplateDataItem(templateMessage.Contents[i]));
            }

            try
            {
                var taskList = new List<Task<SendTemplateMessageResult>>();
                foreach (var openId in templateMessage.OpenIds)
                {
                    var task =  TemplateApi.SendTemplateMessageAsync(WeixinSetting.WeixinAppId, openId, template);
                    taskList.Add(task);
                }

                var results = await Task.WhenAll(taskList);
                //获取发送模板失败的用户List
                return results.Where(sendResult => sendResult.ErrorCodeValue != 0).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
            
        }
    }
}
