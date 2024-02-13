using Contracts.Models;
using Microsoft.Extensions.Configuration;
using PipelineBuddy.Models;
using PipelineBuddy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Implementation.Services
{
    public class JobDataService : IJobDataService
    {
        private IHttpService _httpService;
        private IConfigService _configService;
        private string jobDataFileName = "jobsList.json";

        public JobDataService(IHttpService httpService, IConfigService configService, IConfiguration configRoot)
        {            
            _httpService = httpService;
            _configService = configService;
            jobDataFileName = configRoot.GetSection("jobs").Value;
        }        
        public async Task<JobDataModel> fetchJobData(string jobId, string organization)
        {
            var organizationData = _configService.getOrganizationData(organization);
            var jobURL = (organizationData.JenkinsRootLink).Replace("$ID", jobId);            
            var result =  await _httpService.fetchJsonData<JobDataModel>(jobURL);           
            
            return result;
        }
        public bool JobDataFileExits(string jobDataFile)
        {
            return System.IO.File.Exists(jobDataFile);
        }       
        public void createJobDataFile(List<JobStorageModel> jobs) {

            var newJobData = new JobDataCollectionModel(DateTime.Now, jobs);
            string JSON = JsonSerializer.Serialize(newJobData);
            var dataFile = System.IO.File.Create(jobDataFileName);
            var dataWriter = new System.IO.StreamWriter(dataFile);
            dataWriter.Write(JSON);
            dataWriter.Dispose();
        }
        public void createJobDataFile() {
            if (JobDataFileExits(jobDataFileName))
                return;

            var newJobData = new JobDataCollectionModel(DateTime.Now, new List<JobStorageModel>());           
            
            string JSON = JsonSerializer.Serialize(newJobData);
            var dataFile = System.IO.File.Create(jobDataFileName);
            var dataWriter = new System.IO.StreamWriter(dataFile);
            dataWriter.Write(JSON);
            dataWriter.Dispose();
        }
        public JobDataCollectionModel readJobDataFile() {
            createJobDataFile();

            var options = new JsonSerializerOptions { IncludeFields = true };
            string jobData = System.IO.File.ReadAllText(jobDataFileName);
            JobDataCollectionModel jobDataCollection = JsonSerializer.Deserialize<JobDataCollectionModel>(jobData, options);

            return jobDataCollection;
        }

        public void getJobName()
        {
            throw new NotImplementedException();
        }

        public void getJobPRLink()
        {
            throw new NotImplementedException();
        }

        public void getJobStatus()
        {
            throw new NotImplementedException();
        }
    }
}
