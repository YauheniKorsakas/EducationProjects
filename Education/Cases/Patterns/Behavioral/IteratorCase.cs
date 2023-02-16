using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Iterator
{
    public class IteratorCase : ICase
    {
        public async Task RunAsync() {
            ConcreteCollection collection = new ConcreteCollection();
            collection.AddEmployee(new Employee("Anurag", 100));
            collection.AddEmployee(new Employee("Pranaya", 101));
            collection.AddEmployee(new Employee("Santosh", 102));
            collection.AddEmployee(new Employee("Priyanka", 103));
            collection.AddEmployee(new Employee("Abinash", 104));
            collection.AddEmployee(new Employee("Preety", 105));

            Iterator iterator = new Iterator(collection);
            iterator.Step = 1;

            for (var empl = iterator.First(); !iterator.IsCompleted; empl = iterator.Next()) {
                Console.WriteLine($"{empl.Id} {empl.Name}");
            }
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Employee(string name, int id) {
                Name = name;
                Id = id;
            }
        }

        public interface AbstractIterator
        {
            int Step { get; set; }
            Employee First();
            Employee Next();
            bool IsCompleted { get; }
        }

        public class Iterator : AbstractIterator
        {
            private ConcreteCollection collection;
            private int current = 0;
            public int Step { get; set; } = 1;

            public Iterator(ConcreteCollection collection) {
                this.collection = collection;
            }

            public Employee First() {
                current = 0;
                return collection.GetEmployee(current);
            }

            public Employee Next() {
                current += Step;
                if (!IsCompleted) {
                    return collection.GetEmployee(current);
                }
                else {
                    return null;
                }
            }

            public bool IsCompleted {
                get { return current >= collection.Count; }
            }
        }

        public  interface AbstractCollection
        {
            Iterator CreateIterator();
        }

        public class ConcreteCollection : AbstractCollection
        {
            private readonly List<Employee> listEmployees = new List<Employee>();

            public Iterator CreateIterator() {
                return new Iterator(this);
            }

            public int Count => listEmployees.Count;

            public void AddEmployee(Employee employee) {
                listEmployees.Add(employee);
            }

            public Employee GetEmployee(int IndexPosition) {
                return listEmployees[IndexPosition];
            }
        }
    }
}
