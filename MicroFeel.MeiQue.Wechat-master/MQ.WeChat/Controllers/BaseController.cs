using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MQ.WeChat.Config;
using Senparc.Weixin.Entities;

namespace MQ.WeChat.Controllers
{
    public class BaseController : Controller
    {
        protected IMemoryCache Cache;
        public static SenparcWeixinSetting WeixinSetting;
        protected BaseConfig Setting;

        public BaseController(IOptions<SenparcWeixinSetting> weixinSettingOptions, IOptions<BaseConfig> setting, IMemoryCache memoryCache)
        {
            Cache = memoryCache;
            Setting = setting.Value;
            WeixinSetting = weixinSettingOptions.Value;
        }

        public BaseController(IOptions<SenparcWeixinSetting> weixinSettingOptions, IOptions<BaseConfig> setting)
            :this(weixinSettingOptions, setting, null)
        {

        }
    }
}