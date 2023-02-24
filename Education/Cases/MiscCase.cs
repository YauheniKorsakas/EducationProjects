using Education.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.Misc
{
    public class MiscCase : ICase
    {
        public async Task RunAsync() {
            var lazy = new Lazy<Person>(() => new Person(() => Console.WriteLine($"person created")) { Id = 1 });
            Console.WriteLine("Lazy var was created");
            Console.WriteLine(lazy.IsValueCreated);
            Console.WriteLine(lazy.Value.Id);
            Console.WriteLine("After lazy");
            Console.WriteLine();
        }
    }

    public class Person
    {
        public int Id { get; set; }

        public Person(Action creationCallback) {
            creationCallback?.Invoke();
        }
    }
}
