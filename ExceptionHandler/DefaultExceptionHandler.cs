using PipelineBuddy.ExceptionHandler;
using PipelineBuddy.Services;

namespace ExceptionHandler
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        private IPRLogger _logger { get; set; }
        public DefaultExceptionHandler(IPRLogger logger)
        {
            _logger = logger;
        }

        public void HandleException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
            _logger.Fatal(e.Message);
        }

    }
}