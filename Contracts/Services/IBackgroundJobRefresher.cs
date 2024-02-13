using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Services
{
    public interface IBackgroundJobRefresher
    {        
        int TimerRefresh { get; set; }
        Timer saveTimer { get;  }
        void PauseTimer();
        void StopTimer();
        void StartService() {
            saveTimer.Change(0, TimerRefresh);
        }
        TimerCallback callbackFunction { get; set; }

        bool UpdateJobPredicate(DateTime jobLastUpdated);

    }
}
