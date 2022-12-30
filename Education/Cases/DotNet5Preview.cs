using Education.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class DotNet5Preview : ICase
    {
        public Task RunAsync() {
            LinqMethods();

            return Task.CompletedTask;
        }

        private void Records() {
            // var predicate = (Person p) => p.Name is not null; 
            var header = new Header("title", "yauheni kors") { SupportContact = string.Empty };
            var header1 = new Header("title", "yauheni kors");
            var header2 = header with { SupportContact = "default" }; // use with to support immutability (creates new instance)

            Console.WriteLine(header == header1);
        }

        private void Operators() {
            var name = "zheka";

            if (name is { Length: > 5 }) {

            }

            var validNames = GetNames() is var names && names.Count() == 3;
        }

        private void Ranges() {
            Range itemsToHandle = 4..;
            var cars = GetCars();
            var grouped = cars.GroupBy(car => car.HP switch {
                <= 100 => "low",
                <= 200 => "mid",
                <= 300 => "high"
            });

            foreach (var group in grouped) {
                Console.WriteLine(group.Key);
                
                foreach (var car in group) {
                    Console.WriteLine(car.Name);
                }
                Console.WriteLine();
            }
        }

        private void Patterns() {
            var person = new Person();
            var isValidPerson = person switch {
                { Name: "Zheka" } => true,
                _ => false
            };

            var (age, exp) = (26, 5);

            var isItGood = (age, exp) switch {
                ( > 25, > 3) => true
            };
        }

        private void LinqMethods() {
            var a = 1;
            List<string> titles = new() {
                "zheka",
                "artsyom",
                "serega",
                "zana"
            };
            titles.Append("new");
            var lookup = titles.ToLookup(item => item.First(), item => item);
            var grouped = titles.GroupBy(s => s.First()).Select(group => new { key = group.Key, value = group.Select(g => g + " :added") });

            var guids = GetGuids();
            var paral = guids.AsParallel();
            var stopwatch = new Stopwatch();

            //foreach (var item in lookup) {
            //    Console.WriteLine($"{item.Key}");

            //    foreach (var element in item) {
            //        Console.WriteLine(element);
            //    }
            //}
        }

        private IEnumerable<string> GetGuids() {
            var count = (int)Math.Pow(10, 6);
            var guid = Guid.NewGuid().ToString();

            return Enumerable.Repeat(guid, count);
        }

        private IEnumerable<string> GetNames() => new string[] { "Zheka", "Archi", "Sergey" };

        private IEnumerable<Car> GetCars() =>
            new Car[] {
                new() { Name = "Audi", HP = 243 },
                new() { Name = "BMW", HP = 123 },
                new() { Name = "Mercedes", HP = 255 },
                new() { Name = "Geely", HP = 92 },
                new() { Name = "Lamba", HP = 255 },
            };
        public class Car
        {
            public string Name { get; set; }
            public int HP { get; set; }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public record Header(string Title, string FullName)
    {
        public string SupportContact { get; init; }
    } // immutable, record needs for work with immutable data
    //public record Header
    //{
    //    public string Title { get; set; } = default!;
    //    public string FullName { get; set; } = default!;
    //}
}
