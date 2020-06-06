using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases {
    public class PersonData {
        public static Task<PersonData> GetInstanceAsync() {
            var instance = new PersonData();

            return instance.InitAsync();
        }

        private async Task<PersonData> InitAsync() {
            await Task.Delay(1000);
            Name = "some name";
            Surname = "some surname";

            return this;
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        private PersonData() { }
    }

    public interface IAsyncInit {
        Task Initialization { get; }
    }

    public class AsyncInit : IAsyncInit {
        public Task Initialization { get; }
        public string State { get; set; }

        public AsyncInit() {
            Initialization = InitizalizeAsync();
        }

        private async Task InitizalizeAsync() {
            await Task.Delay(2000);
            State = "Some state";
        }

    }

    public class AsyncInitCase : ICase {
        public async Task RunAsync() {
            //await InitAsync();
            try {
                await ThrowException();
            } catch {
                var a = 1;
            }

            Console.WriteLine("End of the run async.");
        }

        private async Task InitAsync() {
            var instance = new AsyncInit();
            await Task.Delay(3000);
        }

        private async Task ThrowException() {
            //throw new Exception("From async void method.");
            await Task.FromException(new Exception("From async."));
        }
    }
}
