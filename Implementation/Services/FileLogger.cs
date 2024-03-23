using Microsoft.Extensions.Configuration;
using PipelineBuddy.Services;

namespace Implementation.Services
{
    public class FileLogger : IPRLogger
    {
        private readonly string logFileName = "pr_buddy_log.log";
        public FileLogger(IConfiguration configRoot)
        {
            logFileName = configRoot.GetSection("logFile").Value;
        }

        public void Error(string message)
        {
            Log($"ERROR ::  {message}");
        }

        public void Fatal(string message)
        {
            Log($"FATAL ::  {message}");
        }

        public void Info(string message)
        {
            Log($"INFO ::  {message}");
        }

        public void Warn(string message)
        {
            Log($"WARNING ::  {message}");
        }

        private void Log(string message)
        {
            var currentDateTime = DateTime.Now;
            WriteToFile($"[{currentDateTime}] {message}");
        }

        private void WriteToFile(string log)
        {
            using (StreamWriter writer = new StreamWriter(logFileName,true))
            {
                writer.WriteLine(log);
            }
        }
    }
}
