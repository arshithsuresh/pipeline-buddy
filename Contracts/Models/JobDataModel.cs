using PipelineBuddy.Models;
using System.Text.Json.Serialization;

namespace Contracts.Models
{
    public class JobDataModel
    {
        private string _nickName;
        public string name { get; set; }
        public string nickName
        {
            get
            {
                if (_nickName == null) return displayName;
                else return _nickName;
            }
            set { _nickName = value; }
        }
        public string displayName { get; set; }
        public string fullName { get; set; }
        public LatestRunModel latestRun { get; set; }
        public BranchDataModel branch { get; set; }
        public PRDataModel pullRequest { get; set; }

        public string color
        {
            get
            {

                if (latestRun == null)
                    return "#C5D0D8";

                if (latestRun.result == "FAILURE")
                    return "#FF0000";

                else if (latestRun.result == "SUCCESS")
                    return "#00FF00";

                else return "#0000FF";
            }
        }

        [JsonConstructor]
        public JobDataModel(string name, string displayName, string fullName, LatestRunModel latestRun, BranchDataModel branch, PRDataModel pullRequest)
        {
            this.name = name;
            this.displayName = displayName;
            this.fullName = fullName;
            this.latestRun = latestRun;
            this.branch = branch;
            this.pullRequest = pullRequest;
        }

        public JobDataModel(string name, string nickname)
        {
            this.name = name;
            this.displayName = name;
            this.fullName = name;
            this._nickName = nickname;
            this.latestRun = null;
            this.branch = null;
            this.pullRequest = null;
        }

        public JobDataModel(JobDataModel jobDataModel)
        {
            this.name = jobDataModel.name;
            this.displayName = jobDataModel.displayName;
            this.fullName = jobDataModel.fullName;
            this.latestRun= jobDataModel.latestRun;
            this.branch= jobDataModel.branch;
            this.pullRequest= jobDataModel.pullRequest;

            if (jobDataModel._nickName != null)
            {
                this._nickName = jobDataModel._nickName;
            }
        }
    }

}