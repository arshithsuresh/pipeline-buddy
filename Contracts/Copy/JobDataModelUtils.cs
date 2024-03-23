using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Copy
{
    public static class JobDataModelUtils
    {
        public static void UpdateFrom(this JobDataModel jobDataModel, JobDataModel from)
        {
            jobDataModel.name = from.name;
            jobDataModel.displayName = from.displayName;
            jobDataModel.fullName = from.fullName; 
            jobDataModel.latestRun = from.latestRun;
            jobDataModel.branch = from.branch;
            jobDataModel.pullRequest = from.pullRequest;            
        }
    }
}
