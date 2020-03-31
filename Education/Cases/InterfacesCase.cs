using System.Threading.Tasks;
using System;
using Education.Core;

namespace Education.Cases {
    public interface IPerson {
        string Name { set; get; }
    }

    public interface IEmployee : IPerson { }

    public class Empl : IEmployee {
        public string Name { set; get; }

        public Empl(string name) {
            Name = name;
        }
    }

    public interface IJob<out T> where T: IPerson { // covariant
        T DoJob(object empl);
    }

    public interface ISomeJob<in T> where T: IPerson { // contrvariant
        object DoJob(T empl);
    }

    public class Job<T>: IJob<T> where T : IEmployee {
        public T DoJob(object empl) {
            return default(T);
        }
    }

    public class SomeJob<T>: ISomeJob<T> where T: IPerson {
        public object DoJob(T empl) {
            return default(T);
        }
    }

    public class InterfacesCase : ICase {
        public async Task RunAsync() {
            ISomeJob<IEmployee> someEmplJob = new SomeJob<IPerson>();
        }
    }
}
