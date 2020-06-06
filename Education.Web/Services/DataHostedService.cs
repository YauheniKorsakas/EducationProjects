using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Web.Services {
    public class DataHostedService : IHostedService {
        private static int _triggeredTimes = 0;

        public DataHostedService(IHostApplicationLifetime hostApplicationLifetime, IHostLifetime hostLifetime) {

        }

        public Task StartAsync(CancellationToken cancellationToken) {
            var timer = new Timer(
                (state) => {
                    _triggeredTimes++;
                }, null, 0, 5000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}
