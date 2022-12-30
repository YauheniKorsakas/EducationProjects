using System;

namespace Education.WebApi.Services
{
    public class PersonService : IPersonService
    {
        public void Dispose() {
            Console.WriteLine($"Service {nameof(PersonService)} was disposed.");
        }
        
        public void ShowPersonData() {
            Console.WriteLine("Person data");
        }
    }
}
