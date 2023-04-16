using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.DotNet.CSharp9Case
{
    public class CSharp9Case : ICase {
        public async Task RunAsync() {
            InvokeRecords();
        }

        private void InvokeRecords() {
            var stack = new Stack<int>();
            var person = new Person("zheka", "lol");
            var secondPerson = new Person("zheka", "lol");
            Console.WriteLine(person == secondPerson);
            Console.WriteLine(ReferenceEquals(person, secondPerson));
            var newRecord = person with { PhoneNumber = "123" };
            Console.WriteLine(person == secondPerson);
            Console.WriteLine(ReferenceEquals(person, secondPerson));
        }
    }

    public record Person(string Name, string Surname) {
        public string PhoneNumber { get; init; }
    }
}
