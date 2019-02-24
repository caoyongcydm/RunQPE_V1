using System;
using System.Diagnostics;

namespace RunQPE_V1
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++      RunQPE_V1                                +++++++++");
            Console.WriteLine("+++++      Create By CaoYong 2018.12.20             +++++++++");
            Console.WriteLine("+++++      QQ: 403637605                            +++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            ///////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            //打开计时器
            Stopwatch sw = new Stopwatch(); 
            sw.Start();
            ///////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            string appDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;               //程序启动文件夹
            ///////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            //时间处理，获取最新模式时间或者指定模式时间进行运算(北京时)
            DateTime dtNow = DateTime.Now;                         //程序启动时间（北京时）                          
            if (args.Length == 0)                                  //实时运算处理
            {
                dtNow = DateTime.Now;
            }
            else if (args.Length == 1 && args[0].Length == 12)     //指定日期运算处理(北京时)
            {
                try
                {
                    int argYr = int.Parse(args[0].Substring(0, 4));
                    int argMo = int.Parse(args[0].Substring(4, 2));
                    int argDy = int.Parse(args[0].Substring(6, 2));
                    int argHr = int.Parse(args[0].Substring(8, 2));
                    int argMn = int.Parse(args[0].Substring(10, 2));
                    dtNow = new DateTime(argYr, argMo, argDy, argHr, argMn, 0);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("date args content is not right!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("date args is not right!");
                return;
            }
            ///////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("step1: download surface data...");
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.CreateNoWindow = true;         //不显示程序窗口
                p.StartInfo.UseShellExecute = false;       //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;  //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;  //重定向标准错误输出
                p.Start();//启动程序
                p.StandardInput.WriteLine(appDir + @"DownloadCmissData_V1/DownloadCmissData_V1.exe " + dtNow.Year.ToString("d4") + dtNow.Month.ToString("d2") + dtNow.Day.ToString("d2") + dtNow.Hour.ToString("d2") + dtNow.Minute.ToString("d2"));
                p.StandardInput.WriteLine(@"exit");
                p.StandardInput.AutoFlush = true;
                p.StandardOutput.ReadToEnd();
                p.StandardError.ReadToEnd();
                p.WaitForExit(); //等待程序结束
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                Console.WriteLine("Dwonload Surface Wrong");
            }
            ///////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("step2: calculate qpe...");
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.CreateNoWindow = false;         //不显示程序窗口
                p.StartInfo.UseShellExecute = false;       //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;  //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;  //重定向标准错误输出
                p.Start();//启动程序
                p.StandardInput.WriteLine(appDir + @"QPEAnalysis_V1/QPEAnalysis_V1.exe " + dtNow.Year.ToString("d4") + dtNow.Month.ToString("d2") + dtNow.Day.ToString("d2") + dtNow.Hour.ToString("d2") + dtNow.Minute.ToString("d2"));
                p.StandardInput.WriteLine(@"exit");
                p.StandardInput.AutoFlush = true;
                p.StandardOutput.ReadToEnd();
                p.StandardError.ReadToEnd();
                p.WaitForExit(); //等待程序结束
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("QPE Analysis Wrong");
            }
            ///////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            sw.Stop();
            Console.WriteLine("time elasped: " + sw.Elapsed);
            ///////////////////////////////////////////////////////////////////////////////
        }
    }
}