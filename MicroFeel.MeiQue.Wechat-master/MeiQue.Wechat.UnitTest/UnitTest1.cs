using System.Collections.Generic;
using System.Diagnostics;
using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQ.WeChat.Models.TemplateMessage;
using Newtonsoft.Json;
using Senparc.Weixin.Annotations;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace MeiQue.Wechat.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        
        /// <summary>
        /// 预订单模板消息
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
                /*
                 * 商品名称：{{keyword1.DATA}}
                 * 商品价格：{{keyword2.DATA}}
                 * 商品数量：{{keyword3.DATA}}
                 * 下单时间：{{keyword4.DATA}}
                 */
                Contents = new List<string>() { "1号商品", "30", "40", "2018-10-19" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "您收到一份客户预订单",
                Remark = "可能需要提前备货",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "MSaGCBTSTNWzKSn9utOq_5BvnZr9uE5ugxrnhAT7xUU",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }

        /// <summary>
        /// 已付款订单模板消息
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestMethod2()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
            // 订单号：{ { keyword1.DATA} }
            // 订单状态：{ { keyword2.DATA} }
            // 商品名称：{ { keyword3.DATA} }
            Contents = new List<string>() { "43535643656456456", "待处理", "1号商品" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "您收到一份订单",
                Remark = "请您尽快处理！",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "gcu_pgCPfKq51xBhB9oQtkK0KoK-g6aYL6Y5ljlwqRk",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }

        /// <summary>
        /// 送货模板消息
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestMethod3()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
            // 姓名：{ { keyword1.DATA} }
            // 联系方式：{ { keyword2.DATA} }
            // 送货地点：{ { keyword3.DATA} }
            // 商品名称：{ { keyword4.DATA} }
            // 商品数量：{ { keyword5.DATA} }
            Contents = new List<string>() { "张伟", "18817716666", "哈尔滨恒盛豪庭4栋1000", "1号商品", "50" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "亲爱的送货员，以下是订单详情，请及时送达",
                Remark = "如有疑问请拨打电话18817716666",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "WOhVMPZWgLm3awaWiJ0Cv5INuvGf2Baz91Doo5uks1k",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }

        /// <summary>
        /// 安装模板消息
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestMethod4()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
            // 联系人：{ { keyword1.DATA} }
            // 联系电话：{ { keyword2.DATA} }
            // 安装地址：{ { keyword3.DATA} }
            Contents = new List<string>() { "张伟", "18624438893", "哈尔滨恒盛豪庭4栋1000" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "安装提醒",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "YZ5vsFVwOrWIjAb-5q9NeQa842kipYKaCMueg0srQMI",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }
    }
}
