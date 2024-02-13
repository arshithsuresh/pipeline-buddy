using PipelineBuddy.Commands.ViewModel;
using PipelineBuddy.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Commands
{
    public class FetchJobDetailsCommand : AsyncCommandBase
    {
        private IJobDataService jobService;
        private IFetchJob viewModel;
        public FetchJobDetailsCommand(IJobDataService jobService, IFetchJob viewModel ) { 
            this.jobService = jobService;
            this.viewModel = viewModel;
        }
        protected override async Task  ExecuteAync(object? parameter)
        {
            var jobData = await jobService.fetchJobData("PR-273","noorg");
            viewModel.setJobDetails(jobData);
            Trace.Write(jobData);
        }
    }
}
