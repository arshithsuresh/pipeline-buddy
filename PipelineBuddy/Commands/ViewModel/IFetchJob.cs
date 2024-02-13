using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Commands.ViewModel
{
    public interface IFetchJob
    {
        public void setJobDetails(JobDataModel jobData);
    }
}
