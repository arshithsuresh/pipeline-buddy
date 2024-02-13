using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddyView.Store
{
    public class SelectedJobDataStore
    {
        private JobDataModel _selectedJob;

        public JobDataModel SelectedJob
        {
            get 
            { return _selectedJob; }
            set 
            { 
                _selectedJob = value;
                SelectedJobChanged?.Invoke();
            }
        }

        public SelectedJobDataStore() {
            Trace.WriteLine("New SelecetedDataStore");
        }

        public event Action SelectedJobChanged;

    }
}
