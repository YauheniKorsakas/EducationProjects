using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Creational.Singletone
{
    public class SingletoneCase : ICase
    {
        public async Task RunAsync() {
            Console.WriteLine(Singletone.Instance.CreationDate);
        }
    }

    public class Singletone {
        public static Singletone Instance { get; }
        public string CreationDate { get; set; }

        static Singletone() {
            Instance = new Singletone(DateTime.Now.ToString());
        }

        private Singletone(string creationDate) {
            this.CreationDate = creationDate;
        }
    }
}
