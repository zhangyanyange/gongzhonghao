using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MQ.WeChat.Config;
using MQ.WeChat.Models.TemplateMessage;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using MQ.WeChat.Services;
using Flurl.Http;
using MQ.WeChat.Models;
using MQ.WeChat.Models.TemplateMessageResult;
using Newtonsoft.Json;
using Senparc.Weixin.MP.Containers;

namespace MQ.WeChat.Controllers
{
    public class TemplateMessageController : BaseController
    {
        public TemplateMessageController(IOptions<SenparcWeixinSetting> wxSettingOptions, IOptions<BaseConfig> setting)
            : base(wxSettingOptions, setting)
        {
        }

        [HttpGet]
        [ActionName("Template")]
        public async Task<JsonResult> Template()
        {
            var token = AccessTokenContainer.GetAccessToken(WeixinSetting.WeixinAppId);
            var url = $"https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token={token}";
            var task = await url.GetStringAsync();
            var template = JsonConvert.DeserializeObject<Template>(task);
            return Json(template);
        }

        [HttpPost]
        public async Task<Result> Index([FromBody]TemplateMessage templateMessage)
        {
            var template = new WeixinTemplate(templateMessage.Url, templateMessage.TemplateId, "");
            var count = templateMessage.Contents.Count;
            template.first = !string.IsNullOrEmpty(templateMessage.First)
                ? new TemplateDataItem(templateMessage.First)
                : null;
            template.remark = !string.IsNullOrEmpty(templateMessage.Remark)
                ? new TemplateDataItem(templateMessage.Remark)
                : null;
            //利用反射赋值 WeixinTemplate 的 keyword1, keyword2 等属性。
            //有没有更好的写法呢 ??
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
                return new SuccessResult
                {
                    Lists = results.Where(sendResult => sendResult.ErrorCodeValue != 0).ToList()
                };
            }
            catch (Exception)
            {
                return new ErrorResult
                {
                    Lists = new List<SendTemplateMessageResult>()
                };
            }
        }
    }
}
