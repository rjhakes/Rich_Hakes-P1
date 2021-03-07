using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
//https://www.codeproject.com/Articles/1278018/Best-Logging-libraries-for-ASP-NET-MVC
namespace WebErrorLogging.Utilities
{
    public static class Helper
    {
        private static readonly ILogger Infolog;
        private static readonly ILogger Errorlog;
        private static readonly ILogger Warninglog;
        private static readonly ILogger Debuglog;
        private static readonly ILogger Verboselog;
        private static readonly ILogger Fatallog;

        static Helper()
        {

            // 5 MB = 5242880 bytes
            Infolog = new LoggerConfiguration()
                .MinimumLevel.Information()
               .WriteTo.File(new CompactJsonFormatter(), "./logs/Info/log.json",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 5242880,
                rollOnFileSizeLimit: true)
                .CreateLogger();

            Errorlog = new LoggerConfiguration()
                .MinimumLevel.Error()
               .WriteTo.File(new CompactJsonFormatter(), "./ErrorLog/Error/log.json",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 5242880,
                rollOnFileSizeLimit: true)
                .CreateLogger();

            Warninglog = new LoggerConfiguration()
                .MinimumLevel.Warning()
              .WriteTo.File(new CompactJsonFormatter(), "./ErrorLog/Warning/log.json",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            Debuglog = new LoggerConfiguration()
                .MinimumLevel.Debug()
              .WriteTo.File(new CompactJsonFormatter(), "./ErrorLog/Debug/log.json",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            Verboselog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
              .WriteTo.File(new CompactJsonFormatter(), "./ErrorLog/Verbose/log.json",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            Fatallog = new LoggerConfiguration()
                .MinimumLevel.Fatal()
              .WriteTo.File(new CompactJsonFormatter(), "./ErrorLog/Fatal/log.json",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

        }

        public static void WriteError(Exception ex, string message)
        {
            //Error - indicating a failure within the application or connected system
            Errorlog.Write(LogEventLevel.Error, ex, message);
        }

        public static void WriteWarning(Exception ex, string message)
        {
            //Warning - indicators of possible issues or service / functionality degradation
            Warninglog.Write(LogEventLevel.Warning, ex, message);
        }

        public static void WriteDebug(Exception ex, string message)
        {
            //Debug - internal control flow and diagnostic state dumps to facilitate 
            //          pinpointing of recognised problems
            Debuglog.Write(LogEventLevel.Debug, ex, message);
        }

        public static void WriteVerbose(Exception ex, string message)
        {
            // Verbose - tracing information and debugging minutiae; 
            //             generally only switched on in unusual situations
            Verboselog.Write(LogEventLevel.Verbose, ex, message);
        }

        public static void WriteFatal(Exception ex, string message)
        {
            //Fatal - critical errors causing complete failure of the application
            Fatallog.Write(LogEventLevel.Fatal, ex, message);
        }

        public static void WriteInformation(string message)
        {
            //Info
            Infolog.Information(message);
        }

    }
}