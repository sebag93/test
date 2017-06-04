using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI2projekt.Plan;
using Xamarin.Forms;
using System.Net;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Position = Xamarin.Forms.Maps.Position;
using SI2projekt.ApplicationData;

namespace SI2projekt
{
    public partial class Navigation : ContentPage
    {
        private IGeolocator locator;
        private Repository<Slot> ScheduleRepository;

        public Navigation()
        {
            InitializeComponent();
            try
            {
                if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
                {
                    schemastandard();
                }
                if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
                {
                    schemacontrast();
                }

                ScheduleRepository = App.GetApp().ScheduleRepository;
                if (ScheduleRepository.Data.Values.All(x => x.Start.Date < DateTime.Now.Date))//sprawdzamy czy plan jest aktualny
                {
                    //trzeba zaktualizowac plan zajec
                    var schedule = Grabber.Grab(App.GetApp().AppSettings.Get("User"),
                                                App.GetApp().AppSettings.Get("Password"));
                    for (int i = 0; i < schedule.Count; i++)
                        ScheduleRepository.Add(i.ToString(), schedule[i]);
                    ScheduleRepository.Save();
                }
                var tmp = new List<Slot>();
                foreach (var val in ScheduleRepository.Data.Values)
                {
                    if (val.Start.Date == DateTime.Now.Date)
                    {
                        tmp.Add(val);
                    }
                }

                if (tmp.Count > 0)
                {
                    int i = 0;
                    tmp.Sort(new SlotComparator());
                    foreach (var zaj in tmp)
                    {
                        i++;
                        if (DateTime.Now.Hour <= zaj.Stop.Hour)
                        {
                            SetNavigation(zaj.LocationAdress);
                            return;
                        }
                        if (tmp.Count == i)
                        {
                            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
                            {
                                txtNiemaza.Text = "Zajęcia zakończone";
                            }
                            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                            {
                                txtNiemaza.Text = "END";
                            }
                        }
                    }

                }
                else
                {
                    if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
                    {
                        txtNiemaza.Text = "Dzisiaj nie ma zajęć :)";
                    }
                    if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                    {
                        txtNiemaza.Text = "Today you have free day :)";
                    }
                    return;
                }

                //try
                //{
                //    InitializeComponent();

                //    MyMap.MoveToRegion(new MapSpan(new Position(50.012409, 22.674363), 0.0000001, 0.0000001));
                //    MyMap.HasZoomEnabled = true;

                //    locator = CrossGeolocator.Current;
                //    locator.StartListeningAsync(2000, 1, true);
                //    locator.PositionChanged += (sender, e) =>
                //    {
                //        var position = e.Position;
                //        MyMap.MoveToRegion(new MapSpan(new Position(e.Position.Latitude, e.Position.Longitude), 0.0000001, 0.0000001));
                //    };
                //}
                //catch (Exception ex)
                //{ }
            }

            catch
            {
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
                {
                    txtNiemaza.Text = "Brak Harmonogramu :)";
                }
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                {
                    txtNiemaza.Text = "no schedule :)";
                }
            }


            }

        public void schemastandard()
        {
            this.BackgroundColor = Color.Black;
            txtNiemaza.TextColor = Color.White;
            txtNiemaza.FontSize = 30;
        }
        public void schemacontrast()
        {
            this.BackgroundColor = Color.Yellow;
            txtNiemaza.TextColor = Color.Black;
            txtNiemaza.FontSize = 40;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (locator != null)
                locator.StopListeningAsync();
        }

        public Navigation(Slot slot) : this()
        {
            SetNavigation(slot.LocationAdress);
        }

        public Navigation(Budynki.ViewModel viewModel)
        {
            SetNavigation(viewModel.Value);
        }

        public Navigation(Budynki2.ViewModel viewModel)
        {
            SetNavigation(viewModel.Value);
        }

        private void SetNavigation(string address)
        {
            if (string.IsNullOrEmpty(address))
                address = "50.019312, 21.989142";

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(
                      new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case TargetPlatform.Android:
                    //https://developers.google.com/maps/documentation/android-api/intents
                    if (string.IsNullOrEmpty(address))
                        Device.OpenUri(
                          new Uri(string.Format("geo:50.019312, 21.989142")));//new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                    else
                    {
                        Device.OpenUri(
                          new Uri(string.Format("google.navigation:q={0}", address)));
                    }
                    break;
                case TargetPlatform.Windows:
                case TargetPlatform.WinPhone:
                    Device.OpenUri(
                      new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(address))));
                    break;
            }
            try
            {
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
                {
                    txtNiemaza.Text = "Aby powrócic do menu, \n kliknij przycisk wstecz";
                }
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                {
                    txtNiemaza.Text = "If you want back to menu \n please click Back button on your phone";
                }
            }
            catch
            {

            }
            

        }
    }
}
