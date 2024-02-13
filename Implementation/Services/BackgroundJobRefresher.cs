using Microsoft.Extensions.Configuration;
using PipelineBuddy.Services;
using System.Diagnostics;

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


        public BackgroundJobRefresher(IConfiguration appSettings, IJobDataService jobDataService)
        {

            TimerRefresh = int.Parse(appSettings.GetSection("checkTimer").Value!);
            JobRefreshInterval = int.Parse(appSettings.GetSection("jobRefreshInterval").Value!);

            Trace.WriteLine($"Background Service Started! Timer Refresh : {TimerRefresh} :: JobRefreshInterval : {JobRefreshInterval}");
            
        }

        public bool UpdateJobPredicate(DateTime jobLastUpdated) {
           return DateTime.Now - jobLastUpdated >= TimeSpan.FromMilliseconds(JobRefreshInterval);           
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
