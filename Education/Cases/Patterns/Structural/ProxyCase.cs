using Education.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Proxy
{
    public class ProxyCase : ICase {
        public async Task RunAsync() {
            IHttpGetRequestOperation getOperation = new GodelVpnHttpGetRequestsOperation();
            getOperation.Get("http://twitter.com");
            getOperation.Get("http://lols.com");
        }

        public interface IHttpGetRequestOperation {
            void Get(string url);
        }

        public class HttpGetRequestOperation : IHttpGetRequestOperation {
            public void Get(string url) {
                Console.WriteLine($"Performing http request operation by url {url}...");
                Console.WriteLine($"Result of http get for {url} is: Success");
            }
        }

        public class GodelVpnHttpGetRequestsOperation : IHttpGetRequestOperation
        {
            private readonly string[] forbiddenDomains = new string[] { "twitter.com", "youtube.com" };

            public void Get(string url) {
                if (string.IsNullOrEmpty(url)) throw new ArgumentException(nameof(url));

                if (forbiddenDomains.Any(domain => url.Contains(domain, StringComparison.InvariantCultureIgnoreCase))) {
                    Console.WriteLine($"You cannot perform get operation for {url}");
                    return;
                }

                var operation = new HttpGetRequestOperation();
                operation.Get(url);
            }
        }
    }
}
