using System.Text.RegularExpressions;

class 交互菜单
{
    public static string 文件夹;
    static string 用户输入1;
    public static string 用户输入_文件夹地址in;
    static string 用户输入_文件夹地址out;
    public static int 用户进度;
    static void Main(string[] args)
    {
        
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.WriteLine("请选择需要转换的文件夹：\r\n[1]程序所在的文件夹\r\n[2]lrcin文件夹\r\n[3]其他文件夹");

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

        中外歌词分割.flac处理();



        Console.WriteLine("程序已执行完毕，请输入任意内容退出。");
        Console.WriteLine(语言.判断语言("愛-same-CRIER"));
        Console.ReadLine();
    }

}
class 输出
{
    public static void 输出错误信息(string 错误信息)
    {
        if (Regex.IsMatch(错误信息, @"："))
        {
            Console.BackgroundColor = ConsoleColor.Red;//设置背景色
            Console.ForegroundColor = ConsoleColor.White;//设置字体颜色
            Console.Write("【失败】" + 错误信息);
            Console.ResetColor();
            Console.Write("\r\n");
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Red;//设置背景色
            Console.ForegroundColor = ConsoleColor.White;//设置字体颜色
            Console.Write("【失败】错误：" + 错误信息);
            Console.ResetColor();
            Console.Write("\r\n");
        }
        
    }
    public static void 输出正确信息(string 正确信息)
    {
        Console.BackgroundColor = ConsoleColor.Green;//设置背景色
        Console.ForegroundColor = ConsoleColor.White;//设置字体颜色
        Console.Write("【成功】" + 正确信息);
        Console.ResetColor();
        Console.Write("\r\n");
    }
    public static void 输出反色信息(string 信息)
    {
        Console.BackgroundColor = ConsoleColor.White;//设置背景色
        Console.ForegroundColor = ConsoleColor.Black;//设置字体颜色
        Console.Write("【信息】" + 信息);
        Console.ResetColor();
        Console.Write("\r\n");
    }
    public static void 输出自定义信息(bool 类型显示,string 信息类型, string 信息, string 背景色, string 字体颜色,bool 跨行)
    {
        if (Enum.TryParse(背景色, out ConsoleColor 背景色枚举))
        {
            Console.BackgroundColor = 背景色枚举;
        }

        if (Enum.TryParse(字体颜色, out ConsoleColor 字体颜色枚举))
        {
            Console.ForegroundColor = 字体颜色枚举;
        }
        if (类型显示)
        { Console.Write("【" + 信息类型 + "】" + 信息); }
        else
        { Console.Write(信息); }
        Console.ResetColor();
        if (跨行)
        { Console.Write("\r\n");}
    }
}