using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;
using MicroFeel.Pramy.Agent.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;

namespace MicroFeel.Pramy.Agent
{
    public class Startup
    {
        public static ILoggerRepository repository { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("NETCoreRepository");
            //指定配置文件
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //添加Senparc.Weixin配置文件（内容可以根据需要对应修改）
            services.Configure<SenparcWeixinSetting>(Configuration.GetSection("SenparcWeixinSetting"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"

                    );
            });
            #region 微信相关
            ////注册微信
            AccessTokenContainer.Register(senparcWeixinSetting.Value.WeixinAppId, senparcWeixinSetting.Value.WeixinAppSecret);

            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting.Value;

            //提供网站根目录
            if (env.ContentRootPath != null)
            {
                Senparc.Weixin.Config.RootDictionaryPath = env.ContentRootPath;
                Server.AppDomainAppPath = env.ContentRootPath;// env.ContentRootPath;
            }
            Server.WebRootPath = env.WebRootPath;// env.ContentRootPath;



            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            ConfigWeixinTraceLog();         //配置微信跟踪日志（按需）
            RegisterWeixinThreads();        //激活微信缓存及队列线程（必须）
            RegisterSenparcWeixin();        //注册Demo所用微信公众号的账号信息（按需）

            /* 微信配置结束 */

            #endregion
        }

        private void ConfigWeixinTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.WeixinTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //自定义日志记录回调
            Senparc.Weixin.WeixinTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };
        }

        /// <summary>
        /// 激活微信缓存
        /// </summary>
        private void RegisterWeixinThreads()
        {
            ThreadUtility.Register();//如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
        }

        /// <summary>
        /// 注册Demo所用微信公众号的账号信息
        /// </summary>
        private void RegisterSenparcWeixin()
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //注册公众号
            AccessTokenContainer.Register(
                senparcWeixinSetting.WeixinAppId,
                senparcWeixinSetting.WeixinAppSecret,
                "xx公众号");

            //注册小程序（完美兼容）
            AccessTokenContainer.Register(
                senparcWeixinSetting.WxOpenAppId,
                senparcWeixinSetting.WxOpenAppSecret,
                "xx小程序");
        }


    }
}
