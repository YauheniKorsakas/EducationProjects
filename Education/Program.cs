using Education.Cases;
using Education.Cases.Algos;
using Education.Core;
using System;
using System.Threading.Tasks;
using Education.Cases.HttpResponseCase;
using Education.Cases.SeniorPreparation;
using Education.Cases.Patterns;
using Education.Cases.Patterns.AbstractFactory;
using Education.Cases.Patterns.Builder;
using Education.Cases.Patterns.FactoryMethod;
using Education.Cases.Patterns.Prototype;

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CaseRunner.RunCaseAsync<PrototypeCase>();
            
            Console.ReadKey();
        }
    }
}
