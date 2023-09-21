using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitySim.HomoEventArgs;
//using static CitySim.HomoEventArgs;

namespace CitySim
{
    public class Homo
    {
        public const int AdultAge = 18;
        public enum HomoSex { Man, Woman };

        public delegate void HomoDelegate(Homo sender, HomoEventArgs homoEventArgs);
        public static event HomoDelegate HomoEvent;

        public LifeInTown Town;
        public string Name, LastName;
        public int BirthYear;
        public HomoSex Sex;
        public Homo Spouse; // Супруг(а)

        public Homo(LifeInTown town, string name, string lastName, int birthYear, HomoSex sex)
        {
            Town = town;
            Name = name;
            LastName = lastName;
            BirthYear = birthYear;
            Sex = sex;
        }

        public bool Marry(Homo spouse)
        {
            var alert = false;

            foreach (var homo in (new Homo[] { this, spouse }))
            {
                if (homo.Spouse != null)
                {
                    Console.WriteLine("Зафиксирована попытка стать двое{0}\nВиновник - {1} {2}.",
                        homo.Sex == HomoSex.Man ? "женцем" : "мужницей", homo.Name, homo.LastName);
                    alert = true;
                }

                if (Town.Year - homo.BirthYear < AdultAge )
                {
                    Console.WriteLine("Зафиксирована попытка брака с несовершеннолетним\nНесовершеннолетний - {0} {1}, возраст {2}.",
                        homo.Name, homo.LastName, Town.Year - homo.BirthYear);
                    alert = true;
                }
            }

            if (Sex == spouse.Sex)
            {
                Console.WriteLine("Зафиксирована попытка гомосексуального брака\nНарушители - {0} {1} и {2} {3}.",
                    Name, LastName, spouse.Name, spouse.LastName);
                alert = true;
            }

            if (alert)
            {
                Console.WriteLine("Просьба сообщить в правоохранительные органы !\n");
                return false;
            }

            Spouse = spouse;
            spouse.Spouse = this;

            if (HomoEvent != null)
            {
                HomoEvent(this, new HomoEventArgs (HomoEventProp.Marriage));
            }
            return true;
        }

        public void OnAdult()
        {
            Console.WriteLine($"{Name} {LastName} достиг" + (Sex == HomoSex.Man ? "" : "ла") + " совершеннолетия. Поздравляем !");
        }

    }
}
