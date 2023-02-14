using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Flyweight
{
    public class FlyweightCase : ICase
    {
        public async Task RunAsync() {
            var circle = ShapeFactory.GetShape<Circle>();
            Enumerable.Range(1, 5)
                .Select(item => ShapeFactory.GetShape<Circle>())
                .ToList()
                .ForEach(circle => {
                    circle.Color = "red";
                    circle.Draw();
                });
            Enumerable.Range(1, 5)
                .Select(item => ShapeFactory.GetShape<Circle>())
                .ToList()
                .ForEach(circle => {
                    circle.Color = "blue";
                    circle.Draw();
                });
        }

        public interface IShape
        {
            string Color { get; set; }
            void Draw();
        }

        public class Circle : IShape
        {
            public string Color { get; set; } = "white";
            private int X = 10;
            private int Y = 20;
            private int Radius = 30;

            public void Draw() {
                Console.WriteLine($"Circle: {X}, {Y}, {Radius} with color: {Color}");
                Console.WriteLine($"Circle's hashcode: {GetHashCode()}");
            }
        }

        public class ShapeFactory
        {
            private readonly static Dictionary<Type, IShape> storage = new Dictionary<Type, IShape>();

            public static T GetShape<T>() where T: class, IShape, new() {
                if (!storage.TryGetValue(typeof(T), out IShape result)) {
                    result = Activator.CreateInstance<T>();
                    storage.Add(typeof(T), result);
                }

                return (T)result;
            }
        }
    }
}
