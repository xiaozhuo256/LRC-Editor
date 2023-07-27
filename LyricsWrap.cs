using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
//using TagLib;
using System.Linq.Expressions;
using OpenCCNET;

/// <summary>
/// 工具类，用以抽象处理歌词相关方法
/// </summary>
class LyricsWrap
{
    public static List<string> ProcessLyrics(FileInfo targetFile)
    {
        //获取文件内容
        string[] content = File.ReadAllLines(targetFile.FullName);
        List<string> result = new(); //存储还原结果

        foreach (string line in content)
        {
            string[] strings = line.Split("]"); //以时间结束的反方括号为标志分割时间与歌词本身
            string time = strings[0] + "]", //分割出时间部分，补回反括
                   lyric = strings[1]; //分割出歌词

            //基于歌词空格分割语言
            string[] toSplice = lyric.Split(" ");
            string origin = string.Empty, translate = string.Empty;
            for(int i = 1; i <= toSplice.Length; i++)
            {
                if(i <= (toSplice.Length / 2))
                    origin += " " + toSplice[i - 1];
                else
                    translate += " " + toSplice[i - 1];
            }
            result.Add(time + origin);
            result.Add(time + translate);
        }

        return result;

        /*
        foreach (string 文件 in 文件交互.lrc文件列表)
        {
            List<string> 歌词列表 = new List<string>();
            输出.输出反色信息("正在处理" + 文件);
            foreach (string 歌词行 in 文件交互.读取文件(文件))
            {
                输出.输出反色信息(歌词行);
                //提取时间戳和内容
                int 结束索引 = 歌词行.IndexOf(']'); // 查找 ']' 的位置
                if (结束索引 != -1)
                {
                    string 时间戳 = 歌词行.Substring(0, 结束索引 + 1); // 获取时间戳部分
                    string 内容 = 歌词行.Substring(结束索引 + 1).Trim(); // 获取内容部分并去除多余空格

                    //内容编辑1 分割
                    string[] 词段们 = 内容.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    List<string> 词段列表 = new List<string>(词段们);

                    foreach (string 词段 in 词段列表)
                    {
                        Console.Write("*" + 词段 + "*" + "\r\n");
                        Console.WriteLine(" " + 语言.判断语言(词段));
                    }

                }
                else
                {

                }

            }
            Console.ReadLine();
        
        }
        */
    }

/*
    public static void flac处理()
    {
        foreach (string 文件 in 文件交互.flac文件列表)
        {
            StreamWriter 写入器 = new StreamWriter(文件);
            try
            {
                using (var file = TagLib.File.Create(文件))
                {

                    // 检查是否有歌词标签
                    if (file.Tag.Lyrics != null)
                    {
                        foreach (string 歌词行 in 文件交互.分行文本(file.Tag.Lyrics))
                        {
                            输出.输出反色信息(歌词行);
                            //提取时间戳和内容
                            int 结束索引 = 歌词行.IndexOf(']'); // 查找 ']' 的位置
                            if (结束索引 != -1)
                            {
                                string 时间戳 = 歌词行.Substring(0, 结束索引 + 1); // 获取时间戳部分
                                string 内容 = 歌词行.Substring(结束索引 + 1).Trim(); // 获取内容部分并去除多余空格

                                //内容编辑1 分割
                                string[] 词段们 = 内容.Split(new char[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries);

                                List<string> 词段列表 = new List<string>(词段们);

                                foreach (string 词段 in 词段列表)
                                {
                                    try
                                    {
                                        写入器.WriteLine("你好，世界！！");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("异常信息：" + e.Message);
                                    }
                                    finally
                                    {
                                        Console.WriteLine("执行 finally 块。");
                                    }
                                    Console.WriteLine(" " + 语言.判断语言(词段));
                                }

                            }
                            else
                            {

                            }

                        }
                    }
                    else
                    {
                        输出.输出错误信息("该 FLAC 文件没有歌词标签！");
                    }
                }
            }
            catch (Exception ex)
            {
                输出.输出错误信息("读取 FLAC 文件时出现异常：" + ex.Message);
            }
            写入器.Close();
            Console.ReadLine();
        }
    }
*/
}

