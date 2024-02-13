using Contracts.Models;
using PipelineBuddy;
using PipelineBuddyView.Store;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PipelineBuddyView.Components
{
    public partial class WatchListButton : UserControl, INotifyPropertyChanged
    {
        private int _jobDatIndex;

        public int JobDatIndex 
        { 
            get => _jobDatIndex;
            set {
                _jobDatIndex = value;                
                JobChanged();
            } 
        }

        public JobDataModel JobData;

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly AllJobDataStore _jobDataStore;

        public string color
        {
            get
            {
                if (JobData != null)
                    return JobData.color;

                return "#C5D0D8";
            }
        }
        public WatchListButton(int index)
        {
            _jobDataStore = App.GetService<AllJobDataStore>();            
            InitializeComponent();
            JobDatIndex = index;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine($"Change Job to {JobDatIndex}");
            _jobDataStore.currentJobIndex = JobDatIndex;
        }

        public void JobChanged()
        {
            Trace.WriteLine("Changing Job Color ");
            JobData = _jobDataStore.Jobs[JobDatIndex].jobData;
            RaisePropertyChanged(nameof(color));
        }

        protected void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private static void ValueChangedCallback(DependencyObject obj,
           DependencyPropertyChangedEventArgs args)
        {
            WatchListButton button = obj as WatchListButton;
            Trace.WriteLine("VALUE CHANGED");

        }

        private void BtnWatchList_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            { 
                _jobDataStore.RemoveWatchList(JobDatIndex);
            }
            
        }
    }
}
