using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.FactoryMethod
{
    public class FactoryMethodCase : ICase
    {
        public async Task RunAsync() {
            var handlers = new List<BaseDeviceHandler> {
                new MobileDeviceHandler(),
                new LaptopDeviceHandler()
            };
            handlers.ForEach(item => item.Handle());
        }
    }

    public abstract class BaseDeviceHandler {
        public string Name { get; set; }

        public void Handle() {
            var device = GetDevice();
            Console.WriteLine($"{device.GetType().Name} has been handled.");
        }

        protected abstract Device GetDevice();
    }

    public class MobileDeviceHandler : BaseDeviceHandler
    {
        protected override Device GetDevice() {
            return new Mobile();
        }
    }

    public class LaptopDeviceHandler : BaseDeviceHandler
    {
        protected override Device GetDevice() {
            return new Laptop();
        }
    }

    public abstract class Device
    {
        string Name { get; set; }
    }

    public class Mobile : Device { }

    public class Laptop : Device { }
}
