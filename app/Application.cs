using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace log_simulator.app
{
    public class Application 
    {
        private volatile bool isRunning;
        public bool IsRunning
        {
            get => isRunning;
            set => isRunning = value;
        }

        public Application()
        {
            this.IsRunning = true;
        }

        public void Start()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console() 
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) 
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog(); 
                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("INICIALIZE GENERATION LOG APPLICATION");
            
            var machine = new LogMachine(logger, IsRunning);

            new Thread(() =>
            {
                machine.Start();
            }).Start();

            while (isRunning)
            {
                Thread.Sleep(5 * 1000);
            }

            logger.LogInformation("FINISH GENERATION LOG PROCESS. APPLICATION DOWN...");
        }
    }
}
