
namespace PipelineBuddy.ExceptionHandler
{
    public interface IExceptionHandler
    {
        void HandleException(object sender, UnhandledExceptionEventArgs args);
    }
}
