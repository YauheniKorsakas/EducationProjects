using System;

namespace Education.Cases.Patterns
{
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
