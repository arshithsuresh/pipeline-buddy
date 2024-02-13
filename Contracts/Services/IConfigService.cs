using PipelineBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Services
{
    public interface IConfigService
    {
        public ConfigModel currentConfig {  get; }
        protected ConfigModel getConfigFromFile(string path);
        protected void createNewConfigFile(string path, ConfigModel defaultConfig);
        void getJobConfig();

        Organization getOrganizationData(string orgName);
        Dictionary<string, string> getOrganizations();



    }
}
