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

    public class AsyncInitCase : ICase {
        public async Task RunAsync() {
            var instance = PersonData.GetInstanceAsync().Status;

            Console.WriteLine("End of the run async.");
        }
    }
}
