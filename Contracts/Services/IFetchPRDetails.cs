using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Services
{
    public interface IFetchPRDetails
    {
        void getPRDetails(string prId);
    }
}
