using Education.Cases.AsyncProgramming;
using Education.Cases.AsyncProgramming.TasksCase;
using Education.Cases.DotNet.CSharp9Case;
using Education.Cases.Misc;
using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CaseRunner.RunCaseAsync<PLINQCase>();
            
            Console.ReadKey();
        }
    }
}
