using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineBuddy.Models
{
    public  class PRDataModel
    {
        public string author { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        public PRDataModel(string author, string id, string title, string url)
        { 
            this.author = author;
            this.id = id;
            this.title = title;
            this.url = url;
        }

    }
}
