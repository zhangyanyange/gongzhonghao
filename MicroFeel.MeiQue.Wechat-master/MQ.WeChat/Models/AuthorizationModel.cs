namespace MQ.WeChat.Models
{
    public class AuthorizationModel
    {
        public string OpenId { get; set; }
        //命名不能是 Controller 和 Action，否则重定向的 Controller 接收错误。
        public string RedirectController { get; set; }
        public string RedirectAction { get; set; }
    }
}
