using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace MusicEffectControl.ConvertManage
{
    public class ConvertHelper
    {
        public static byte[] StreamToByte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string FileToBase64String(string FilePath)
        {
            FileStream fsForRead = new FileStream(FilePath, FileMode.Open);//文件路径
            string base64Str = "";
            try
            {
                //读写指针移到距开头10个字节处
                fsForRead.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[fsForRead.Length];
                int log = Convert.ToInt32(fsForRead.Length);
                //从文件中读取10个字节放到数组bs中
                fsForRead.Read(bs, 0, log);
                base64Str = Convert.ToBase64String(bs);
                return base64Str;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
                return base64Str;
            }
            finally
            {
                fsForRead.Close();
            }
        }
        public static bool Base64StringToFile(string base64String, string TargetPath,string TargetName)
        {
            bool opResult = false;
            try
            {
                if (!Directory.Exists(TargetPath))
                {
                    Directory.CreateDirectory(TargetPath);
                }

                string strbase64 = base64String.Trim().Substring(base64String.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                FileStream fs = new FileStream(TargetPath + "\\" + TargetName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                fs.Write(b, 0, b.Length);
                fs.Close();

                opResult = true;
            }
            catch (Exception e)
            {
              
            }
            return opResult;
        }



        public static byte[] Base64ToBytes(string ByteStr)
        {
            byte[] ImageBytes = Convert.FromBase64String(ByteStr);
            MemoryStream Memory = new MemoryStream(ImageBytes, 0, ImageBytes.Length);
            Memory.Write(ImageBytes, 0, ImageBytes.Length);

            return ImageBytes;
        }
        public static string UrlEnCode(string Msg)
        {
            return HttpUtility.UrlEncode(Msg);
        }
        public static string UrlDeCode(string Msg)
        {
            return HttpUtility.UrlDecode(Msg);
        }
        public static double GetRate(double A, double B)
        {
            double Value = (A / B);
            var T1 = Math.Round(Value, 2); 
            return  T1 * 100; 
        }
        public static double RoundDouble(double v, int x)
        {
            return ChinaRound(v, x);
        }
        public static double ChinaRound(double value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }
       

      
      
        public static int MorningOrNoon(DateTime SetTime)
        {
            var GetTime = SetTime;
            if (GetTime.Hour > 10)
            {
                if (GetTime.Hour <= 11)
                {
                    return 1;
                }
                else
                if (GetTime.Hour <= 16)
                {
                    return 2;
                }
                else
                if (GetTime.Hour <= 20)
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
       
        public static string StringDivision(string Message, string Left, string Right)
        {
            if (Message.Contains(Left) && Message.Contains(Right))
            {
                string GetLeftString = Message.Substring(Message.IndexOf(Left) + Left.Length);
                string GetRightString = GetLeftString.Substring(0, GetLeftString.IndexOf(Right));
                return GetRightString;
            }
            else
            {
                return string.Empty;
            }
        }
        public static string GetStringNoEmp(string Message)
        {
            return Message.Replace(" ", "").Replace("    ", "").Replace("　", "");
        }

        public static object ObjToDateTime(object Any)
        {
            if (Any != null)
            {
                DateTime SetTime = new DateTime();
                if (DateTime.TryParse(ConvertHelper.ObjToStr(Any),out SetTime))
                {
                    return SetTime;
                }
            }
            return null;
        }

        public static string ObjToStr(object Item)
        {
            string GetConvertStr = string.Empty;
            if (Item == null == false)
            {
                GetConvertStr = Item.ToString();
            }
            return GetConvertStr;
        }
        public static int ObjToInt(object Item)
        {
            int Number = -1;
            if (Item == null == false)
            {
                int.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }
        public static int ObjToIntR(object Item)
        {
            int Number = 0;
            if (Item == null == false)
            {
                int.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }
        public static double ObjToDouble(object Item)
        {
            double Number = -1;
            if (Item == null == false)
            {
                double.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }
        public static bool ObjToBool(object Item)
        {
            bool Check = false;
            if (Item == null == false)
            {
                Boolean.TryParse(Item.ToString(), out Check);
            }
            return Check;
        }

        public static long ObjToLong(object Item)
        {
            long Number = 0;
            if (Item == null == false)
            {
                long.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }


    }
}
