using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddyView.Config
{
    public interface IAppConfig
    {
        public string version { set; get; }
    }
}
