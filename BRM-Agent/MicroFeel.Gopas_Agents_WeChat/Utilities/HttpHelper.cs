using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace 美雀同步DingUserDept.Utils
{
    public static class HttpHelper
    {
        /// <summary>  
        /// Http同步Get同步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="encode">编码(默认UTF8)</param>  
        /// <returns></returns>  
        public static string HttpGetRequest(string url, Encoding encode = null)
        {
            string result;

            try
            {
                var webClient = new WebClient { Encoding = Encoding.UTF8 };

                if (encode != null)
                    webClient.Encoding = encode;

                result = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        
        public static async Task<string> HTTPPostDataAsync(string url, string data)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "Post";
            request.ContentType = "application/json; charset=utf-8";
            try
            {
                using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
                {
                    streamWriter.Write(data);
                    streamWriter.Flush();
                }

                using (var task = await request.GetResponseAsync() as HttpWebResponse)
                using (var sr = new StreamReader(task.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                return "error:" + exp.Message;
            }
        }

        public static string ObjectListToJson<T>(IList<T> objectList, string className)
        {
            var sbResult = new StringBuilder();
            sbResult.Append("{");
            className = string.IsNullOrEmpty(className) ? objectList[0].GetType().Name : className;
            sbResult.Append("\"" + className + "\":[");

            for (var i = 0; i < objectList.Count; i++)
            {
                var item = objectList[i];
                if (i > 0)
                {
                    sbResult.Append(",");
                }
                sbResult.Append(ObjectToJson(item));
            }

            sbResult.Append("]}");
            return sbResult.ToString();
        }
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                var sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }
    }
}