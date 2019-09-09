using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class BaseController : Controller
    {
        internal static string BaseUrl = "https://api.gopas.com.cn/api/";

        public SenparcWeixinSetting WeixinSetting;
        public ILog Log;
        public BaseController(IOptions<SenparcWeixinSetting> weixinSettingOptions)
        {
            WeixinSetting = weixinSettingOptions.Value;
            Log = LogManager.GetLogger(Startup.repository.Name, "");
        }
    }
}
