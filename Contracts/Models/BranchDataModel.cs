using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Models
{
    public class BranchDataModel
    {
        public bool isPrimary { get; set; }
        public string url { get; set; }

        public BranchDataModel(bool isPrimary, string url) {
            this.isPrimary = isPrimary;
            this.url = url;
        }
    }
}
