using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Adapter
{
    public class AdapterCase : ICase
    {
        public async Task RunAsync() {
            IStringValidator stringValidator = new StringValidator();
            IPersonValidator personValidator = new PersonValidator(stringValidator);
            var savePersonOperation = new SavePersonOperation(personValidator);
            var invalidPerson = new Person {
                Name = null,
                Surname = "Surname"
            };
            var validPerson = new Person {
                Name = "zheka",
                Surname = "born to work in microsoft"
            };
            savePersonOperation.Save(validPerson);
        }

        public class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        public interface IStringValidator
        {
            void CheckIfStringIsValid(string toValidate);
        }

        public class StringValidator : IStringValidator
        {
            public void CheckIfStringIsValid(string toValidate) {
                if (String.IsNullOrEmpty(toValidate)) {
                    throw new ArgumentException("Input string is invalid.");
                }
            }
        }

        public class SavePersonOperation
        {
            private readonly IPersonValidator personValidator;

            public SavePersonOperation(IPersonValidator personValidator) {
                this.personValidator = personValidator;
            }

            public void Save(Person person) {
                personValidator.Validate(person);
                Console.WriteLine("Saving...");
                Console.WriteLine($"{person.GetType().Name} was validated and saved.");
            }
        }

        public interface IPersonValidator
        {
            void Validate(Person person);
        }

        public class PersonValidator : IPersonValidator
        {
            private readonly IStringValidator stringValidator;

            public PersonValidator(IStringValidator stringValidator) {
                this.stringValidator = stringValidator;
            }

            public void Validate(Person person) {
                _ = person ?? throw new ArgumentNullException(nameof(person));
                stringValidator.CheckIfStringIsValid(person.Name);
                stringValidator.CheckIfStringIsValid(person.Surname);
            }
        }
    }
}
