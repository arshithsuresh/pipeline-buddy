using Contracts.Models;
using PipelineBuddy;
using PipelineBuddy.Commands;
using PipelineBuddy.Commands.ViewModel;
using PipelineBuddy.Models;
using PipelineBuddy.Services;
using PipelineBuddyView.Store;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PipelineBuddyView.ViewModel
{
    public class MainViewModel : IFetchJob, INotifyPropertyChanged
    {
        private AllJobDataStore _allJobStore;
        private SelectedJobDataStore _selectedJobStore;
        private IJobDataService jobDataService;
        private IBackgroundJobRefresher _jobRefresher;

        private bool isFetching = false;

        public int currentJobDataIndex
        {
            get { return _allJobStore.currentJobIndex; }
            set
            {
                _allJobStore.currentJobIndex = value;
            }
        }
        public ObservableCollection<JobStorageModel> Jobs => _allJobStore.Jobs;
        public ConfigModel config { get; set; }

        public ICommand ShowWindowCommand { get; set; }

        public string currentJobName
        {
            get
            {
                if (_selectedJobStore.SelectedJob != null)
                    return _selectedJobStore.SelectedJob.nickName;
                else
                    return "No jobs found!";
            }
        }

        public string currentJobColor
        {
            get
            {
                if (_selectedJobStore.SelectedJob != null)
                {
                    if (isFetching)
                        return "#FCE205";
                    else
                        return _selectedJobStore.SelectedJob.color;
                }
                else
                {
                    return "#C5D0D8";
                }

            }
        }

        public FetchJobDetailsCommand FetchJobCommand { get; set; }

        public Visibility refreshBtnVisible
        {
            get
            {
                if (_selectedJobStore.SelectedJob != null)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        public MainViewModel()
        {
            config = App.GetService<IConfigService>().currentConfig;
            _selectedJobStore = App.GetService<SelectedJobDataStore>();
            _allJobStore = App.GetService<AllJobDataStore>();
            jobDataService = App.GetService<IJobDataService>();
            _jobRefresher = App.GetService<IBackgroundJobRefresher>();

            _allJobStore.CurrentJobChanged += _allJobStore_CurrentJobChanged;
            _selectedJobStore.SelectedJobChanged += _allJobStore_CurrentJobChanged;
            _allJobStore.Jobs.CollectionChanged += Jobs_CollectionChanged;
            InitWindow();

            StartRefresherService();

        }

        private void Jobs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var addedJob = e.NewItems[0] as JobStorageModel;
                var addedIndex = e.NewStartingIndex;

                UpdateJobData(addedIndex);

                Trace.WriteLine($"New Job Added :: {addedJob.jobId} :: {addedIndex} ");
            }
        }

        private void InitWindow()
        {
            var collection = jobDataService.readJobDataFile();
            _allJobStore.AddFromCollection(collection);
        }

        private void _allJobStore_CurrentJobChanged()
        {
            RaisePropertyChanged(nameof(currentJobName));
            RaisePropertyChanged(nameof(currentJobColor));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddCurrentToWatchlist()
        {
            _allJobStore.AddToWatchList(_allJobStore.currentJobIndex);
        }

        public void RemoveCurrentFromWatchlist(int index)
        {
            _allJobStore.RemoveWatchList(_allJobStore.currentJobIndex);

        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public async Task UpdateCurrentJobData(string jobId, string organization)
        {
            isFetching = true;
            RaisePropertyChanged(nameof(currentJobColor));
            JobDataModel updatedJob = await jobDataService.fetchJobData(jobId, organization);
            isFetching = false;
            _allJobStore.UpdateCurrentJob(updatedJob);

        }

        public async Task UpdateJobData(int index)
        {
            var toChange = _allJobStore.Jobs[index];
            JobDataModel updatedJob = await jobDataService.fetchJobData(toChange.jobId, toChange.organization);
            _allJobStore.UpdateJob(index, updatedJob);

            if (index == currentJobDataIndex)
            {
                _allJobStore.UpdateCurrentSelectedJob();
            }
        }

        public void DeleteCurrentJob()
        {
            Trace.WriteLine($"Deleting Current Job {currentJobName}");
            _allJobStore.DeleteCurrentJob();
        }

        public void setJobDetails(JobDataModel jobData)
        {
            // Change job data in store
        }

        public void RefreshCurrentJob()
        {
            var selectedJob = _allJobStore.Jobs[_allJobStore.currentJobIndex];
            UpdateCurrentJobData(selectedJob.jobData.name, selectedJob.organization);
        }

        void Dispose()
        {
            _allJobStore.CurrentJobChanged -= _allJobStore_CurrentJobChanged;
        }

        protected void StartRefresherService()
        {
            _jobRefresher.callbackFunction = RefreshJobs;
            _jobRefresher.StartService();
        }

        public void SaveCurrentJobs()
        {
            Trace.WriteLine("Svaing All Jobs");
            jobDataService.createJobDataFile(_allJobStore.Jobs.ToList());
        }

        public void OpenGHE()
        {
            var gheLink = _allJobStore.Jobs[currentJobDataIndex].jobData.pullRequest.url;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = gheLink,
                UseShellExecute = true
            });
        }

        public async void RefreshJobs(object? sender)
        {
            Trace.WriteLine($"Refreshing {Jobs.Count} jobs.");
            for (int i = 0; i < _allJobStore.Jobs.Count; i++)
            {
                var job = _allJobStore.Jobs[i];
                if (_jobRefresher.UpdateJobPredicate(job.lastUpdated))
                {
                    // Update Job
                    await UpdateJobData(i);
                    // Random Delay
                    await Task.Delay(500);
                    job.lastUpdated = DateTime.Now + TimeSpan.FromMilliseconds(_jobRefresher.TimerRefresh * i);
                }
            }
        }

    }
}
