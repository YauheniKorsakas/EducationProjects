using Education.Cases.DotNet;
using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.SeniorPreparation
{
    public class Animal
    {
        public string Name { get; set; }
    }

    public class AnimalGiraffe : Animal
    {
        public string Surname { get; set; }
    }

    public class PatternMatching : ICase
    {
        private Person _person;
        public async Task RunAsync() {
            var animal = new Animal { Name = "source" };
            Check(animal);
        }

        private void DiscardPattern() {
            _person = new Person();

            var isValidPerson = _person switch {
                Person _ and { Age: 12 } => true,
                { Age: < 1 } => false,
                { Age: 1 or 2 or 3 } => true, 
                _ => false
            };
        }

        private void Check(Animal animal) {
            var isItValid = animal is Animal;
            animal = null;
        }
    }
}
