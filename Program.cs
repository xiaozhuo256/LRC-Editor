using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
class Program
{
    public static string targetPath = string.Empty;
    //public static int 用户进度;

    [STAThread]
    static void Main()
    {
        #region 获取目标文件夹

        Console.OutputEncoding = System.Text.Encoding.Unicode;

        //此处需做非空判断，原代码未设计合理异常处理逻辑
        bool selectionVaild = false;

        /* 根据设计交互逻辑，此处用户只需输入一个数值，故应只读一个字节而非读取整行，导致用户操作冗余
           根据ASCII码表，1、2、3分别对应49~51*/
        int userInput;

        while (!selectionVaild)
        {
            //\r\n转义符冗余
            Console.WriteLine("请选择需要转换的文件夹：\n[1] 程序所在的文件夹\n[2] 同目录Lyrics文件夹\n[3] 其他文件夹\n");
            Console.Write("请选择：_\b");

            userInput = Console.Read(); 
            //此处应用Switch开关代Else If循环
            switch(userInput)
            {
                case 49: //读取程序所在文件夹
                    targetPath = Environment.CurrentDirectory;
                    selectionVaild = true;
                    break;
                case 50: //读取LyRiCs文件夹
                    targetPath = Environment.CurrentDirectory + "\\Lyrics";
                    selectionVaild = true;
                    break;
                case 51: //读取其他文件夹
                    CommonOpenFileDialog dialog = new()
                    {
                        IsFolderPicker = true,
                        RestoreDirectory = true,
                        Title = "请选择文件夹路径"
                    };
                    //判空逻辑
                    while (dialog.ShowDialog() != CommonFileDialogResult.Ok || dialog.FileName == null)
                        MessageBox.Show("出现错误，请重试", "重试", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    targetPath = dialog.FileName;
                    selectionVaild = true;
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("必须在选项1~3中选择一个值! 重新输入...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(500);
                    break;
            }
        }

        /*
        用户输入1 = Console.ReadLine();
        if (用户输入1 == "1")
        {
            用户输入_文件夹地址in = AppDomain.CurrentDomain.BaseDirectory;
            用户进度 = 1;
        }
        else if (用户输入1 == "2")
        {
            用户输入_文件夹地址in = AppDomain.CurrentDomain.BaseDirectory+"lrcin";
            用户进度 = 1;
        }
        else if (用户输入1 == "3")
        {
            Console.WriteLine("请输入要打开的文件夹路径");
            用户输入_文件夹地址in = Console.ReadLine();
            用户进度 = 1;
        }
        else
        {
            输出.输出错误信息("请输入一个有效值！");
        }
        文件交互.打开文件夹();

        if (用户进度 == 2)
        {
            Console.WriteLine("请输入要进行的操作：\r\n[1]将中外歌词分割");
        }
        */

        #endregion

        #region 处理文件

        DirectoryInfo targetFolderInfo = new(targetPath);
        List<FileInfo> LyricsFiles = new();

        foreach(FileInfo info in targetFolderInfo.GetFiles())
        {
            if(info.Extension.Equals(".lrc"))
                LyricsFiles.Add(info);
        }

        //处理.lrc文件
        foreach(FileInfo info in LyricsFiles)
            File.WriteAllLines(info.FullName, LyricsWrap.ProcessLyrics(info));

        #endregion

        //Console.WriteLine("程序已执行完毕，请输入任意内容退出。");
        //Console.WriteLine(语言.判断语言("愛-same-CRIER"));
        //Console.ReadLine();

        Console.WriteLine("转换完成，按任意键退出...");
        Console.ReadKey(); //按任意键退出应使用ReadKey方法实现
    }

}