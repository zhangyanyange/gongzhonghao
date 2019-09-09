using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQ.WeChat.Models.Ticket;
using Flurl.Http;
using Microsoft.Extensions.Options;
using MQ.WeChat.Config;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Helpers;

namespace MQ.WeChat.Controllers
{
    public class TicketController : BaseController
    {

        public TicketController(IOptions<SenparcWeixinSetting> wxSettingOptions, IOptions<BaseConfig> setting)
        :base(wxSettingOptions, setting)
        {}

        internal static Ticket GetTicket(string url)
        {
            var ticket = JsApiTicketContainer.GetJsApiTicket(WeixinSetting.WeixinAppId);
            var timestamp = JSSDKHelper.GetTimestamp();
            var nonceStr = JSSDKHelper.GetNoncestr();
            var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, url);
            
            var model = new Ticket
            {
                ticket = ticket,
                nonceStr = nonceStr,
                timestamp = timestamp,
                signature = signature
            };
            return model;
        }

        public Ticket Index(string url)
        {
            return GetTicket(url);
        }
    }
}