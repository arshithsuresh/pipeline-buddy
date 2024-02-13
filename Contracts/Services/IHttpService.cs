﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PipelineBuddy.Services
{
    public interface IHttpService
    {
        Task<T> fetchJsonData<T>(string url);
    }
}
