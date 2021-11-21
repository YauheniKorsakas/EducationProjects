using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Education.Cases.HttpResponseCase
{
    public class HttpResponseCase : ICase
    {
        public async Task RunAsync() {

            var httpResponse = new HttpResponseMessage();
            var person = new Person {
                Name = "zheka",
                Surname = "top"
            };
            var serializedPerson = "{ \"name\": \"zheka\", \"surname\": \"kors\" }";
            var content = new StringContent(serializedPerson);
            httpResponse.Content = content;

            var result = await content.ReadFromJsonAsync<Animal>();


        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class Animal
    {
        public string Nickname { get; set; }
    }
}
