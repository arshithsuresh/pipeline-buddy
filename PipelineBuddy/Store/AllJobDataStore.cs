using Contracts.Models;
using PipelineBuddy;
using PipelineBuddy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddyView.Store
{
    
    public class AllJobDataStore
    {
        private DateTime _lastUpdated;

        private SelectedJobDataStore _selectedJobDataStore;
        private int _currentJobIndex = 0;
        public ObservableCollection<JobStorageModel> Jobs;
        public ObservableCollection<int> WatchListedJobs;

        public AllJobDataStore()
        {
            _selectedJobDataStore = App.GetService<SelectedJobDataStore>(); 
            Jobs = new ObservableCollection<JobStorageModel>();
            WatchListedJobs = new ObservableCollection<int>();
        }

        
        public int currentJobIndex  { 
            get{ return _currentJobIndex; }
            set
            {
                if (Jobs.Count <= 0)
                    _currentJobIndex = -1;
                else { 
                    _currentJobIndex = Math.Clamp(value, 0, Jobs.Count - 1);
                    Trace.WriteLine($"ALl Job Store :: Moved to {_currentJobIndex}");
                    UpdateCurrentSelectedJob();
                }
                CurrentJobChanged?.Invoke();
            }
        }

        public void UpdateCurrentSelectedJob() {
            if (Jobs.Count <= 0) {
                _selectedJobDataStore.SelectedJob = null;
                return;
            }

            _selectedJobDataStore.SelectedJob = Jobs[_currentJobIndex].jobData;
            CurrentJobChanged?.Invoke();

        }

        public void AddToWatchList(int jobIndex) {
            if (WatchListedJobs.Contains(jobIndex))
                return;

            WatchListedJobs.Add(jobIndex);
        }
        public void RemoveWatchList(int jobIndex) {
            if (WatchListedJobs.Contains(jobIndex))
                WatchListedJobs.Remove(jobIndex);
        }


        public bool AddNewJob(JobStorageModel jobData) 
        {            
            Jobs.Add(jobData);

            if (_selectedJobDataStore.SelectedJob == null)
            {
                _selectedJobDataStore.SelectedJob = Jobs[0].jobData;
            }
            updateLastUpdated();

            Trace.WriteLine("Jop Added "+Jobs.Count+" :: "+_lastUpdated.ToString());
            return true;
        }

        public bool DeleteCurrentJob()
        {
            if (Jobs.Count == 0) return false;

            DeleteJob(_currentJobIndex);
            UpdateCurrentSelectedJob();
            return true;
        }

        public bool DeleteJob(int index) 
        {
            if (index < 0)
                return false;

            Jobs.RemoveAt(index);
            _currentJobIndex--;
            updateLastUpdated();
            return true;
        }
        public bool DeleteJob(JobStorageModel jobData)
        {
            Jobs.Remove(jobData);
            _currentJobIndex--;
            updateLastUpdated();
            return true;
        }

        void updateLastUpdated()
        {
            _lastUpdated = DateTime.Now;
        }
        public void AddFromCollection(JobDataCollectionModel jobData)
        {
            Jobs.Clear();
            jobData.jobList.ForEach(jobData => { AddNewJob(jobData); });
        }
        public void UpdateJob(int index, JobDataModel data)
        {
            Trace.WriteLine($"Job Data Changed to  {data.displayName},{ data.latestRun.result} ");
            Jobs[index].jobData = data;            
            CurrentJobChanged?.Invoke();
        }
        public void UpdateCurrentJob(JobDataModel data)
        {
            UpdateJob(_currentJobIndex, data);
            _selectedJobDataStore.SelectedJob = Jobs[_currentJobIndex].jobData;
            CurrentJobChanged?.Invoke();
        }
        public void UpdateCurrentJob(string selectorColor)
        {            
            _selectedJobDataStore.SelectedJob = Jobs[_currentJobIndex].jobData;
            CurrentJobChanged?.Invoke();
        }

        public event Action CurrentJobChanged;
    }
}
