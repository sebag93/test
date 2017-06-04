using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SI2projekt
{
  public partial class Budynki : ContentPage
  {
    public class ViewModel
    {
      public string Key { get; set; }//pokazuje klucz wartosc 
      public string Value { get; set; }
           

      public ViewModel(string key, string value)
      {
        Key = key;
        Value = value;
      }
    }
    private static ObservableCollection<ViewModel> 
            buldings = new ObservableCollection<ViewModel>();
    public Budynki()
    {
            buldings.Clear();
            buldings.Add(new ViewModel("Budynek A Wydział Elektrotechniki i Informatyki", "50.026864, 21.985243")); //vievModel obsługa wszystkich akcji połaczonych z widokiem
            buldings.Add(new ViewModel("Budynek B Wydział Elektrotechniki i Informatyki - Dziekanat", "50.026831, 21.984441"));
            buldings.Add(new ViewModel("Budynek C Wydział Budowy Maszyn i Lotnictwa", "50.026305, 21.983746"));
            buldings.Add(new ViewModel("Budynek D Wydział Elektrotechniki i Informatyki", "50.025810, 21.983674"));
            buldings.Add(new ViewModel("Budynek E Wydział Elektrotechniki i Informatyki", "50.026328, 21.984691"));
            buldings.Add(new ViewModel("Budynek F Wydział Elektrotechniki i Informatyki, Zakład Poligrafii, Oficyna Wydawnicza", "50.025956, 21.983414"));
            buldings.Add(new ViewModel("Budynek G Wydział Budowy Maszyn i Lotnictwa", "50.025964, 21.984189"));
            buldings.Add(new ViewModel("Budynek H Wydział Chemiczny - Dziekanat", "50.019830, 21.985741"));
            buldings.Add(new ViewModel("Budynek J Studium Języków Obcych, Studium Wychowania Fizycznego i Sportu", "50.019441, 21.980521"));
            buldings.Add(new ViewModel("Budynek K Wydział Budownictwa, Inżynierii Środowiska i Architektury, Wydział Matematyki i Fizyki Stosowanej - Katedra Fizyki, Katedra Matematyki", "50.019502, 21.985528"));
            buldings.Add(new ViewModel("Budynek L Wydział Budowy Maszyn i Lotnictwa - Dziekanat", "50.018154, 21.986880"));
            buldings.Add(new ViewModel("Budynek Ł Hala Sportowa", "50.018414, 21.980445"));
            buldings.Add(new ViewModel("Budynek O Stołówka Akademik", "50.020534, 21.983454"));
            buldings.Add(new ViewModel("Budynek P Wydział Budownictwa, Inżynierii Środowiska i Architektury- Dziekanat", "50.019011, 21.981421"));
            buldings.Add(new ViewModel("Budynek R Hotel Asystenta", "50.019559, 21.980021"));
            buldings.Add(new ViewModel("Budynek S Wydział Zarządzania - Dziekanat, Zespół Sal Wykładowych", "50.019328, 21.987269"));
            buldings.Add(new ViewModel("Budynek W Laboratorium Geo-Drogowe WBIŚiA", "50.017030, 21.982155"));
            buldings.Add(new ViewModel("Budynek V Regionalne Centrum Dydaktyczno-Konferencyjne i Biblioteczno-Administracyjne - Rektorat, Administracja", "50.019176, 21.988837"));
            buldings.Add(new ViewModel("DS Promień", "50.019984, 21.981735"));
            buldings.Add(new ViewModel("DS Arcus", "50.019684, 21.982271"));
            buldings.Add(new ViewModel("DS Nestor", "50.020260, 21.982089"));
            buldings.Add(new ViewModel("DS Akapit", "50.019877, 21.982754"));
            buldings.Add(new ViewModel("DS Pingwin", "50.020418, 21.982695"));
            buldings.Add(new ViewModel("DS Ikar", "50.020039, 21.983360"));
            buldings.Add(new ViewModel("Radio Centrum", "50.020039, 21.983360"));
            buldings.Add(new ViewModel("DS Alchemik", "50.018564, 21.982470"));
            buldings.Add(new ViewModel("Klub Plus", "50.019726, 21.980877"));
            buldings.Add(new ViewModel("Przychodnia Akademicka", "50.019726, 21.980877"));
            buldings.Add(new ViewModel("Wiata Grzybek", "50.019402, 21.983178"));
            buldings.Add(new ViewModel("Preinkubator Akademicki", "50.018047, 21.985492"));

            InitializeComponent();
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                schemastandard();
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                schemacontrast();
            }
            lstBuldings.ItemsSource = buldings;
      BindingContext = this;
            
    }
        public void schemastandard()
        {
            this.BackgroundColor = Color.Black;
            lstBuldings.BackgroundColor = Color.Black;
            txtNiemaza.TextColor = Color.White;
        }
        public void schemacontrast()
        {
            this.BackgroundColor = Color.Yellow;
            lstBuldings.BackgroundColor = Color.Yellow;
            txtNiemaza.TextColor = Color.Black;
            
        }

    private void LstBuldings_OnItemTapped(object sender, ItemTappedEventArgs e)
    {
      Navigation.PushAsync(new Navigation(e.Item as ViewModel));
    }
  }
}
