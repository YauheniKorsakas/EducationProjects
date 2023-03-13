using Education.Cases.Patterns.Behavioral.TemplateMethod;
using Education.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.Misc
{
    public class MiscCase : ICase
    {
        public async Task RunAsync() {
            var list = new List<int> { 1, 2, 3 };
            var cast = list.AsReadOnly();
            var isItReadonly = cast is IReadOnlyCollection<int>;
            list[0] = 100;
            foreach (var item in cast) {
                Console.WriteLine(item);
            }
        }
    }
}
