using PipelineBuddy.ExceptionHandler;

namespace ExceptionHandler
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public DefaultExceptionHandler()
        {
        }

        public void HandleException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }

    }
}