using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class SyncContextAndConfigureAwaitCase : ICase
    {
        public async Task RunAsync() {
            var awaitablePerson = new AwaitablePerson();
            var result = await awaitablePerson;
            Console.WriteLine(result);
            AwaitablePerson? prsn;
            prsn = null;
            var result1 = GetString(null);
        }

        private string GetString(string str) {
            return str;
        }
    }

    public class AwaitablePerson
    {
        public PersonAwaiter GetAwaiter() {
            return new PersonAwaiter();
        }
    }

    public class PersonAwaiter : INotifyCompletion // Anything can be awaited. Async keyword uses state machine under the hood,
        // to let program understand how to deal with awaitable constructions.
    {
        public bool IsCompleted { get => true; }

        public void OnCompleted(Action continuation) {
            Console.WriteLine("Task completed.");
        }

        public int GetResult() => 1;
    }
}
