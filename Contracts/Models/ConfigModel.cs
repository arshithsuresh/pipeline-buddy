using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Models
{
    public class ConfigModel
    {
        public string username { get; set; }
        public Dictionary<string,Organization> organizations { get; set; }

        public ConfigModel(string username, Dictionary<string, Organization> organizations) {
            this.username = username;
            this.organizations = organizations;                
        }
    }

    public class Organization
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string GHERootLink { get; set; }
        public string JenkinsRootLink { get; set; } 
        public List<Action> actions { get; set; }

        public Organization(string name, string displayName, string GHERootLink, string jenkinsRootLink, List<Action> actions = null) {
            this.name = name;
            this.GHERootLink = GHERootLink;
            this.JenkinsRootLink = jenkinsRootLink;
            this.displayName = displayName;
            this.actions = actions;
        }
    }

    public class Actions {
        public string displayName { get; set; }
    }
}
