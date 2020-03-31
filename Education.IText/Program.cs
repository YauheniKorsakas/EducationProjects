using Education.Core;
using Education.IText.Cases;
using System;
using System.Threading.Tasks;

namespace Education.IText {
    class Program {
        static async Task Main(string[] args) {
            await CaseRunner.RunCaseAsync<BasicFunctionalityCase>();

            Console.ReadKey();
        }
    }
}
