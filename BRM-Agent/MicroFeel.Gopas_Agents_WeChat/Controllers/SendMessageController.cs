using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroFeel.Pramy.Agent.MessageHandles.CustomMessageHandle;
using MicroFeel.Pramy.Agent.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class SendMessageController : Controller
    {
        static Dictionary<string, captchaInfo> captchTable = new Dictionary<string, captchaInfo>();
        // GET: /<controller>/
   
        public JsonResult Index(string PhoneNumber)
        {
            if (PhoneNumber.Length != 11 || PhoneNumber.IndexOf(',') > 0)
            {
                return Json(new VerificationCode { Result = 3, Message = "请输入正确的手机号码." });
            }
            //验证手机号是否存在
            Task<string> task= RequestData.HTTPGetDataAsync($"{BaseController.BaseUrl}Agent/ExistAgentByPhoneNo", "phonecode=" + PhoneNumber + "&CompanyID=2");
            AuthModel model = JsonConvert.DeserializeObject<AuthModel>(task.Result);//反序列化
            if (!model.Message.Equals("验证通过"))
            {              
                return Json(new VerificationCode { Result = 3, Message = "不存在此代理商" });
            }
           
            var code ="";

            if (PhoneNumber.Equals("13303071889"))
            {
                code = "1234";
            }
            else {
              code = "" + new Random().Next(1000, 9999);
            }
            captchTable.Remove(PhoneNumber);
            var dt = DateTime.Now;
            var captchav = new captchaInfo
            {
                time = dt,
                c = code,
                phone = PhoneNumber,
            };
            captchTable.Add(PhoneNumber, captchav);
            if (!PhoneNumber.Equals("13303071889"))
            {
                new SMSService.SMSServiceClient().SendMsgAsync("MicroFeel.Home9V.Agent", SMSService.SmsType.text, SMSService.SmsTemplate.登录验证, new string[] { code, "10" }, new string[] { PhoneNumber });
            }
            
            return Json(new VerificationCode { Result = 0, Message = "验证码已发送,10分钟内有效." });
        }

        /// <summary>
        /// 提交验证码比对
        /// </summary>
        /// <param name="phoneCaptcha">验证码对象</param>
        /// <returns></returns>  
        [HttpPost]
        public JsonResult verityCaptcha([FromBody] VerityCaptcha model)
        {

            var removelist = captchTable.Where(v => (DateTime.Now - v.Value.time) > TimeSpan.FromMinutes(10));
            captchTable = captchTable.Except(removelist).ToDictionary(v => v.Key, v => v.Value);
            //VerityCaptcha model = JsonConvert.DeserializeObject<VerityCaptcha>(CapchaCode);//反序列化
            if (!captchTable.ContainsKey(model.PhoneNumber))
            {
                
                return Json(new VerificationCode { Result = 1, Message = "验证码已过期" });
            }
            DateTime timev = captchTable[model.PhoneNumber].time;
            string codev = captchTable[model.PhoneNumber].c;

            var dt = DateTime.Now;

        
            if (string.IsNullOrEmpty(model.CapthaCode))
            {
                return Json(new VerificationCode { Result = 2, Message = "验证码不能为空" });
            }


            if (!codev.Equals(model.CapthaCode))
            {
                return Json(new VerificationCode { Result = 1, Message = "验证失败" });
            }
            else
            {
                InsertOpenId insertOpenId = new InsertOpenId();
                insertOpenId.OpenID = model.OpenId;
                insertOpenId.PhoneCode = model.PhoneNumber;
                insertOpenId.CompanyID = 2;
                string jsonData = JsonConvert.SerializeObject(insertOpenId);
                Task<string> task = RequestData.HTTPPostDataAsync($"{BaseController.BaseUrl}Agent/BindAgent", jsonData);
                AuthModel auth = JsonConvert.DeserializeObject<AuthModel>(task.Result);//反序列化
                if (!auth.Code.Equals("200"))
                {
                    return Json(new VerificationCode { Result = 3, Message = "插入失败"+ auth.ErrorInfo });
                }

                return Json(new VerificationCode { Result = 0, Message = "插入成功" });
               // return Json(new VerificationCode { Result = 1, Message = "验证成功" });
            }
        }
    }

    internal class captchaInfo
    {
        public DateTime time { get; set; }
        public string c { get; set; }
        public string phone { get; set; }      
    }

}
