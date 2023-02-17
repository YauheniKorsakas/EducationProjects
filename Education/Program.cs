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
using Education.Cases.Patterns.Behavioral.ChainOfResponsibility;
using Education.Cases.Patterns.Behavioral;
using Education.Cases.Patterns.Behavioral.Interpreter;
using Education.Cases.Patterns.Behavioral.Iterator;
using Education.Cases.Patterns.Behavioral.Mediator;
using Education.Cases.Patterns.Behavioral.Memento;
using Education.Cases.Patterns.Behavioral.Observer;
using Education.Cases.Patterns.Behavioral.State;

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CaseRunner.RunCaseAsync<StateCase>();
            
            Console.ReadKey();
        }
    }
}
