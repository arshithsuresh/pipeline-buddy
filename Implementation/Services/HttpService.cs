using PipelineBuddy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Contracts.Models;

namespace Implementation.Services
{
    public class HttpService : IHttpService
    {
        public async Task<T> fetchJsonData<T>(string url)
        {
            var JSONObject = default(T);
            using (var _httpClient = new HttpClient())
            {
                try
                {
                    var options = new JsonSerializerOptions { IncludeFields = true };
                    var response = await _httpClient.GetStringAsync(url);
                    JSONObject = JsonSerializer.Deserialize<T>(response, options);
                }
                catch (Exception ex)
                {

                }
            }

            return JSONObject!;
        }

    }
}
