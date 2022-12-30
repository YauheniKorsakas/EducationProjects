using Education.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.SeniorPreparation
{
    class NetAlgo : ICase
    {
        public async Task RunAsync() {
            Stack();
        }

        private void Stack() {
            var linkedList = new LinkedList<int>();
            var lastNode = linkedList.AddLast(1);
            linkedList.AddFirst(2);
            linkedList.AddBefore(lastNode, 10100);
            var hashtable = new Hashtable();
            
            foreach(var item in linkedList) {
                Console.WriteLine(item);
            } 
        }
    }
}
