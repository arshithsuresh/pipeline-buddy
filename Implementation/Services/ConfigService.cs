using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineBuddy.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using PipelineBuddy.Models;
using Microsoft.Extensions.Configuration;

namespace Implementation.Services
{
    public class ConfigService : IConfigService
    {
        readonly string configFileName;

        ConfigModel _currentConfig;

        public ConfigModel currentConfig { get { return _currentConfig;  } }

        public ConfigService(ConfigModel defaultConfig, IConfiguration configRoot) {
            
            configFileName = configRoot.GetSection("config").Value;
            ArgumentNullException.ThrowIfNull(nameof (configFileName));
            if (checkIfConfigExits(configFileName))
            {
                _currentConfig = getConfigFromFile(configFileName);
            }
            else
            {
                createNewConfigFile(configFileName, defaultConfig);
                _currentConfig = defaultConfig;
            }
            
        }
        
        public Dictionary<string,string> getOrganizations() {
            Dictionary<string,string> orgs = new Dictionary<string,string>();
            foreach (KeyValuePair<string, Organization> org in _currentConfig.organizations)
            {
                orgs.Add(org.Key, org.Value.displayName);
            }
            return orgs;
        }

        public Organization getOrganizationData(string orgName)
        {
            if(_currentConfig.organizations.ContainsKey(orgName))
            return _currentConfig.organizations[orgName];

            throw new Exception("No Organization found! " + orgName);
        }
        public void createNewConfigFile(string path, ConfigModel defaultConfig)
        {
            string JSON = JsonSerializer.Serialize(defaultConfig);
            var logFile = System.IO.File.Create(path);
            var logWriter = new System.IO.StreamWriter(logFile);
            logWriter.Write(JSON);
            logWriter.Dispose();

            Console.WriteLine(logFile.Name);
        }

        public ConfigModel getConfigFromFile(string path)
        {            
            var options = new JsonSerializerOptions { IncludeFields = true };
            string configData = System.IO.File.ReadAllText(configFileName);
            ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(configData, options);

            return config;
        }

        public bool checkIfConfigExits(string  configFilePath) {
            return System.IO.File.Exists(configFilePath);
        }

        public void getJobConfig()
        {
            throw new NotImplementedException();
        }

      
    }
}
