using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Command
{
    public class CommandCase : ICase
    {
        public async Task RunAsync() {
            var zheka = new Person { Name = "zheka" };
            var artsom = new Person { Name = "artsiom" };
            var personRepository = new PersonRepository();
            ISavePersonCommand savePersonCommand = new SavePersonCommand(personRepository);
            IShowAllPersonsCommand showAllPersonsCommand = new ShowAllPersonsCommand(personRepository);

            savePersonCommand.Execute(zheka);
            showAllPersonsCommand.Execute();
            savePersonCommand.Execute(artsom);
            showAllPersonsCommand.Execute();
        }

        public abstract class BaseEntity
        {
            public int Id { get; set; }
        }

        public class Person : BaseEntity
        {
            public string Name { get; set; }
        }

        public interface IRepository<T> where T : BaseEntity
        {
            void Save(T item);
            void ShowAll();
        }

        public abstract class Repository<T> : IRepository<T> where T : BaseEntity
        {
            protected readonly List<T> storage = new List<T>();

            public virtual void Save(T item) {
                if (!storage.Select(s => s.Id).Contains(item.Id)) {
                    item.Id = storage.Any() ? storage.Max(s => s.Id) + 1 : 1;
                    storage.Add(item);
                    return;
                }

                throw new ArgumentException(nameof(item), "Object is already exist");
            }

            public virtual void ShowAll() {
                storage.ForEach(item => Console.WriteLine(item.Id));
            }
        }

        public class PersonRepository : Repository<Person>
        {
            public override void Save(Person item) {
                base.Save(item);
                Console.WriteLine($"Person with name '{item.Name}' and id '{item.Id}' successfully saved.");
            }

            public override void ShowAll() {
                storage.ForEach(item => Console.WriteLine($"{item.Id} {item.Name}"));
            }
        }

        public interface ICommand { }

        public interface ISavePersonCommand : ICommand
        {
            void Execute(Person person);
        }

        public interface IShowAllPersonsCommand : ICommand
        {
            void Execute();
        }

        public class SavePersonCommand : ISavePersonCommand
        {
            private readonly IRepository<Person> personRepository;

            public SavePersonCommand(IRepository<Person> personRepository) {
                this.personRepository = personRepository;
            }

            public void Execute(Person person) {
                if (person == null) throw new ArgumentNullException(nameof(person));

                personRepository.Save(person);
            }
        }

        public class ShowAllPersonsCommand : IShowAllPersonsCommand
        {
            private readonly IRepository<Person> personRepository;

            public ShowAllPersonsCommand(IRepository<Person> personRepository) {
                this.personRepository = personRepository;
            }

            public void Execute() {
                personRepository.ShowAll();
            }
        }
    }
}
