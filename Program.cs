using System;
using System.Text;
using System.IO;
namespace GenDir
{
    class Program
    {
        static int j = 1;
        static StringBuilder sb = new StringBuilder();
        static void Main(string[] args)
        {
            //测试E:\公司项目
            Console.WriteLine("Please enter a path:");
            string path = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(path))
            {
                GenFileStructure(path, j);
            }
            Console.WriteLine("The file directory is loaded!Press 'Y' to save,press 'N' to exit!");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                File.WriteAllText(Path.Combine(path, DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_文件夹目录结构.txt"), sb.ToString(), Encoding.UTF8);
            }
            else if (key.Key == ConsoleKey.N)
            {
                Environment.Exit(0);
            }
        }

        static void GenFileStructure(string path, int index)
        {
            try
            {
                bool isDirectory = IsDirectory(path);
                if (isDirectory)
                {
                    string[] entries = Directory.GetFileSystemEntries(path);
                    if (entries.Length > 0)
                    {
                        foreach (var v in entries)
                        {
                            Console.WriteLine(string.Format(new string('.', index) + "{0}", Path.GetFileName(v)));
                            sb.Append(string.Format(new string('.', index) + "{0}\r\n", Path.GetFileName(v)));
                            if (IsDirectory(v))
                            {
                                ++j;
                                GenFileStructure(v, j);
                                j--;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(path);
                    sb.Append(path + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool IsDirectory(string path)
        {
            FileAttributes fi = File.GetAttributes(path);
            if (fi==FileAttributes.Directory|| fi == (FileAttributes.ReadOnly|FileAttributes.Directory))
            {
                return true;
            }  
            else
            {
                return false;
            }
        }

    }
}
