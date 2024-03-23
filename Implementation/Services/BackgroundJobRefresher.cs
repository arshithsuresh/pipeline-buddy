using Microsoft.Extensions.Configuration;
using PipelineBuddy.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Implementation.Services
{
    public class BackgroundJobRefresher : IBackgroundJobRefresher
    {
        private Timer _timer;

        private int _TimerRefresh;
        private int JobRefreshInterval;

        private TimerCallback _timerCallbackFn;

        public int TimerRefresh { get => _TimerRefresh; set => _TimerRefresh = value; }
        public Timer saveTimer { get => _timer; }

        private readonly IPRLogger _logger;

        public BackgroundJobRefresher(IConfiguration appSettings, IJobDataService jobDataService, IPRLogger logger)
        {

            _logger = logger;
            TimerRefresh = int.Parse(appSettings.GetSection("checkTimer").Value!);
            JobRefreshInterval = int.Parse(appSettings.GetSection("jobRefreshInterval").Value!);

            Trace.WriteLine($"Background Service Started! Timer Refresh : {TimerRefresh} :: JobRefreshInterval : {JobRefreshInterval}");
            
        }

        public bool UpdateJobPredicate(DateTime jobLastUpdated) {
            _logger.Info($"Checking for new job update ");
            TimeSpan elapsedTimer = DateTime.Now - jobLastUpdated;
            TimeSpan refreshTimer = TimeSpan.FromMilliseconds(JobRefreshInterval);
           return elapsedTimer >= refreshTimer;           
        }
        public void PauseTimer()
        {
            throw new NotImplementedException();
        }

        public void StopTimer()
        {
            
        }

       

        public TimerCallback callbackFunction { 
            get { return _timerCallbackFn;  } 
            set {
                _timerCallbackFn = value;

                if(_timer != null)
                {
                    _timer.Dispose();
                }

                _timer = new Timer(callbackFunction, null, Timeout.Infinite, TimerRefresh);
            } }


    }
}
