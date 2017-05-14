using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Glyde.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using SiteSpeedManager.Models.SiteSpeed;
using SiteSpeedManager.Transport;

namespace SiteSpeedManager.Agent.Services
{
    public class SiteSpeedProcess : ISiteSpeedProcess
    {
        private readonly IConfigurationService _configurationService;
        private readonly ILogger _logger;
        private readonly Process _process;
        private bool _neverStarted = true;
        private readonly JsonSerializer _serializer;

        public bool IsRunning
        {
            get
            {
                if (_neverStarted)
                    return false;

                _process.Refresh();
                return !_process.HasExited;
            }
        }

        public SiteSpeedProcess(IConfigurationService configurationService, ILogger logger)
        {
            _serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters =
                {
                    new StringEnumConverter(),
                },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });

            _configurationService = configurationService;
            _logger = logger;
            _process = new Process()
            {
                StartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Nodist\bin\sitespeed.io.cmd")
                {
                    UseShellExecute = false,
                },
            };
        }

        public void Run(SiteSpeedJobDetails jobDetails)
        {
            var tempPath = Path.GetTempPath();
            var filename = $"sitespeed-{Guid.NewGuid()}.json";
            var tempFile = Path.Combine(tempPath, filename);

            using (var file = new FileStream(
                tempFile,
                FileMode.OpenOrCreate,
                FileAccess.Write,
                FileShare.ReadWrite,
                4096,
                FileOptions.DeleteOnClose | FileOptions.RandomAccess))
            using (var sr = new StreamWriter(file))
            using (var tw = new JsonTextWriter(sr))
            {
                _logger.Info($"Dumping sitespeed configuration to [{tempFile}]");
                _serializer.Serialize(tw, jobDetails.Settings);

                tw.Flush();

                _neverStarted = false;
                _process.StartInfo.Arguments = $"--config {tempFile} {jobDetails.Uri}";

                _logger.Debug($"Starting sitespeedio with arguments [{_process.StartInfo.Arguments}]");
                _process.Start();

                _process.WaitForExit();
            }
        }
    }

    public interface ISiteSpeedProcess
    {
        bool IsRunning { get; }
        void Run(SiteSpeedJobDetails jobDetails);
    }
}