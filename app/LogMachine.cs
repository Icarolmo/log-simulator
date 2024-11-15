using Microsoft.Extensions.Logging;

namespace log_simulator.app
{
    public class LogMachine
    {
        private static bool _isRunning;
        private readonly ILogger _logger; 

        public LogMachine(ILogger logger, bool isRunning) 
        { 
            _logger = logger;
            _isRunning = isRunning;
        }

        public void Start()
        {
            for(int i = 0; i < 50000; i++)
            {
                GenerateInfoLog();
                Waiting();
                GenerateWarnLog();
                Waiting();
                GenerateErrorLog();
                Waiting();
            }

            _logger.LogCritical("STOPPING LOG MACHINE");   

            _isRunning = false;
        }

        private void GenerateInfoLog(int quantity = 5)
        {
            for(int i = 0; i < quantity; i++)
            {
                _logger.LogInformation("GENERATION INFORMATION LOG {0} - GENERATION INFORMATION LOG {0} - GENERATION INFORMATION LOG {0}", i, i, i);
            }
        }

        private void GenerateWarnLog(int quantity = 3)
        {
            for (int i = 0; i < quantity; i++)
            {
                _logger.LogWarning("GENERATION WARNING LOG {0} - GENERATION WARNING LOG {0} - GENERATION WARNING LOG {0}", i, i, i);
            }
        }

        private void GenerateErrorLog(int quantity = 1)
        {
            for (int i = 0; i < quantity; i++)
            {
                _logger.LogError("GENERATION ERROR LOG {0} - GENERATION ERROR LOG {0} - GENERATION ERROR LOG {0}", i, i, i);
            }
        }

        private void Waiting(int timeInSecond = 1)
        {
            Thread.Sleep(timeInSecond * 1000);
        }
    }
}
