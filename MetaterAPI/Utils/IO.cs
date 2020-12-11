using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MetaterAPI.Utils
{
    public static class IO
    {
        public static string FormatPath(string localPath) { return Directory.GetCurrentDirectory() + @"\" + localPath; }
        public static string GetFile(string localPath) { return File.ReadAllText(FormatPath(localPath)); }
        public static string[] GetFilesInDirectory(string localPath) { return Directory.GetFiles(FormatPath(localPath)); }
        public static string[] GetLines(string localPath) { return File.ReadAllLines(FormatPath(localPath)); }
        public static void AddLine(string localPath, string line)
        {
            string text = GetFile(localPath);
            text += line + "\n";
            File.WriteAllText(FormatPath(localPath), text);
        }
    }
}
