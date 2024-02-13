using Contracts.Models;
using PipelineBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Services
{
    public interface IJobDataService
    {
        void getJobName();
        void getJobPRLink();
        void getJobStatus();

        void createJobDataFile();
        void createJobDataFile(List<JobStorageModel> jobs);
        JobDataCollectionModel readJobDataFile();
        Task<JobDataModel> fetchJobData(string jobId, string organization);
       
    }
}
