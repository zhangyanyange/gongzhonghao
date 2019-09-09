using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MicroFeel.Pramy.Agent.MessageHandles.CustomMessageHandle
{
    public static class RequestData
    {
            public static async Task<string> HTTPPostDataAsync(string url, string data)
            {
                var request = HttpWebRequest.Create(url) as HttpWebRequest;
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

        internal static void HTTPGetzDataAsync()
        {
            throw new NotImplementedException();
        }

        public static string HTTPPostData(string url, string data)
            {
                var request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                        streamWriter.Flush();
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (Exception exp)
                {
                    return "error:" + exp.Message;
                }
            }
        public static async Task<string> HTTPGetDataAsync(string url, string data)
        {
            var request = HttpWebRequest.Create(url + "?" + data) as HttpWebRequest;
            request.Method = "Get";

            using (var task = await request.GetResponseAsync() as HttpWebResponse)
            using (var sr = new StreamReader(task.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        public static string HTTPGetData(string url, string data)
        {
            var request = HttpWebRequest.Create(url + "?" + data) as HttpWebRequest;
            request.Method = "Get";

            using (var response = request.GetResponse() as HttpWebResponse)
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        public static string ToJsonDateTime(this DateTime dt)
        {
            return (dt - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("0");
        }

        public static DateTime ParseDateTime(this double dtvalue)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(dtvalue);
        }
        public static DateTime ParseDateTime(this string dtvalueString)
        {
            var str = dtvalueString.Substring(6, dtvalueString.Length - 8);

            return ParseDateTime(double.Parse(str));
        }
    }
}
