using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.TemplateMethod
{
    public class TemplateMethodCase : ICase
    {
        public async Task RunAsync() {
            var personStorage = new PersonStorage();
            var carStorage = new CarStorage();
            var okPerson = new Person {
                Id = 1,
                Name = "zheka"
            };
            var notOkPerson = new Person {
                Id = 2,
                Name = "lol"
            };
            var okCar = new Car {
                Id = 1,
                Name = "nissan",
                Weight = 600
            };
            var notOkCar = new Car {
                Id = 2,
                Name = "bmw",
                Weight = 1001
            };

            // personStorage.SaveData(notOkPerson);
            personStorage.SaveData(okPerson);
            Console.WriteLine("\n");
            carStorage.SaveData(okCar);
            Console.WriteLine("\n");
            carStorage.SaveData(notOkCar);
        }
    }


    public interface IDataStorage<T>
    {
        void SaveData(T data);
    }

    public abstract class BaseData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() {
            return $"id = {Id}, name = {Name}";
        }
    }

    public class Person : BaseData { }

    public class Car : BaseData
    {
        public int Weight { get; set; }
    }

    public abstract class BaseDataStorage<T> : IDataStorage<T> where T : BaseData
    {
        protected readonly List<T> storage = new List<T>();

        public void SaveData(T data) {
            ValidateDataForSave(data);
            SaveDataInternal(data);
            LogSavedData(data);
        }

        protected void ValidateDataForSave(T data) {
            Console.WriteLine("Data validation...");

            if (data is null || storage.Contains(data) || storage.Select(item => item.Id).Contains(data.Id)) {
                throw new ArgumentException("Invalid data.");
            }

            ValidateDataForSaveInternal(data);
            Console.WriteLine("Data validation was ok.");
        }

        protected virtual void ValidateDataForSaveInternal(T data) { }
        protected abstract void SaveDataInternal(T data);
        protected abstract void LogSavedData(T data);
    }

    public class PersonStorage : BaseDataStorage<Person>
    {
        private readonly string[] shittyNames = new string[] { "Lol", "Idiot" };

        protected override void LogSavedData(Person data) {
            Console.WriteLine($"Saved person data: {data}");
        }

        protected override void SaveDataInternal(Person data) {
            Console.WriteLine($"Saving person...");
            storage.Add(data);
        }

        protected override void ValidateDataForSaveInternal(Person data) {
            if (shittyNames.Any(item => item.Equals(data.Name, StringComparison.InvariantCultureIgnoreCase))) {
                throw new ArgumentException($"Data to save contains unacceptable name");
            }
        }
    }

    public class CarStorage : BaseDataStorage<Car>
    {
        protected override void LogSavedData(Car data) {
            Console.WriteLine($"Saved car data: {data}");
        }

        protected override void SaveDataInternal(Car data) {
            Console.WriteLine($"Saving car...");
            storage.Add(data);
        }

        protected override void ValidateDataForSaveInternal(Car data) {
            _ = data.Weight switch {
                < 0 or > 1000 => throw new ArgumentException("Car weight is incorrect."),
                _ => data.Weight
            };
        }
    }
}
