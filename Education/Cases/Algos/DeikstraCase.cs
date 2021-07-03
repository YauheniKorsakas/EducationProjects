using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class DeikstraCase : ICase
    {
        public Task RunAsync()
        {
            ExecuteCase();

            return Task.CompletedTask;
        }

        private void ExecuteCase()
        {
            throw new NotImplementedException();
        }
    }
}
// алго дейкстры находит путь с минимальным весом, а не количеством ребер как в бфс.
// бфс только в невзвешенном графе.
// алго работает только с напарвленными ациклическими графами.
// дейкстра работает только с положительными весами.