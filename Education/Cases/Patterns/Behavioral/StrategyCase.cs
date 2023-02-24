using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Strategy
{
    public class StrategyCase : ICase
    {
        public async Task RunAsync() {
            IPersonStatisticRepresentator representationForAccountant = new PersonStatisticRepresentationForAccountant();
            IPersonStatisticRepresentator representationForTeamlead = new PersonStatisticRepresentationForTeamlead();
            var statisticsStorage = new PersonStatisticsStorage();
            statisticsStorage.ShowTextRepresentation(1, representationForAccountant);
            statisticsStorage.ShowTextRepresentation(1, representationForTeamlead);
        }

        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class PersonStatistic
        {
            public Person Person { get; set; }
            public int TotalEarnedMoney { get; set; }
            public int LastYearEarnedMoney { get; set; }
            public int ClosedTasks { get; set; }
        }

        public interface IPersonStatisticRepresentator
        {
            string GetPersonStatisticRepresentation(PersonStatistic personStatistic);
        }

        public interface IPersonStatisticStorage
        {
            void ShowTextRepresentation(int personId, IPersonStatisticRepresentator representator);
        }

        public class PersonStatisticRepresentationForAccountant : IPersonStatisticRepresentator
        {
            public virtual string GetPersonStatisticRepresentation(PersonStatistic personStatistic) {
                return $"Representation for accountant: Person id - {personStatistic.Person.Id}, Last year earned money - {personStatistic.LastYearEarnedMoney}, " +
                    $"Total earned money - {personStatistic.TotalEarnedMoney}";
            }
        }

        public class PersonStatisticRepresentationForTeamlead : IPersonStatisticRepresentator
        {
            public virtual string GetPersonStatisticRepresentation(PersonStatistic personStatistic) {
                return $"Representation for teamlead: Person id - {personStatistic.Person.Id}, Closed tasks - {personStatistic.ClosedTasks}";
            }
        }

        public class PersonStatisticsStorage : IPersonStatisticStorage
        {
            private readonly List<PersonStatistic> personStatistics = new List<PersonStatistic> {
                new PersonStatistic {
                    Person = new Person {
                        Id = 1,
                        Name = "Zheka"
                    },
                    ClosedTasks = 1000,
                    LastYearEarnedMoney = 12,
                    TotalEarnedMoney = 13
                }
            };

            public void ShowTextRepresentation(int personId, IPersonStatisticRepresentator representator) {
                var personStatistic = personStatistics.FirstOrDefault(item => item.Person.Id == personId);
                var resultRepresentation = representator.GetPersonStatisticRepresentation(personStatistic);
                Console.WriteLine(resultRepresentation);
            }
        }
    }
}
