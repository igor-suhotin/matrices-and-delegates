using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitySim.Homo;
using static CitySim.HomoEventArgs;

namespace CitySim
{
    class TownGame
    {
        static void Main(string[] args)
        {
            var town = new LifeInTown(2000);
            town.Add(new Homo(town, "Элтон", "Джон", 1985, HomoSex.Man));
            town.Add(new Homo(town, "Рудольф", "Нуреев", 1991, HomoSex.Man));

            town.Add(new Homo(town, "Елизавета", "Виндзор", 1984, HomoSex.Woman));
            town.Add(new Homo(town, "Илья", "Муромец", 1983, HomoSex.Man));

            town.Add(new Homo(town, "Одиссей", "Итакский", 1984, HomoSex.Man));

            town[0].Marry(town[2]);
            town[0].Marry(town[1]);

            for (int i = 0; i < 30; i++)
            {
                town.Add(new Homo(town, $"МужскоеИмя_{i}", $"МужскаяФамилия_{i}", 2000 - i, HomoSex.Man));
                town.Add(new Homo(town, $"ЖенскоеИмя_{i}", $"ЖенскаяФамилия_{i}", 2000 - i, HomoSex.Woman));
            }


            HomoEvent += delegate (Homo sender, HomoEventArgs eventArgs)
            {
                if (eventArgs.Type == HomoEventProp.Marriage)
                    Console.WriteLine((sender.Sex == HomoSex.Man ?
                          "{0} {1} {2} года рождения и {3} {4} {5} года рождения" :
                          "{3} {4} {5} года рождения и {0} {1} {2} года рождения") +
                          " сочетались законным браком. Поздравим новобрачных ! Совет да любовь !",
                          sender.Name, sender.LastName, sender.BirthYear,
                          sender.Spouse.Name, sender.Spouse.LastName, sender.Spouse.BirthYear);
            };

            LifeInTown.YearIsEnded += delegate (int year)
            {
                Console.WriteLine("{0}\n{1}-й год прошёл.\n{0}", new string('=', 60), year);
            };

            int begin = town.Year;
            for (int currentYear = begin; currentYear < begin + 10;)
            {
                currentYear = town.IncrementYear();

                // Поздравим с совершеннолетием !
                for (int k = 0; k < town.HomosCount; k++)
                    if (currentYear - town[k].BirthYear == Homo.AdultAge)
                        town[k].OnAdult();
                Svaha(town);
            }

            /*
            Console.WriteLine($"===\nСписок совершеннолетних ({town.Adultes().Count()})");
            foreach (Homo h in town.Adultes())
            {
                Console.WriteLine($"{h.Name} {h.LastName} {h.BirthYear}");
            }

            Console.WriteLine($"===\nСписок холостых совершеннолетних мужчин ({town.UnMarriedAdultOnSex(HomoSex.Man).Count()})");
            foreach (Homo h in town.UnMarriedAdultOnSex(HomoSex.Man))
            {
                Console.WriteLine($"{h.Name} {h.LastName}");
            }
            Console.WriteLine($"===\nСписок незамужних совершеннолетних женщин ({town.UnMarriedAdultOnSex(HomoSex.Woman).Count()})");

            foreach (Homo h in town.UnMarriedAdultOnSex(HomoSex.Woman))
            {
                Console.WriteLine($"{h.Name} {h.LastName}");
            }
            Console.WriteLine("===");

            */




            Console.WriteLine("Для завершения нажмите любой символ");
            Console.ReadKey();
        }

        static void Svaha(LifeInTown town)
        {
            int brideCount = town.UnMarriedAdultOnSex(HomoSex.Woman).Count();
            int groomCount = town.UnMarriedAdultOnSex(HomoSex.Man).Count();

            int maxPairs = Math.Min(brideCount, groomCount);
            int possiblePairsCount = groomCount * brideCount;

            Random rnd = new Random();
            // Выберем количество новых семей
            int numPairs = rnd.Next(maxPairs + 1);

            while (numPairs > 0)
            {
                foreach (Homo man in town.UnMarriedAdultOnSex(HomoSex.Man))
                {
                    foreach (Homo woman in town.UnMarriedAdultOnSex(HomoSex.Woman))
                        if (rnd.Next(100) <= (int)(100 * numPairs / possiblePairsCount) + 1) /* Вероятность выбора */
                        {
                            man.Marry(woman);
                            break;
                        }

                    possiblePairsCount--;
                    if (--numPairs == 0) break;
                }
            }
        }
    }
}
