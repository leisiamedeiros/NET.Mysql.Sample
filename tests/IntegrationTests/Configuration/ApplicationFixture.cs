using AutoFixture;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.IntegrationTests.Configuration
{
    public class ApplicationFixture : Fixture, IDisposable
    {
        public readonly HttpClient _client;

        public ApplicationFixture()
        {
            var jsonfilepath = $"{Directory.GetCurrentDirectory()}/appsettings.test.json";

            IWebHostBuilder builder = WebHost.CreateDefaultBuilder()
                                                .UseStartup<Startup>()
                                                .ConfigureAppConfiguration((hostingContext, config) =>
                                                {
                                                    config.AddJsonFile(jsonfilepath, false, true);
                                                });

            var server = new TestServer(builder);
            _client = server.CreateClient();
        }

        public async Task<HttpResponseMessage> SendPostAsync<T>(T command, string relativePath)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_client.BaseAddress, ""),
                Content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json")
            };

            return await _client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetAsync(string relativePath)
        {
            return await _client.GetAsync(relativePath);
        }

        public async Task<T> ReadResponseMessageAsync<T>(HttpResponseMessage message)
        {
            var result = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }
    }
}
