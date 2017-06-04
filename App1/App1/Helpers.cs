using SI2projekt.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SI2projekt
{
  public static class Helpers
  {
    public static string TranslateToPolish(this DayOfWeek day)
    {
      switch (day)
      {
        case DayOfWeek.Monday:
          return "Poniedzialek";
        case DayOfWeek.Tuesday:
          return "Wtorek";
        case DayOfWeek.Wednesday:
          return "Sroda";
        case DayOfWeek.Thursday:
          return "Czwartek";
        case DayOfWeek.Friday:
          return "Piatek";
        case DayOfWeek.Saturday:
          return "Sobota";
        case DayOfWeek.Sunday:
          return "Niedziela";

      }
      return string.Empty;
    }
    public static string TranslateToEnglish(this DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Monday";
                case DayOfWeek.Tuesday:
                    return "Tuesday";
                case DayOfWeek.Wednesday:
                    return "Wednesday";
                case DayOfWeek.Thursday:
                    return "Thursday";
                case DayOfWeek.Friday:
                    return "Friday";
                case DayOfWeek.Saturday:
                    return "Saturday";
                case DayOfWeek.Sunday:
                    return "Sunday";

            }
            return string.Empty;
        }
    }
    public class SlotComparator : IComparer<Slot>
    {
        public int Compare(Slot x, Slot y)
        {
            return x.Start.CompareTo(y.Start);
        }
    }
}
