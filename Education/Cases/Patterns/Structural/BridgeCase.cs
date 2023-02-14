using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Bridge
{
    public class BridgeCase : ICase
    {
        public async Task RunAsync() {
            BaseRemoteControl sonyRemoteControl = new SonyRemoteControl(new SonyTV());
            sonyRemoteControl.SwitchOn();
            sonyRemoteControl.SetChannel(101);
            sonyRemoteControl.SwitchOff();

            Console.WriteLine();
            BaseRemoteControl samsungRemoteControl = new SamsungRemoteControl(new SamsungTV());
            samsungRemoteControl.SwitchOn();
            samsungRemoteControl.SetChannel(202);
            samsungRemoteControl.SwitchOff();

            Console.ReadKey();
        }

        public interface ITV
        {
            string ModelName { get; }
            void SwitchOn();
            void SwitchOff();
            void SetChannel(int channelNumber);
        }

        public class SamsungTV : ITV
        {
            public string ModelName => "Samsung TV L234";

            public void SetChannel(int channelNumber) {
                Console.WriteLine($"Setting channel {channelNumber}: {ModelName}");
            }

            public void SwitchOff() {
                Console.WriteLine($"Switching off: {ModelName}");
            }

            public void SwitchOn() {
                Console.WriteLine($"Switching on: {ModelName}");
            }
        }

        public class SonyTV : ITV
        {
            public string ModelName => "Sony TV S00";

            public void SetChannel(int channelNumber) {
                Console.WriteLine($"Setting channel {channelNumber}: {ModelName}");
            }

            public void SwitchOff() {
                Console.WriteLine($"Switching off: {ModelName}");
            }

            public void SwitchOn() {
                Console.WriteLine($"Switching on: {ModelName}");
            }
        }

        public abstract class BaseRemoteControl
        {
            protected ITV ownedTV;

            public BaseRemoteControl(ITV ownedTV) {
                this.ownedTV = ownedTV;
            }

            public abstract void SwitchOn();
            public abstract void SwitchOff();
            public abstract void SetChannel(int channelNumber);
        }

        public class SamsungRemoteControl : BaseRemoteControl
        {
            public SamsungRemoteControl(ITV ownedTV) : base(ownedTV) { }

            public override void SetChannel(int channelNumber) {
                ownedTV.SetChannel(channelNumber);
            }

            public override void SwitchOff() {
                ownedTV.SwitchOff();
            }

            public override void SwitchOn() {
                Console.Beep();
                ownedTV.SwitchOn();
            }
        }

        public class SonyRemoteControl : BaseRemoteControl
        {
            public SonyRemoteControl(ITV ownedTV) : base(ownedTV) { }

            public override void SetChannel(int channelNumber) {
                ownedTV.SetChannel(channelNumber);
            }

            public override void SwitchOff() {
                Console.Beep();
                ownedTV.SwitchOff();
            }

            public override void SwitchOn() {
                ownedTV.SwitchOn();
            }
        }
    }
}
