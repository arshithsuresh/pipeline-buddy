using PipelineBuddy.Services;
using PipelineBuddyView.ViewModel;
using PipelineBuddyView.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace PipelineBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private MainViewModel _viewModel;
        private bool addNewJobWindowOpen = false;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = App.GetService<MainViewModel>();

            this.DataContext= _viewModel;
        }

       public void GetData() {          
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        public void onRefreshButton(object sender, EventArgs e)
        {
            _viewModel.RefreshCurrentJob();
        }
        public void onNextButton(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Jobs", _viewModel.Jobs.Count.ToString());
            _viewModel.currentJobDataIndex += 1;
            Trace.WriteLine("Current " + _viewModel.currentJobDataIndex.ToString(), _viewModel.currentJobColor.ToString());
        }

        public void onPreviousButton(object sender, RoutedEventArgs e)
        {
            _viewModel.currentJobDataIndex -= 1;
            Trace.WriteLine("Current " + _viewModel.currentJobDataIndex.ToString(), _viewModel.currentJobColor.ToString());

        }       

        private void NextBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (addNewJobWindowOpen) return;

            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                AddNewJobWindow jobWindow = new AddNewJobWindow();
                var mainWindow = Window.GetWindow(this);
                var location = new Point(5, -14);
                var width = System.Windows.SystemParameters.WorkArea.Width;
                jobWindow.Left = mainWindow.Left + mainWindow.Width/2 - jobWindow.Width/2;
                jobWindow.Top = mainWindow.Top + mainWindow.Height + location.Y;
                
                jobWindow.Show();
                addNewJobWindowOpen = true;
                jobWindow.Closed += handleAddNewWindowClosed;
            }
        }

        private void PrevBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                _viewModel.DeleteCurrentJob();
            }
        }

        private void handleAddNewWindowClosed(object? sender, EventArgs e)
        {
            addNewJobWindowOpen = false;
        }

        private void JobWatchList_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Add_WatchList_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddCurrentToWatchlist();
        }

        protected override void OnClosed(EventArgs e)
        {
            _viewModel.SaveCurrentJobs();
            base.OnClosed(e);
        }

        private void JobButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenGHE();
        }
    }
}
