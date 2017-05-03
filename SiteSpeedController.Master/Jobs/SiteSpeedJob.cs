using System.Threading.Tasks;
using NLog;
using Quartz;

namespace SiteSpeedController.Master.Jobs
{
    public class SiteSpeedJob : IJob
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public async Task Execute(IJobExecutionContext context)
        {
            Log.Info("Executing job ...");
            // todo
            await Task.Delay(500);
        }
    }
}