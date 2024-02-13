using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Models
{
    public class JobStorageModel
    {
        public DateTime lastUpdated { get;set; }
        public string username { get;set; }
        public string organization { get; set; }
        public string jobId { get; set; }
        public JobDataModel jobData { get; set; }

        
        public JobStorageModel(DateTime lastUpdated, string username, string organization, string jobId, JobDataModel jobData)
        {
            this.lastUpdated = lastUpdated;
            this.username = username;
            this.organization = organization;
            this.jobId = jobId;
            this.jobData = jobData;
        }
    }
}
