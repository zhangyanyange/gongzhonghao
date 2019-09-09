//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace SMSService
{
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SmsType", Namespace="http://schemas.datacontract.org/2004/07/MicroFeel.SMS")]
    public enum SmsType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        text = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        voice = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SmsTemplate", Namespace="http://schemas.datacontract.org/2004/07/MicroFeel.SMS")]
    public enum SmsTemplate : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        登录验证 = 7290,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        登录语音验证 = 7291,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        运费提示1 = 10893,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        运费提示2 = 10895,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        自取业务提示 = 10896,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        客服业务通知 = 65313,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SMSService.ISMSService")]
    public interface ISMSService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMSService/SendMsg", ReplyAction="http://tempuri.org/ISMSService/SendMsgResponse")]
        System.Threading.Tasks.Task SendMsgAsync(string ApplicationName, SMSService.SmsType smsType, SMSService.SmsTemplate smsTemplate, string[] paramContent, string[] phoneNumbers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMSService/SendMsgWF", ReplyAction="http://tempuri.org/ISMSService/SendMsgWFResponse")]
        System.Threading.Tasks.Task SendMsgWFAsync(string ApplicationName, int smsType, int smsTemplate, string[] paramContent, string[] phoneNumbers);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface ISMSServiceChannel : SMSService.ISMSService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class SMSServiceClient : System.ServiceModel.ClientBase<SMSService.ISMSService>, SMSService.ISMSService
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SMSServiceClient() : 
                base(SMSServiceClient.GetDefaultBinding(), SMSServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ISMSService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMSServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(SMSServiceClient.GetBindingForEndpoint(endpointConfiguration), SMSServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMSServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SMSServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMSServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SMSServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task SendMsgAsync(string ApplicationName, SMSService.SmsType smsType, SMSService.SmsTemplate smsTemplate, string[] paramContent, string[] phoneNumbers)
        {
            return base.Channel.SendMsgAsync(ApplicationName, smsType, smsTemplate, paramContent, phoneNumbers);
        }
        
        public System.Threading.Tasks.Task SendMsgWFAsync(string ApplicationName, int smsType, int smsTemplate, string[] paramContent, string[] phoneNumbers)
        {
            return base.Channel.SendMsgWFAsync(ApplicationName, smsType, smsTemplate, paramContent, phoneNumbers);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISMSService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISMSService))
            {
                return new System.ServiceModel.EndpointAddress("http://123.206.107.160/SMSService/SMSService.svc");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return SMSServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ISMSService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return SMSServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ISMSService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ISMSService,
        }
    }
}
