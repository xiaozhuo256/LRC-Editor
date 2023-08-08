using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Forms;

namespace Lyrics_Formatter
{
    class Program
    {
        static void Main()
        {
            string targetPath = string.Empty;

            bool success = false;
            do
            {
                Console.WriteLine("请选择程序将要处理的目标目录：\n\n[1] 程序所在目录\n[2] 其他目录\n");
                Console.Write("请选择：_\b");

                ConsoleKeyInfo userSelection = Console.ReadKey();

                switch (userSelection.KeyChar)
                {
                    case '1':
                        targetPath = Environment.CurrentDirectory;
                        success = true;
                        break;
                    case '2':
                        CommonOpenFileDialog dialog = new()
                        {
                            IsFolderPicker = true,
                            RestoreDirectory = true,
                            EnsurePathExists = true,
                            Title = "请选择文件夹路径"
                        };
                        if (dialog.ShowDialog().Equals(CommonFileDialogResult.Cancel) ||
                            dialog.FileName == null || dialog.FileName == string.Empty)
                            return;
                        targetPath = dialog.FileName;
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("输入的的值无效，请输入\"1\"或\"2\"作为选择\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(1000);
                        success = false;
                        break;
                }
            }
            while(!success);

            List<FileInfo> LyricsFiles = new(),
                           FlacFiles = new();

            foreach(string path in Directory.GetFiles(targetPath))
            {
                FileInfo file = new(path);
                switch(file.Extension)
                {
                    case ".lrc":
                        LyricsFiles.Add(file);
                        break;
                    case ".flac":
                        FlacFiles.Add(file);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}