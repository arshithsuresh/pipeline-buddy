using Contracts.Models;
using PipelineBuddy;
using PipelineBuddy.Models;
using PipelineBuddy.Services;
using PipelineBuddyView.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddyView.ViewModel
{
    public class AddNewViewModel : INotifyPropertyChanged
    {
        public Dictionary<string, string> organizations;

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IConfigService _configService;
        private readonly AllJobDataStore _allJobDataStore;


        public AddNewViewModel()
        {
            _configService = App.GetService<IConfigService>();
            _allJobDataStore = App.GetService<AllJobDataStore>();
            Initialize();
        }

        public void AddNewJob(string jobId, string organization)
        {
            var jobData = new JobDataModel(jobId, jobId, jobId, null, null, null);
            var jobStorageEntry = new JobStorageModel(DateTime.Now, _configService.currentConfig.username, organization, jobId, jobData);

            _allJobDataStore.AddNewJob(jobStorageEntry);
        }

        public void AddNewJob(string jobId, string organization, string nickName)
        {
            var jobData = new JobDataModel(jobId, nickName);
            var jobStorageEntry = new JobStorageModel(DateTime.Now, _configService.currentConfig.username, organization, jobId, jobData);

            _allJobDataStore.AddNewJob(jobStorageEntry);


        }

        private void Initialize()
        {
            var orgs = _configService.getOrganizations();
            organizations = orgs;
            RaisePropertyChanged(nameof(organizations));
        }


        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
