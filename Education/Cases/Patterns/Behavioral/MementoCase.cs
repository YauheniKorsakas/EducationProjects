using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Memento
{
    public class MementoCase : ICase
    {
        public async Task RunAsync() {
            TVHall hall = new TVHall();
            hall.LedTV = new LEDTV("Xiaomi");
            hall.WallColor = "White";

            TVHallStatesStorage stateStorage = new TVHallStatesStorage();
            stateStorage.AddState(hall.CreateState());
            hall.LedTV = new LEDTV("Samsung");
            hall.WallColor = "Green";
            stateStorage.AddState(hall.CreateState());
            hall.LedTV = new LEDTV("LG");
            hall.WallColor = "Black";

            Console.WriteLine("\nOrignator current state : " + hall.GetDetails());
            Console.WriteLine("\nOriginator restoring to Xiaomi");
            hall.SetState(stateStorage.GetState(0));
            Console.WriteLine("\nOrignator current state : " + hall.GetDetails());
            Console.ReadKey();
        }

        public class LEDTV
        {
            public string Name { get; set; }

            public LEDTV(string name) {
                Name = name;
            }

            public string GetDetails() => $"LEDTV [Name = {Name}]";
        }

        public class TVHallState {
            public LEDTV LedTV { get; set; }
            public string WallColor { get; set; }

            public TVHallState(LEDTV ledTV, string wallColor) {
                LedTV = ledTV;
                WallColor = wallColor;
            }

            public string GetDetails() => $"TVHallState [LedTV={LedTV.GetDetails()}, Wall color={WallColor}]";
        }

        public class TVHallStatesStorage
        {
            private List<TVHallState> ledTvList = new List<TVHallState>();

            public void AddState(TVHallState memento) {
                ledTvList.Add(memento);
                Console.WriteLine("New hall state in storage:" + memento.GetDetails());
            }

            public TVHallState GetState(int index) => ledTvList.ElementAt(index);
        }

        public class TVHall {
            public LEDTV LedTV { get; set; }
            public string WallColor { get; set; }

            public TVHallState CreateState() => new TVHallState(LedTV, WallColor);

            public void SetState(TVHallState state) {
                LedTV = state.LedTV;
                WallColor = state.WallColor;
            }

            public string GetDetails() => $"TVHall [LedTV={LedTV.GetDetails()}, Wall color={WallColor}]";
        }
    }
}
