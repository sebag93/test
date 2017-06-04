using System;

namespace SI2projekt.Plan
{
  public class Slot//przechowywanie poszcegolnych skladnikow planu
  {
    public string Name;
    public string Location;
    public string LocationDetails;
    public string LocationAdress;

    public DateTime Start;
        public DateTime Stop;
    public TimeSpan Duration;
  }
}