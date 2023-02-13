using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Creational.AbstractFactory
{
    public class AbstractFactoryCase : ICase
    {
        public async Task RunAsync() {
            var driversGroup = new DriversGroup();
            var driverLead = driversGroup.GetLead();
            var driverSub = driversGroup.GetSubordinate();
            driverLead.SendMessageToSubordinate(driverSub);
        }
    }

    public abstract class BaseEmployeeGroup
    {
        public abstract Lead GetLead();
        public abstract Subordinate GetSubordinate();
    }

    public class DriversGroup : BaseEmployeeGroup
    {
        public override Lead GetLead() {
            return new ChiefDriver();
        }

        public override Subordinate GetSubordinate() {
            return new SubordinateDriver();
        }
    }

    public abstract class Lead
    {
        public abstract void SendMessageToSubordinate(Subordinate subordinate);
    }

    public abstract class Subordinate { }

    public class ChiefDriver : Lead
    {
        public override void SendMessageToSubordinate(Subordinate subordinate) {
            Console.WriteLine($"{GetType().Name} owns {subordinate?.GetType()?.Name}");
        }
    }

    public class SubordinateDriver : Subordinate { }
}
