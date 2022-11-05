using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GMS
{
    class MD5Helper
    {
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            //加密
            //字符串转字节数组
            byte[] buffer = Encoding.Default.GetBytes(str); 
            //返回一个加密好的字节数组
            byte[] MD5Buffer = md5.ComputeHash(buffer);
            //字节数组转为字符串
            //return Encoding.Default.GetString(MD5Buffer); //出现了乱码问题
            string strNew = "";
            for (int i = 0; i < MD5Buffer.Length; i++)
            {
                strNew += MD5Buffer[i].ToString("x2");//x——将十进制转换为十六进制，2——每次都是两位数
            }
            return strNew;
        }
    }
}
