using System;
namespace MQ.WeChat.Models
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public DateTime ExpDateTime { get; set; }
    }
}
