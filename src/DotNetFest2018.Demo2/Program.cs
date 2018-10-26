using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DotNetFest2018.Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .WriteTo.Console(LogEventLevel.Verbose)
                .CreateLogger();

            while (true)
            {
                logger.Verbose("Verbose");

                logger.Debug("Debug");

                logger.Information("Information");

                logger.Warning("Warning");

                logger.Error("Error");

                logger.Fatal("Fatal");

                Console.WriteLine($"Minimum level is {loggingLevelSwitch.MinimumLevel}");

                Console.WriteLine("--------------------------------------------------");

                Thread.Sleep(4000);
            }
        }
    }
}
