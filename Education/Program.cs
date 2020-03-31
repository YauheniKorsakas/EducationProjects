﻿using Education.Cases;
using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education {
    public class Program {
        public static async Task Main(string[] args) {
            await CaseRunner.RunCaseAsync<LockCase>();=

           Console.ReadKey();
        }

        private static ICase GetCaseInstance<T>() where T: ICase, new() => Activator.CreateInstance<T>();
    }
}
