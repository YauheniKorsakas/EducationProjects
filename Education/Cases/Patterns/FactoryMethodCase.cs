using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.FactoryMethod
{
    public class FactoryMethodCase : ICase
    {
        public async Task RunAsync() {
            var producers = new List<BaseDeviceProducer> {
                new MobileDevideProducer(),
                new LaptopDeviceProducer()
            };
            var producedDevices = producers.Select(s => s.GetDevice());
            
            foreach (var device in producedDevices) {
                Console.WriteLine($"{device.GetType().Name}");
            }
        }
    }

    public abstract class BaseDeviceProducer {
        public abstract Device GetDevice();
    }

    public class MobileDevideProducer : BaseDeviceProducer
    {
        public override Device GetDevice() {
            return new Mobile();
        }
    }

    public class LaptopDeviceProducer : BaseDeviceProducer
    {
        public override Device GetDevice() {
            return new Laptop();
        }
    }

    public abstract class Device { }

    public class Mobile : Device { }

    public class Laptop : Device { }
}
