using FileExtensionChanger.Checker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileExtensionChanger
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("パスが指定されていません。");
                return;
            }
            var sourcePath = args[0];
            var files = new List<string>();
            if (File.Exists(sourcePath))
            {
                files.Add(sourcePath);
            }
            else if (Directory.Exists(sourcePath))
            {
                files.AddRange(Directory.GetFiles(sourcePath));
            }
            if (files.Count == 0)
            {
                Console.WriteLine("パスのファイル/フォルダが見つかりません。");
                return;
            }
            try
            {
                foreach (var file in files)
                {
                    Console.Write($"{file}");
                    try
                    {
                        // ファイルタイプ判定
                        var fileType = GetFileType(file);
                        if (fileType != FileType.Unknown)
                        {
                            // 拡張子変更
                            var newFile = ChangeExtension(file, fileType);
                            if (file != newFile)
                            {
                                File.Move(file, newFile);
                                Console.WriteLine("変更成功");
                            }
                            else
                            {
                                Console.WriteLine("変更なし");
                            }
                        }
                        else
                        {
                            Console.WriteLine("非対応");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                Console.WriteLine("終了");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 指定パスのファイルを読み込みファイルタイプを判定する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static FileType GetFileType(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                // TODO:とりあえず32バイト
                var buffer = new byte[32];
                var length = stream.Read(buffer);
                if (length == 0)
                {
                    return FileType.Unknown;
                }
                if (JpegChecker.Check(buffer.AsSpan(0, length)))
                {
                    return FileType.Jpeg;
                }
            }
            return FileType.Unknown;
        }
        /// <summary>
        /// 各ファイルタイプの拡張子
        /// </summary>
        static Dictionary<FileType, string> Extensions = new Dictionary<FileType, string>()
        {
            { FileType.Jpeg,"jpg" }
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string ChangeExtension(string path, FileType fileType)
        {
            var newPath = new StringBuilder();
            newPath.Append(Path.GetDirectoryName(path));
            if (newPath.Length > 0)
            {
                newPath.Append(@"\");
            }
            newPath.Append(Path.GetFileNameWithoutExtension(path) + "." + Extensions[fileType]);
            return newPath.ToString();
        }
    }
}
