using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Visitor
{
    public class VisitorCase : ICase
    {
        public async Task RunAsync() {
            var hotel = new Hotel();
            var cleaningService = new HotelRoomCleaningService();
            var fireAlarmCheckService = new HotelRoomFireAlarmCheckService();
            hotel.ApplyToRooms(cleaningService);
            hotel.ApplyToRooms(fireAlarmCheckService);
        }

        public interface IHotelRoom
        {
            int Number { get; set; }
            void Accept(IHotelRoomService externalComission);
        }

        public interface IHotelRoomService
        {
            void Visit(IHotelRoom hotelRoom);
        }

        public class HotelRoomCleaningService : IHotelRoomService
        {
            public void Visit(IHotelRoom hotelRoom) {
                Console.WriteLine($"Cleaning room under number {hotelRoom.Number}...\nEverything cleaned up!");
            }
        }

        public class HotelRoomFireAlarmCheckService : IHotelRoomService
        {
            public void Visit(IHotelRoom hotelRoom) {
                Console.WriteLine($"Checking fire alarm in room under number {hotelRoom.Number}...\nEverthing checked up!");
            }
        }

        public class HotelRoom : IHotelRoom
        {
            public int Number { get; set; }

            public void Accept(IHotelRoomService externalComission) {
                externalComission?.Visit(this);
            }
        }

        public class Hotel
        {
            private readonly List<HotelRoom> rooms;

            public Hotel() {
                rooms = new List<HotelRoom>
                {
                    new HotelRoom { Number = 1 },
                    new HotelRoom { Number = 2 },
                    new HotelRoom { Number = 3 },
                };
            }

            public void ApplyToRooms(IHotelRoomService hotelService) {
                Console.WriteLine($"\n{hotelService.GetType().Name} is applying...");

                foreach (var room in rooms) {
                    room.Accept(hotelService);
                }
            }
        }
    }
}
