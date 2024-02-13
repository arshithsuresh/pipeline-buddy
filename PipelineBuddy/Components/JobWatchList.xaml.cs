using Contracts.Models;
using PipelineBuddy;
using PipelineBuddy.Models;
using PipelineBuddyView.Store;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace PipelineBuddyView.Components
{

    public partial class JobWatchList : UserControl, INotifyPropertyChanged
    {
        private readonly AllJobDataStore allJobDataStore;
        public JobDataModel watchListJob { get; set; }
        public ObservableCollection<int> watchListJobs => allJobDataStore.WatchListedJobs;
         

        public JobWatchList()
        {
            this.Visibility = System.Windows.Visibility.Hidden;

            allJobDataStore = App.GetService<AllJobDataStore>();            
            InitializeComponent();
            this.DataContext = this;

            watchListJobs.CollectionChanged += WatchListChanged;
        }


        private void WatchListChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (!this.IsVisible)
                {
                    this.Visibility = System.Windows.Visibility.Visible;
                }

                var addedJobIndex = int.Parse(e.NewItems[0].ToString());
                Trace.WriteLine($"New WatchList {addedJobIndex}");

                WatchListButton newJob = new WatchListButton(addedJobIndex);
                allJobDataStore.CurrentJobChanged += newJob.JobChanged;
                newJob.MouseDown += NewJob_MouseDown;


                Trace.WriteLine($"JOB DATA :: {newJob.JobData.name}, {allJobDataStore.Jobs[addedJobIndex].jobData.name}");
                WatchList.Children.Add(newJob);

            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (this.IsVisible && watchListJobs.Count == 0)
                {
                    this.Visibility = System.Windows.Visibility.Hidden;
                }
                var removedIndex = e.OldStartingIndex;
                Trace.WriteLine($"Removed WatchList {removedIndex}");
                WatchList.Children.RemoveAt(removedIndex);
            }
        }

        private void NewJob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {              
                Trace.WriteLine($"JOB CLICKED ::  {sender}  {nameof(sender)}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void WrapPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }
    }


}
