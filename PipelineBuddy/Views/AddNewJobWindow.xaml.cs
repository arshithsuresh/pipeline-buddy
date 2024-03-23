using Implementation.Services;
using Microsoft.Extensions.DependencyInjection;
using PipelineBuddy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PipelineBuddy;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using PipelineBuddyView.Config;
using System.ComponentModel;
using PipelineBuddyView.ViewModel;
using System.Text.RegularExpressions;

namespace PipelineBuddyView.Views
{
    /// <summary>
    /// Interaction logic for AddNewJobWindow.xaml
    /// </summary>
    public partial class AddNewJobWindow : Window
    {
        private AddNewViewModel viewModel;

        public AddNewJobWindow()
        {           
            InitializeComponent();

            viewModel = new AddNewViewModel();
            this.DataContext = viewModel;

            Initialize();
        }

        public void Initialize()
        {
            OrgList.ItemsSource = viewModel.organizations;
        }
        
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string orgName = OrgList.SelectedValue == null ? "" : OrgList.SelectedValue.ToString();
            string jobID = JobId.Text;

            if (orgName == "" || !Regex.IsMatch(jobID, "PR-([0-9]*)"))
            {
                MessageBox.Show("Enter PR ID and Organization correctly", "Alert", MessageBoxButton.OK);
                return;
            }

            var nickName = txtNickname.Text.Trim();

            if (nickName.Length > 0)
            {
                viewModel.AddNewJob(jobID, orgName, nickName);
            }
            else
            {
                viewModel.AddNewJob(jobID, orgName);
            }
            Close();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

    }
}
