using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.State
{
    public class StateCase : ICase
    {
        public async Task RunAsync() {
            var atm = new ATM();
            atm.EjectDebitCard();
            atm.InsertDebitCard();
            atm.WithdrawMoney();
            atm.EnterPin();
            atm.WithdrawMoney();
            atm.EjectDebitCard();
        }

        public interface IAtm
        {
            IAtmState CurrentState { get; set; }
            void InsertDebitCard();
            void EjectDebitCard();
            void EnterPin();
            void WithdrawMoney();
        }

        public class ATM : IAtm
        {
            public IAtmState CurrentState { get; set; }

            public ATM() {
                CurrentState = new CardNotInsertedAtmState(this);
            }

            public void EjectDebitCard() {
                CurrentState.EjectDebitCard();
            }

            public void EnterPin() {
                CurrentState.EnterPin();
            }

            public void InsertDebitCard() {
                CurrentState.InsertDebitCard();
            }

            public void WithdrawMoney() {
                CurrentState.WithdrawMoney();
            }
        }

        public interface IAtmState
        {
            IAtm Atm { get; }
            void InsertDebitCard();
            void EjectDebitCard();
            void EnterPin();
            void WithdrawMoney();
        }

        public class CardNotInsertedAtmState : IAtmState
        {
            public IAtm Atm { get; }

            public CardNotInsertedAtmState(IAtm atm) {
                Atm = atm;
            }

            public void InsertDebitCard() {
                Console.WriteLine("Debit Card inserted");
                Atm.CurrentState = new CardInsertedAtmState(Atm);
                Console.WriteLine($"Atm state was changed to {typeof(CardInsertedAtmState).Name}");
            }

            public void EnterPin() {
                Console.WriteLine("You cannot enter the pin, as No Debit Card in ATM Machine slot");
            }

            public void EjectDebitCard() {
                Console.WriteLine("You cannot eject the Debit, as no Debit Card in ATM Machine slot");
            }

            public void WithdrawMoney() {
                Console.WriteLine("You cannot withdraw money, as No Debit Card in ATM Machine slot");
            }
        }

        public class CardInsertedAtmState : IAtmState
        {
            public IAtm Atm { get; }
            private bool pinEntered { get; set; } = false;

            public CardInsertedAtmState(IAtm atm) {
                Atm = atm;
            }

            public void EjectDebitCard() {
                Console.WriteLine("Debit Card ejected");
                Atm.CurrentState = new CardNotInsertedAtmState(Atm);
                Console.WriteLine($"Atm state was changed to {typeof(CardNotInsertedAtmState).Name}");
            }

            public void EnterPin() {
                pinEntered = true;
                Console.WriteLine("Pin number has been entered correctly");
            }

            public void InsertDebitCard() {
                Console.WriteLine("You cannot insert the Debit Card, as the Debit card is already there");
            }

            public void WithdrawMoney() {
                Console.WriteLine("Trying to withdraw money...");

                if (pinEntered) {
                    Console.WriteLine("Money has been withdrawn");
                } else {
                    Console.WriteLine("Enter pin first.");
                }
            }
        }
    }
}
