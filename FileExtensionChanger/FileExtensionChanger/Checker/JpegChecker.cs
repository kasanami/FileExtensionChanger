using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileExtensionChanger.Checker
{
    public class JpegChecker
    {
        /// <summary>
        /// ASCII文字で"JFIF"とヌル終端
        /// </summary>
        static readonly byte[] JFIF = Encoding.ASCII.GetBytes("JFIF\0");
        /// <summary>
        /// ASCII文字で"ICC_PROFILE"とヌル終端
        /// </summary>
        static readonly byte[] ICC_PROFILE = Encoding.ASCII.GetBytes("ICC_PROFILE\0");
        /// <summary>
        /// JPEGファイルならtrueを返す。
        /// </summary>
        /// <param name="buffer">チェックするバッファ</param>
        public static bool Check(Span<byte> buffer)
        {
            if (buffer.Length < 4)
            {
                return false;
            }
            // SOI スタートマーカ(Start of Image) 0xFFD8
            // に加え、次にマーカー(0xFF) があればjpegと判断する。
            return buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF;
        }
    }
}