/*
class 文件交互
{
    public static List<string> 总文件列表 = new List<string>();
    public static List<string> lrc文件列表 = new List<string>();
    public static List<string> flac文件列表 = new List<string>();
    public static List<string> 其他文件列表 = new List<string>();

    public static void 遍历目录(string 目录路径,List<string> 总文件列表, List<string> lrc文件列表, List<string> flac文件列表, List<string> 其他文件列表)
    {
        DirectoryInfo 目录信息 = new DirectoryInfo(目录路径);
        FileInfo[] 文件们 = 目录信息.GetFiles(); // 文件
        DirectoryInfo[] 子目录们 = 目录信息.GetDirectories(); // 子目录

        foreach (FileInfo 文件信息 in 文件们)
        {
            总文件列表.Add(文件信息.FullName); // 添加文件名到列表中
            
            if(文件信息.FullName.Contains(".lrc") || 文件信息.FullName.Contains(".flac"))
            {
                if (文件信息.FullName.Contains(".lrc"))
                {
                    输出.输出自定义信息(true,"文件", "已找到 " + 文件信息.DirectoryName + "\\", "0", "DarkGray", false); 
                    输出.输出自定义信息(false, "", 文件信息.Name, "", "Green", true);
                    lrc文件列表.Add(文件信息.FullName);

                }
                if (文件信息.FullName.Contains(".flac"))
                {
                    输出.输出自定义信息(true, "文件", "已找到 " + 文件信息.DirectoryName + "\\", "0", "DarkGray", false); 
                    输出.输出自定义信息(false, "", 文件信息.Name, "", "Blue", true);
                    flac文件列表.Add(文件信息.FullName);
                }
            }
            else
            {
                输出.输出自定义信息(true, "文件", "已找到 " + 文件信息.DirectoryName + "\\", "0", "DarkGray", false); 
                输出.输出自定义信息(false, "", 文件信息.Name, "", "White", true);
                其他文件列表.Add(文件信息.FullName);
            }
        }

        // 递归遍历子目录内的文件列表
        foreach (DirectoryInfo 子目录信息 in 子目录们)
        {
            遍历目录(子目录信息.FullName,总文件列表,lrc文件列表,flac文件列表,其他文件列表);
        }
    }
    public static void 打开文件夹()
    {

        // 使用 DirectoryInfo 打开目录
        DirectoryInfo 打开的目录 = new DirectoryInfo(Program.用户输入_文件夹地址in);

        // 检查目录是否存在
        if (打开的目录.Exists)
        {
            输出.输出正确信息("已找到" + Program.用户输入_文件夹地址in + "文件夹");
            // 获取文件夹中的文件和子文件夹
            string[] 文件们 = Directory.GetFiles(Program.用户输入_文件夹地址in);
            string[] 子文件夹们 = Directory.GetDirectories(Program.用户输入_文件夹地址in);

            // 检查文件夹是否为空
            if (文件们.Length == 0 && 子文件夹们.Length == 0)
            {
                输出.输出错误信息("文件夹为空");
            }
            else
            {

                文件交互.遍历目录(Program.用户输入_文件夹地址in, 总文件列表, lrc文件列表, flac文件列表, 其他文件列表);
                if (总文件列表.Any(文件 => 文件.Contains(".lrc") || 文件.Contains(".flac")))
                {
                    输出.输出正确信息("已检测到文件并加载");
                    输出.输出自定义信息(true, "文件", "总文件数：" + 总文件列表.Count, "0", "White",true);
                    输出.输出自定义信息(true, "文件", "lrc文件数：" + lrc文件列表.Count, "0", "Green", true);
                    输出.输出自定义信息(true, "文件", "flac文件数：" + flac文件列表.Count, "0", "Blue", true);
                    输出.输出自定义信息(true, "文件", "其他文件数：" + 其他文件列表.Count, "0", "Gray", true);
                    Program.用户进度 = 2;
                }
                else
                {
                    输出.输出错误信息("文件夹不包含 .lrc 格式或 .flac 格式的文件！");
                }
            }
        }
    

        else
        {
            输出.输出错误信息("未找到文件夹，正在创建...");
            try
            {
                // 创建“lrcin”文件夹
                Directory.CreateDirectory(Program.用户输入_文件夹地址in);
                输出.输出正确信息("已创建“" + Program.用户输入_文件夹地址in + "”文件夹");
            }
            catch (Exception ex)
            {
                输出.输出错误信息("创建“" + Program.用户输入_文件夹地址in + "”文件夹时出现错误：" + ex.Message);
            }
        }
    }
    public static List<string> 读取文件(string 文件地址)
    {
        List<string> 歌词列表 = new List<string>();
        string line;
        // 创建一个 StreamReader 对象，用于读取文件内容
        StreamReader sr = new StreamReader(文件地址);
        // 循环读取并显示文件内容，直到到达文件末尾
        while ((line = sr.ReadLine()) != null)
        {
            歌词列表.Add(line);
        }

        // 关闭文件
        sr.Close();
        return 歌词列表;
    }
    public static List<string> 分行文本(string 文本内容)
    {

        string[] 词段们 = 文本内容.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        List<string> 集合 = new List<string>(词段们);
        return 集合;
    }
}
class 语言
{   
    public static string 判断字符(string 输入文本)
    {
        
    /*
        日本假名的unicode编码：
        [\u3040-\u309F \u30A0-\u30FF]
    
        汉字的unicode编码：
        [\u4E00-\u9FFF \u3400-\u4DBF]
        [\u4E00-\u9FFF \u3400-\u4DBF \U00020000-\U0002A6DF \U0002A700-\U0002B73F \U0002B740-\U0002B81F \U0002B820-\U0002CEAF \U0002CEB0-\U0002EBEF \U00030000-\U0003134F]
    
        韩文字符的unicode编码：
        [\uac00-\ud7ff]
    
        英文的unicode编码:
        [a-zA-Z]
    *
         

        if (System.Text.RegularExpressions.Regex.IsMatch(输入文本, @"^[\u3040-\u309F \u30A0-\u30FF]+$"))
        {
            return ("日");
        }
        else if (System.Text.RegularExpressions.Regex.IsMatch(输入文本, @"^[\uAC00-\uD7AF]+$"))
        {
            return ("韩");
        }
        else if (System.Text.RegularExpressions.Regex.IsMatch(输入文本, @"^[a-zA-Z]+$"))
        {
            return ("英");
        }
        else if (System.Text.RegularExpressions.Regex.IsMatch(输入文本, @"^[\d]+$"))
        {
            return ("数");
        }
        else if (System.Text.RegularExpressions.Regex.IsMatch(输入文本, @"^[\u4E00-\u9FFF\u3400-\u4DBF\uD840-\uD868\uDC00-\uDFFF\uD869-\uD86A\uDC00-\uDEDF\uD86A-\uD86C\uDC00-\uDFFF\uD86C-\uD86F\uDC00-\uDEFF\uD869-\uD86A\uDF00-\uDFFF\u20000-\u2A6DF\u2A700-\u2B73F\u2B740-\u2B81F\u2B820-\u2CEAF\u2CEB0-\u2EBEF\u30000-\u3134F]+$"))
        {
            return ("中");
        }
        else
        {
            return ("");
        }
    }
    public static string 判断语言(string 输入文本)
    {
        int 汉字字符数 = 0;
        int 韩文字符数 = 0;
        int 英文字符数 = 0;
        int 假名字符数 = 0;
        int 总字数 = 0;
        int 标点符号数量 = System.Text.RegularExpressions.Regex.Matches(输入文本, @"\p{P}").Count;
    
        总字数 = 输入文本.Length - 标点符号数量;
    
        for (int i = 0; i < 输入文本.Length; i++)
        {
            // 将字符转换为字符串
            string 字符 = 输入文本[i].ToString();
    
            if (判断字符(字符) == "中")
            {
                汉字字符数++; 
            }
            else if (判断字符(字符) == "韩")
            {
                韩文字符数++;
            }
            else if (判断字符(字符) == "日")
            {
                假名字符数++;
            }
            else if (判断字符(字符) == "英")
            {
                英文字符数++;
            }
        }
        if ((总字数 == 汉字字符数) && !(总字数 == 0 || 汉字字符数 == 0))
        {
            return ("中文");
        }
        else if (总字数 == 英文字符数 && !(总字数 == 0 || 英文字符数 == 0))
        {
            return ("英文");
        }
        else if (((汉字字符数 > 0 && 假名字符数 > 0 && 汉字字符数 + 假名字符数 == 总字数) || 总字数 == (汉字字符数 + 假名字符数 + 英文字符数) || (总字数 == 假名字符数)) && !(总字数 == 0))
        {
            return ("日文");
        }
        else if (((汉字字符数 > 0 && 韩文字符数 > 0 && 汉字字符数 + 韩文字符数 == 总字数) || 总字数 == (汉字字符数 + 韩文字符数 + 英文字符数) || (总字数 == 韩文字符数)) && !(总字数 == 0))
        {
            return ("韩文");
        }

        else if (输入文本.Contains(" "))
        {
            输出.输出错误信息("字段" + "“" + 输入文本 + "”" + "包含空格！");
            return ("");
        }
        else
        {
            输出.输出错误信息("“" + 输入文本 + "”" + "是无法识别的语言！");
            输出.输出反色信息("\r\n总字数：" + 总字数 + "\r\n汉字字符数：" + 汉字字符数 + "\r\n韩文字符数:" + 韩文字符数 + "\r\n假名字符数:" + 假名字符数 + "\r\n英文字符数:" + 英文字符数 + "\r\n标点符号数：" + 标点符号数量);
            return ("");
        }
    }
}
*/