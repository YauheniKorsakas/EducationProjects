using Education.Cases;
using Education.Cases.Algos;
using Education.Core;
using System;
using System.Threading.Tasks;
using Education.Cases.HttpResponseCase;

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CaseRunner.RunCaseAsync<StructureCase>();

            Console.ReadKey();
        }

        private static ICase GetCaseInstance<T>() where T : ICase, new() => Activator.CreateInstance<T>();
    }
}
