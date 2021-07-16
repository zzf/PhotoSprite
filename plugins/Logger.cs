using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;

namespace PhotoSprite.Plugins
{
    public class Logger
    {
        private static Logger mLogger;
        public static Logger Instance()
        {
            if (null == mLogger)
                mLogger = new Logger();

            return mLogger;
        }

        /// <summary>
        /// 输出格式化
        /// </summary>
        string FORMAT = "[yyyy-MM-dd_HH:mm:ss_ffff] ";

        /// <summary>
        /// 日志信息输出类
        /// </summary>
        public string logFile = ""; // 现场log文件路径
        public string oldLogFile = "";  //  上一次运行时的log

        /// <summary>
        /// 默认log文件
        /// </summary>
        public void Init()
        {
            logFile = AppDomain.CurrentDomain.BaseDirectory + "/log/cur_log.txt";
            oldLogFile = AppDomain.CurrentDomain.BaseDirectory + "/log/old_log.txt";

            // 路径
            String dir = System.IO.Path.GetDirectoryName(logFile);
            try
            {
                if (Directory.Exists(dir))
                {
                    // log目录已存在
                    try
                    {
                        if (File.Exists(oldLogFile))
                        {
                            File.Delete(oldLogFile);
                        }
                        if (File.Exists(logFile))
                        {
                            File.Move(logFile, oldLogFile);
                            if (File.Exists(logFile))
                                File.Delete(logFile);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("File Error! " + ex.StackTrace.ToString() + ex.Message);
                    }
                }
                else
                {
                    // 创建log目录
                    Directory.CreateDirectory(dir);
                }
                // 创建当前程序运行log文件
                FileStream fs = File.Create(logFile);
                fs.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Create Directory Error! " + ex.StackTrace.ToString() + ex.Message);
            }
        }

        /// <summary>
        /// 追加一条log信息
        /// </summary>
        public void Append(string text)
        {
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.Write(DateTime.Now.ToString(FORMAT) + text);
            }
        }

        /// <summary>
        /// 追加一行log信息
        /// </summary>
        public void Log(string text)
        {
            text += "\r\n";
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.Write(DateTime.Now.ToString(FORMAT) + text);
            }
        }
    }
}
