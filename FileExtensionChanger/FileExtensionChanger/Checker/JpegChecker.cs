using System;
using System.Collections.Generic;
using System.Text;

namespace FileExtensionChanger.Checker
{
    public class JpegChecker
    {
        public static bool Check(Span<byte> buffer)
        {
            if (buffer.Length < 10)
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
                if (buffer[6] != 0x4A ||
                    buffer[7] != 0x46 ||
                    buffer[8] != 0x49 ||
                    buffer[9] != 0x46 ||
                    buffer[10] != 0x00)
                {
                    return false;
                }
                return true;
            }
            // 不明
            return false;
        }
    }
}
