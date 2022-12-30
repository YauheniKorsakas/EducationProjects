using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.Patterns
{
    public class FactoryMethodCase
    {
    }

    public interface IPerson
    {
        string Name { get; set; }
    }

    public class Driver : IPerson
    {
        public string Name { get; set; }
    }

    public class Worker : IPerson
    {
        public string Name { get; set; }
    }

    public interface IPersonProducer
    {
        IPerson GetPerson();
    }

    public class DriverProducer : IPersonProducer
    {
        public IPerson GetPerson() {
            return new Driver();
        }
    }
}
