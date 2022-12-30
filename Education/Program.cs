using Education.Cases;
using Education.Cases.Algos;
using Education.Core;
using System;
using System.Threading.Tasks;
using Education.Cases.HttpResponseCase;
using Education.Cases.SeniorPreparation;
using Education.Cases.Patterns;

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // await CaseRunner.RunCaseAsync<TaskCase>();
            var singletone = Singletone.Instance;
            Console.WriteLine(singletone.CreationDate);
            Console.ReadKey();
        }

        private static ICase GetCaseInstance<T>() where T : ICase, new() => Activator.CreateInstance<T>();
    }
}
