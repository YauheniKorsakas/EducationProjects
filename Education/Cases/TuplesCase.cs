using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases {
    public class TuplesCase : ICase {
        public Task RunAsync() {
            var (_, surname) = GetPersonData();

            return Task.CompletedTask;
        }

        private (string, string) GetPersonData() {
            return (n: "name", item123: "surname");
        }
    }
}
