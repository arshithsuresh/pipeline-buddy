using PipelineBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddyView.Config
{
    public static class DefaultConfig
    {
        public static ConfigModel getDefaultConfig()
        {
            Dictionary<string, Organization> organizationList = new Dictionary<string, Organization>();
            organizationList.Add("reaplceName", new Organization("reaplceName","repalceDisplayName", "reaplceWithGitHubLink", "reaplceWithGitHubLink"));

            ConfigModel defaultConfig = new ConfigModel("reaplaceUser", organizationList);
            return defaultConfig;
        }
    }
}
