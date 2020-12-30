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
        public static void SetFile(string localPath, string data) { File.WriteAllText(FormatPath(localPath), data); }
        public static string[] GetFilesInDirectory(string localPath) { return Directory.GetFiles(FormatPath(localPath)); }
        public static string[] GetLines(string localPath) { return File.ReadAllLines(FormatPath(localPath)); }
        public static void SetLines(string localPath, string[] data) { File.WriteAllLines(FormatPath(localPath), data); }
        public static void AddLine(string localPath, string line)
        {
            string text = GetFile(localPath);
            text += line + "\n";
            File.WriteAllText(FormatPath(localPath), text);
        }
    }
}
