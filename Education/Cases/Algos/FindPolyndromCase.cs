using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class FindPolyndromCase : ICase
    { // 12321
        public async Task RunAsync() {
            int num = 123454321;
            var parsedNum = num.ToString();

            for (var i = 0; i < (parsedNum.Length / 2); i++) {
                if (parsedNum[i] != parsedNum[parsedNum.Length - i - 1]) {
                    Console.WriteLine("no");
                    break;
                }

                if (i == (parsedNum.Length - 1) / 2) {
                    Console.WriteLine("yes");
                    break;
                }

                Console.WriteLine("yes");
            }

        }
    }
}
