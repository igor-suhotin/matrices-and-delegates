using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitySim.Homo;

namespace CitySim
{
    public class LifeInTown
    {
        public delegate void EndOfYear(int year);

        public static event EndOfYear YearIsEnded;

        private int year = 2000;
        public int Year
        {
            get { return year; }
        }

        private List<Homo> homos = new List<Homo>();

        public int HomosCount
        {
            get { return homos.Count; }
        }

        public Homo this[int i]
        {
            get { return homos[i]; }
        }

        public int Add(Homo homo)
        {
            homos.Add(homo);
            return homos.Count;
        }


        public LifeInTown() { }
        public LifeInTown(int year)
        {
            this.year = year;
        }

        public int IncrementYear()
        {
            LifeInTown.YearIsEnded(year);
            return ++year;
        }

        public IEnumerable<Homo> Adultes()
        {
            return homos.Where<Homo>(h => (h.Town.Year - h.BirthYear >= Homo.AdultAge));
        }

        public IEnumerable<Homo> UnMarriedAdultOnSex(HomoSex sex)
        {
            return homos.Where<Homo>(h => (h.Town.Year - h.BirthYear >= Homo.AdultAge && h.Sex == sex && h.Spouse == null));
        }


    }
}
