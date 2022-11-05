using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GMS.util
{
    static class HttpUtil
    {
        public static string OssRootUrl = "http://7.zhangjian.link/";
        public static string GetMacByNetworkInterface()
        {
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    return BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                }
            }
            catch (Exception)
            {
            }
            return "00-00-00-00-00-00";
        }
        /**根据经纬度查询地理位置接口示例
         * http://restapi.amap.com/v3/geocode/regeo?location=119.785925777778,33.4773057777778&key=17479d86c0c6a0305024e1142351a0a4
         * */
        public static string HttpGetNew(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public static string HttpPostNew(string Url, string postDataStr)
        {
            byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(postDataStr);
            HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;//(HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = postBytes.Length;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(postBytes, 0, postBytes.Length);
            myRequestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public static async Task<HttpResult> UploadPicture(string filePath,string key)
        {
            if (filePath.IsNullOrEmpty() || key.IsNullOrEmpty() || key.IsNullOrWhiteSpace())
            {
                return null;
            }
            string AccessKey = "mjd6uCjQwnEAJIuBO7SVEZmgKc9oIwS3qHGWJx_O";
            string SecretKey = "f7N8e_NqIWD32WZiq1tHz64amL60WbK28Ikl1nK8";
            Mac mac = new Mac(AccessKey, SecretKey);
            // 上传文件名
            //string key = ""; 
            //"dk/" + Status.stuId + "/" + DateTime.Now.ToString("yyyy-MM-dd/HH-mm-ss") + ".jpg";
            //Status.picUrl = key;
            // 存储空间名
            string Bucket = "z1334";
            // 设置上传策略
            PutPolicy putPolicy = new PutPolicy();
            // 设置要上传的目标空间
            putPolicy.Scope = Bucket;
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(3600);
            // 文件上传完毕后，在多少天后自动被删除
            // putPolicy.DeleteAfterDays = 1;
            // 生成上传token
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config();
            // 设置上传区域
            config.Zone = Zone.ZONE_CN_East;
            // 设置 http 或者 https 上传
            config.UseHttps = false;
            config.UseCdnDomains = false;
            config.ChunkSize = ChunkUnit.U512K;
            // 表单上传
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadFile(filePath, key, token, null);
            return result;
        }
        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <returns></returns>
        public static string HttpDownload(string url)
        {
            using (var client = new WebClient())
            {
                string tempFile = Path.GetTempFileName();
                client.DownloadFile(url, tempFile);//下载临时文件
                //Console.WriteLine("Using " + tempFile);
                //return FileToStream(tempFile, true);
                return tempFile;
            }
        }
        /// <summary>
        /// 文件转流
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="isDelete">是否删除临时文件</param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName, bool isDelete = false)
        {

            //打开文件

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // 读取文件的 byte[]

            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();

            // 把 byte[] 转换成 Stream

            Stream stream = new MemoryStream(bytes);
            if (isDelete)
            {
                File.Delete(fileName);//删除临时文件
            }
            return stream;
        }
    }
}
