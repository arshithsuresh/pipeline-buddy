using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Models
{
    public class LatestRunModel
    {
        public string id { get; set; }
        public string state { get; set; }
        public string result { get; set; }
        public string startTime { get; set; }

        public LatestRunModel(string id, string state, string result, string startTime)
        { 
            this.id = id;
            this.state = state;
            this.result = result;
            this.startTime = startTime;
        }
    }
}
