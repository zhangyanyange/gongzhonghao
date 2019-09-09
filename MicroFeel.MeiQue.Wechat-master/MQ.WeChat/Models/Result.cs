using System.Collections.Generic;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace MQ.WeChat.Models
{
    public class Result
    {
        public virtual int Status { get; set; } = 0;
        public List<SendTemplateMessageResult> Lists { get; set; }
    }
}
