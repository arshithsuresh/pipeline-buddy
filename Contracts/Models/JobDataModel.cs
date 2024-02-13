using PipelineBuddy.Models;
using System.Drawing;

namespace Contracts.Models
{
    public class JobDataModel
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string fullName { get; set; }
        public LatestRunModel latestRun { get; set; }
        public BranchDataModel branch { get; set; }
        public PRDataModel pullRequest { get; set; }

        public string color { get {

                if (latestRun == null)
                    return "#C5D0D8";
                
                if ( latestRun.result == "FAILURE")
                    return "#FF0000";
                else if(latestRun.result == "SUCCESS")
                    return "#00FF00";                
                else return "#0000FF";
            }
        }

        public JobDataModel(string name,string displayName, string fullName, LatestRunModel latestRun, BranchDataModel branch, PRDataModel pullRequest)
        {
            this.name = name; 
            this.displayName = displayName;
            this.fullName = fullName;
            this.latestRun = latestRun;
            this.branch = branch;
            this.pullRequest = pullRequest;
        }
    }

}