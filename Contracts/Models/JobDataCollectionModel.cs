using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Models
{
    public class JobDataCollectionModel
    {
        public DateTime lastUpdated { get; set; }
        public List<JobStorageModel> jobList { get; set; }

        public JobDataCollectionModel(DateTime lastUpdated, List<JobStorageModel> jobList) {
            this.lastUpdated = lastUpdated;
            this.jobList = jobList;
        }
    }
}
