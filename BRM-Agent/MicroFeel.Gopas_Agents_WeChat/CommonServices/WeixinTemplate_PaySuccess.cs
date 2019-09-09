using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace MicroFeel.Pramy.Agent.CommonServices
{
    public class WeixinTemplate_PaySuccess : TemplateMessageBase
    {
        public TemplateDataItem first { get; set; }
        public TemplateDataItem keyword1 { get; set; }
        public TemplateDataItem keyword2 { get; set; }
        public TemplateDataItem keyword3 { get; set; }
        public TemplateDataItem keyword4 { get; set; }
        public TemplateDataItem keyword5 { get; set; }

        public TemplateDataItem remark { get; set; }
        public WeixinTemplate_PaySuccess(string url, string TEMPLATE_ID, string TEMPLATE_NAME)
        : base(TEMPLATE_ID, url, TEMPLATE_NAME)
        {

        }
    }
}
