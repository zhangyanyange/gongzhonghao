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
        /// Ԥ����ģ����Ϣ
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
                /*
                 * ��Ʒ���ƣ�{{keyword1.DATA}}
                 * ��Ʒ�۸�{{keyword2.DATA}}
                 * ��Ʒ������{{keyword3.DATA}}
                 * �µ�ʱ�䣺{{keyword4.DATA}}
                 */
                Contents = new List<string>() { "1����Ʒ", "30", "40", "2018-10-19" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "���յ�һ�ݿͻ�Ԥ����",
                Remark = "������Ҫ��ǰ����",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "MSaGCBTSTNWzKSn9utOq_5BvnZr9uE5ugxrnhAT7xUU",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }

        /// <summary>
        /// �Ѹ����ģ����Ϣ
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestMethod2()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
            // �����ţ�{ { keyword1.DATA} }
            // ����״̬��{ { keyword2.DATA} }
            // ��Ʒ���ƣ�{ { keyword3.DATA} }
            Contents = new List<string>() { "43535643656456456", "������", "1����Ʒ" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "���յ�һ�ݶ���",
                Remark = "�������촦��",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "gcu_pgCPfKq51xBhB9oQtkK0KoK-g6aYL6Y5ljlwqRk",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }

        /// <summary>
        /// �ͻ�ģ����Ϣ
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestMethod3()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
            // ������{ { keyword1.DATA} }
            // ��ϵ��ʽ��{ { keyword2.DATA} }
            // �ͻ��ص㣺{ { keyword3.DATA} }
            // ��Ʒ���ƣ�{ { keyword4.DATA} }
            // ��Ʒ������{ { keyword5.DATA} }
            Contents = new List<string>() { "��ΰ", "18817716666", "��������ʢ��ͥ4��1000", "1����Ʒ", "50" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "�װ����ͻ�Ա�������Ƕ������飬�뼰ʱ�ʹ�",
                Remark = "���������벦��绰18817716666",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "WOhVMPZWgLm3awaWiJ0Cv5INuvGf2Baz91Doo5uks1k",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }

        /// <summary>
        /// ��װģ����Ϣ
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestMethod4()
        {
            var url = $"https://meiquewechat.microfeel.net/templatemessage/index";
            var templateMessage = new TemplateMessage
            {
            // ��ϵ�ˣ�{ { keyword1.DATA} }
            // ��ϵ�绰��{ { keyword2.DATA} }
            // ��װ��ַ��{ { keyword3.DATA} }
            Contents = new List<string>() { "��ΰ", "18624438893", "��������ʢ��ͥ4��1000" },
                OpenIds = new List<string>() { "oOeJg1kP5_HEwTEV3I59jwqa19vI" },
                First = "��װ����",
                Url = "https://meiquewechat.microfeel.net/Order/OrderDetail?billID=323a6836-6e67-4e82-8549-208c4a143a16",
                TemplateId = "YZ5vsFVwOrWIjAb-5q9NeQa842kipYKaCMueg0srQMI",
            };
            var result = url.PostJsonAsync(templateMessage).ReceiveString().Result;
            var list = JsonConvert.DeserializeObject<List<SendTemplateMessageResult>>(result);
            Debug.Assert(list.Count == 0, result);
        }
    }
}
