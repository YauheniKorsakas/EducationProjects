using Education.Models;
using System;
using System.Collections.Generic;

namespace Education
{
    public interface ITest { }

    public class Girafe : Animal, IDisposable, ITest 
    {
        public IEnumerable<int> Dates { get; set; }

        public CommonData Data { get; set; }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}