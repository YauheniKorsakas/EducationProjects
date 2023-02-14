using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Facade
{
    public class FacadeCase : ICase
    {
        public async Task RunAsync() {
            var creditEmiter = new CreditEmmiter(new AbilityToPayValidator(), new MoneyActions(), new BankingServiceRegistrator());
            var person = new Person {
                Id = 1,
                Name = "Zheka"
            };
            var creditSum = 12345;
            creditEmiter.Emit(person, creditSum);
        }

        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class CreditEmmiter
        {
            private readonly IPersonValidator personValidator;
            private readonly IMoneyActions moneyActions;
            private readonly IBankingServiceRegistrator bankingServiceRegistrator;

            public CreditEmmiter(IPersonValidator personValidator, IMoneyActions moneyActions, IBankingServiceRegistrator bankingServiceRegistrator) {
                this.personValidator = personValidator;
                this.moneyActions = moneyActions;
                this.bankingServiceRegistrator = bankingServiceRegistrator;
            }

            public void Emit(Person person, int creditSum) {
                personValidator.Validate(person);
                moneyActions.ReserveInitialAmount(person, creditSum / 2);
                bankingServiceRegistrator.RegisterCredit(person, creditSum);
            }
        }

        public interface IPersonValidator
        {
            void Validate(Person person);
        }

        public interface IMoneyActions
        {
            void ReserveInitialAmount(Person person, int initialAmount);
        }

        public interface IBankingServiceRegistrator
        {
            void RegisterCredit(Person person, int sum);
        }

        public class AbilityToPayValidator : IPersonValidator
        {
            public void Validate(Person person) {
                Console.WriteLine("Ability to pay validation...");
                Console.WriteLine($"Client {person.Id}-{person.Name} is able to pay.");
            }
        }

        public class MoneyActions : IMoneyActions
        {
            public void ReserveInitialAmount(Person person, int initialAmount) {
                Console.WriteLine($"Initial amount of money ({initialAmount}) has been reserved.");
            }
        }

        public class BankingServiceRegistrator : IBankingServiceRegistrator
        {
            public void RegisterCredit(Person person, int sum) {
                Console.WriteLine($"Credit amount of {sum}$ has been issued for {person.Id}-{person.Name}.");
            }
        }

    }
}
