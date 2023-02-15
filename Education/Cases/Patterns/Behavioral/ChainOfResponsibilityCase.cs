using Education.Core;
using System.Threading.Tasks;
using System;

namespace Education.Cases.Patterns.Behavioral.ChainOfResponsibility
{
    public class ChainOfResponsibilityCase : ICase
    {
        public async Task RunAsync() {
            var personLoan = new PersonLoanRequest(1, 150000);
            ILoanRequestValidator loanRequestValidator = new PersonIdLoanRequestValidator();
            loanRequestValidator.SetNextValidator(new MoneyAmountLoanRequestValidator());
            loanRequestValidator.Validate(personLoan);
        }

        public class PersonLoanRequest
        {
            public int PersonId { get; set; }
            public int MoneyToLoan { get; set; }

            public PersonLoanRequest(int personId, int moneyToLoan) {
                PersonId = personId;
                MoneyToLoan = moneyToLoan;
            }
        }

        public interface ILoanRequestValidator
        {
            void Validate(PersonLoanRequest personLoan);
            void SetNextValidator(ILoanRequestValidator nextValidator);
        }

        public abstract class BaseLoanRequestValidator : ILoanRequestValidator
        {
            protected ILoanRequestValidator nextValidator;

            public abstract void Validate(PersonLoanRequest personLoan);

            public void SetNextValidator(ILoanRequestValidator nextValidator) {
                this.nextValidator = nextValidator;
            }
        }

        public class PersonIdLoanRequestValidator : BaseLoanRequestValidator, ILoanRequestValidator
        {
            public override void Validate(PersonLoanRequest personLoanRequest) {
                Console.WriteLine($"Checking person id ({personLoanRequest.PersonId}) for existance in system...");
                Console.WriteLine($"Person id ({personLoanRequest.PersonId}) does exist.");

                nextValidator?.Validate(personLoanRequest);
            }
        }

        public class MoneyAmountLoanRequestValidator : BaseLoanRequestValidator, ILoanRequestValidator
        {
            private int MaxAmountOfMoneyToLoan = 10000;

            public override void Validate(PersonLoanRequest personLoanRequest) {
                Console.WriteLine($"Checking money amount ({personLoanRequest.MoneyToLoan}) for validity according to loan plan...");

                if (personLoanRequest.MoneyToLoan > MaxAmountOfMoneyToLoan) {
                    throw new BusinessException($"Requested money to loan ({personLoanRequest.MoneyToLoan}) is greater than max amount ({MaxAmountOfMoneyToLoan}).");
                }
                Console.WriteLine($"Person id ({personLoanRequest.PersonId}) does exist.");

                nextValidator?.Validate(personLoanRequest);
            }
        }

        public class BusinessException : Exception
        {
            public BusinessException(string message) : base(message) { }
        }
    }
}
