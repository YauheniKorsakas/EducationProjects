using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Education.Cases {
    public class MiscCase : ICase
    {
        public Task RunAsync() {
            Person.Count++;
            Console.WriteLine(Employee.Count);

            return Task.CompletedTask;
        }
    }

    public class Person
    {
        public static int Count { get; set; } = 1;
    }

    public class Employee : Person
    {

    }
}
