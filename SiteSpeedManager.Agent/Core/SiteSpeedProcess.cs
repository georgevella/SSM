using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SiteSpeedManager.Agent.Core
{
    public class SiteSpeedProcess : ISiteSpeedProcess
    {
        private readonly Process _process;
        private bool _neverStarted = true;

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

        public SiteSpeedProcess()
        {
            _process = new Process()
            {
                StartInfo = new ProcessStartInfo("D:\\tools\\procexp.exe"),
                EnableRaisingEvents = true,
            };

            _process.Exited += ProcessOnExited;
        }

        private void ProcessOnExited(object sender, EventArgs eventArgs)
        {

        }

        public async Task Run()
        {
            _neverStarted = false;
            await Task.Run(() => _process.Start());
        }
    }

    public interface ISiteSpeedProcess
    {
        bool IsRunning { get; }
        Task Run();
    }
}