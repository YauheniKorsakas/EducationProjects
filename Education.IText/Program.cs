using Education.Core;
using Education.IText.Cases;
using System;
using System.Threading.Tasks;

namespace Education.IText {
    class Program {
        static async Task Main(string[] args) {
            // await CaseRunner.RunCaseAsync<ParseCase>();
            DbActions(async () => {
                await Task.Delay(3000);
                Console.WriteLine("End of the action.");
            });

            Console.WriteLine("End of the main.");
            Console.ReadKey();
        }

        static Task DbActions(Action action) {
            Console.WriteLine($"{nameof(DbActions)} has been started.");
            action?.Invoke();
            Console.WriteLine($"{nameof(DbActions)} has been completed.");

            return Task.CompletedTask;
        }
    }
}
