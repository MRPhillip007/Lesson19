using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqLesson
{
    class Program
    {
        static void Main()
        {
            var persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(File.ReadAllText("data.json")).ToList();

            // find 2 persons whos ‘about’ have the most same words
            char[] delimiterChars = { ' ', ',', '.', ':', '!', '?' };

            FindSameAbout(persons, delimiterChars);

            // find out who is located farthest north/south/west/east using latitude/longitude data
            LocatedFarthest(persons);


        }

        static void FindSameAbout(List<Person> persons, char[] delimiterChars)
        {
            var personsWordsPairsResult = new Dictionary<List<string>, int>();

            var personWordsPairs = persons.Select(x => new { personId = x.Id, words = x.About.Split(delimiterChars) }).ToList();

            var pairs = from i in Enumerable.Range(0, personWordsPairs.Count - 1)
                        from j in Enumerable.Range(i + 1, personWordsPairs.Count - i - 1)
                        select Tuple.Create(personWordsPairs[i], personWordsPairs[j]);

            foreach (var pair in pairs)
            {
                var commonWords = pair.Item1.words.Intersect(pair.Item2.words);

                personsWordsPairsResult.Add(new List<string> { pair.Item1.personId, pair.Item2.personId }, commonWords.Count());
            }

            var result = persons.Where(p => personsWordsPairsResult.OrderByDescending(x => x.Value).FirstOrDefault().Key.Contains(p.Id)).ToList();

            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        static void LocatedFarthest(List<Person> persons)
        {
            var farthestNorth = persons.Where(p => p.Latitude == persons.Max(p => p.Latitude)).FirstOrDefault();
            var farthestSouth = persons.Where(p => p.Latitude == persons.Min(p => p.Latitude)).FirstOrDefault();
            var farthestWest = persons.Where(p => p.Longitude == persons.Max(p => p.Longitude)).FirstOrDefault();
            var farthestEast = persons.Where(p => p.Longitude == persons.Min(p => p.Longitude)).FirstOrDefault();

            Console.WriteLine($"North - {farthestNorth}\n South - {farthestSouth}\n West {farthestWest}\n East{farthestEast}");
        }
    }

}
//checked
