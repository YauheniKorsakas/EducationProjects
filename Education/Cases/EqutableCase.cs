using Education.Core;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Education.Cases {
    public class Car {
        public string Name { get; set; }
    }

    public class EqutableCase : ICase {
        public Task RunAsync() {
            var car1 = new Car();
            var car2 = new Car();

            var firstNumber = 1;
            var secondNumber = 2;

            var eq = firstNumber.Equals(1);
            var refeq = object.ReferenceEquals(firstNumber, firstNumber);

            return Task.CompletedTask;
        }
    }
}
