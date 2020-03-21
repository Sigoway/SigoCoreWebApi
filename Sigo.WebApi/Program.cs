using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Sigo.WebApi
{
    /// <summary>
    /// 启动文件
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Log对象
        /// </summary>
        private static ILog _logger;

        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args">启动参数</param>
        public static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                Console.Title = $"{SigoConst.WebAppName} 启动于 {DateTime.Now}";

                InitLog4net();

                _logger.Debug($"********** {SigoConst.WebAppName}服务启动... **********");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                OutputLog($"********** {SigoConst.WebAppName}服务启动失败 ********** 异常信息如下：{ex}");
                Console.ReadKey();
            }
            finally
            {
                if (_logger != null)
                {
                    _logger.Debug($"********** {SigoConst.WebAppName}服务停止 **********");
                }
            }
        }

        /// <summary>
        /// 构造Web主机
        /// </summary>
        /// <param name="args">启动参数</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //config.AddJsonFile("test.json");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel().UseStartup<Startup>();
                });

        /// <summary>
        /// 初始化Log4net
        /// </summary>
        private static void InitLog4net()
        {
            //创建Log4net仓储
            var repository = LogManager.CreateRepository(SigoConst.RepositoryName);
            //监控Log4net配置文件
            var log4netConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "log4net.config");
            XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(log4netConfig));

            _logger = LogManager.GetLogger(SigoConst.RepositoryName, SigoConst.LogName);
        }

        /// <summary>
        /// 处理未知异常
        /// </summary>
        /// <param name="sender">事件发生元对象</param>
        /// <param name="e">事件参数</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OutputLog($"{SigoConst.WebAppName}发生未知错误，异常信息如下：{e.ExceptionObject}");
        }

        /// <summary>
        /// 进程关闭
        /// </summary>
        /// <param name="sender">事件发生元对象</param>
        /// <param name="e">事件参数</param>
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            OutputLog($"********** {SigoConst.WebAppName}服务关闭 **********");
        }

        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        private static void OutputLog(string errorMsg)
        {
            if (_logger != null)
            {
                _logger.Error(errorMsg);
            }
            else
            {
                Console.WriteLine(errorMsg);
            }
        }
    }
}
