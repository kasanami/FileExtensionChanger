using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileExtensionChanger.Checker
{
    public class JpegChecker
    {
        static readonly byte[] JFIF = Encoding.UTF8.GetBytes("JFIF\0");
        static readonly byte[] ICC_PROFILE = Encoding.UTF8.GetBytes("ICC_PROFILE\0");
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
            if (buffer[0] != 0xFF || buffer[1] != 0xD8)
            {
                return false;
            }
            // JFIFフォーマット APP0 (0xFFE0)
            if (buffer[2] == 0xFF && buffer[3] == 0xE0)
            {
                // ASCII文字で"JFIF"とヌル終端
                var slicedBuffer = buffer.Slice(6, JFIF.Length);
                return slicedBuffer.SequenceEqual(JFIF);
            }
            // カラープロファイル APP2 (0xFFE2)
            if (buffer[2] == 0xFF && buffer[3] == 0xE2)
            {
                // ASCII文字で"ICC_PROFILE"とヌル終端
                var slicedBuffer = buffer.Slice(6, ICC_PROFILE.Length);
                return slicedBuffer.SequenceEqual(ICC_PROFILE);
            }
            // JPEGファイルじゃないor未対応のJPEGファイル
            return false;
        }
    }
}
