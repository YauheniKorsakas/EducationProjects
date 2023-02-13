﻿using Education.Cases;
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

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CaseRunner.RunCaseAsync<SingletoneCase>();
            
            Console.ReadKey();
        }
    }
}
