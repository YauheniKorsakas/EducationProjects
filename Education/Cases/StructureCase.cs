using Education.Core;
using Education.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class StructureCase : ICase
    {
        public StructureCase(int age) {

        }

        public async Task RunAsync() {
            IStruct str = new DisplayingPerson(1, new Person { });
            var str1 = default(DisplayingPerson);
            Console.WriteLine(str1);

        }



    }

    interface IStruct
    {
        int Age { get; }
    }

    public readonly struct DisplayingPerson : IStruct
    {
        public int Age { get; }
        public Person Person { get; }

        public DisplayingPerson(int age, Person person) {
            Age = age;
            Person = person;
        }
    }
}
