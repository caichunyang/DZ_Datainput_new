using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Common
{
    public class DealFile
    {
        //
        /// <summary>
        /// 根据图片路径返回图片的字节流byte[]
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns>返回的字节流</returns>
        public static byte[] getImageByte(string imagePath)
        {
            try
            {
                FileStream files = new FileStream(imagePath, FileMode.Open);
                byte[] imgByte = new byte[files.Length];
                files.Read(imgByte, 0, imgByte.Length);
                files.Close();
                return imgByte;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
