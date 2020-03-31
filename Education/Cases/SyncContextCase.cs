using System.Threading;
using System.Threading.Tasks;
using System;
using Education.Core;

namespace Education.Cases {
    public class SyncContextCase : ICase {
        public async Task RunAsync() {
            Console.WriteLine("Scene started.");
            await ConfigureAwaitScene().ContinueWith((task, o) => { }, SynchronizationContext.Current);
            Console.WriteLine("Scene ended.");
        }

        private void SyncContextScene() {
            var sc = SynchronizationContext.Current ?? new SynchronizationContext();

            ThreadPool.QueueUserWorkItem((s) => {
                sc.Post(async (p) => {
                    var currentContext = SynchronizationContext.Current;
                    Console.WriteLine(await IncrementAsync(1));
                }, null);
            });
        }

        private async Task ConfigureAwaitScene() {
            var person = await GetPersonAsync();
            Console.WriteLine($"Person: {person.ToString()}");
        }

        private Task<int> GetValueAsync(int value) {
            return Task.Run(async () => {
                await Task.Delay(2000);
                return value;
            });
        }

        private Task<int> IncrementAsync(int number) => Task.FromResult(++number);

        private Task<Person> GetPersonAsync() {
            return Task.Run(async () => {
                await Task.Delay(2000);
                return new Person { Id = 1 };
            });
        }
    }

    public class Person {
        public int Id { get; set; }

        public override string ToString() => $"Id: {Id}";
    }
}
