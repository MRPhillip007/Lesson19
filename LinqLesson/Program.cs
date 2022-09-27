using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             TODO:
                find out who is located farthest north/south/west/east using latitude/longitude data
                find max and min distance between 2 persons
                find 2 persons whos ‘about’ have the most same words
                find persons with same friends (compare by friend’s name)  
             */
            List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText("data.json"));
            foreach (Person person in persons)
            {
                System.Console.WriteLine(person.Id);
            }
        }
    }
}
