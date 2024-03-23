using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Services
{
    public interface IPRLogger
    {
        public void Info(string message);
        public void Warn(string message);
        public void Error(string message);
        public void Fatal(string message);

    }
}
