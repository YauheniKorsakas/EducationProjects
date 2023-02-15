using Education.Cases;
using Education.Cases.Algos;
using Education.Core;
using System;
using System.Threading.Tasks;
using Education.Cases.HttpResponseCase;
using Education.Cases.SeniorPreparation;
using Education.Cases.Patterns;
using Education.Cases.Patterns.Creational.AbstractFactory;
using Education.Cases.Patterns.Creational.Builder;
using Education.Cases.Patterns.Creational.FactoryMethod;
using Education.Cases.Patterns.Creational.Prototype;
using Education.Cases.Patterns.Creational.Singletone;
using Education.Cases.Patterns.Structural.Adapter;
using Education.Cases.Patterns.Structural.Bridge;
using Education.Cases.Patterns.Structural.Composite;
using Education.Cases.Patterns.Structural.Decorator;
using Education.Cases.Patterns.Structural.Facade;
using Education.Cases.Patterns.Structural.Flyweight;
using Education.Cases.Patterns.Structural.Proxy;
using Education.Cases.Patterns.Structural.ChainOfResponsibility;
using Education.Cases.Patterns.Structural.Command;

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CaseRunner.RunCaseAsync<CommandCase>();
            
            Console.ReadKey();
        }
    }
}
