using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI2projekt.ApplicationData;
using SI2projekt.Plan;
using Xamarin.Forms;

namespace SI2projekt
{
    public partial class PlanPage : ContentPage
    {
        private DateTime SelectedDay;
        private Repository<Slot> ScheduleRepository;
        double deltax = 0;
        public PlanPage()
        {
            InitializeComponent();
            SelectedDay = DateTime.Now;
            ScheduleRepository = App.GetApp().ScheduleRepository;
            var pan = new PanGestureRecognizer();
            pan.PanUpdated += PanGesture_PanUpdated;
            ScheduleLayout.GestureRecognizers.Add(pan);
            glowny.GestureRecognizers.Add(pan);

            //var panGesture = new PanGestureRecognizer();
            //panGesture.PanUpdated += PanGesture_PanUpdated;
            //lsvPlan.GestureRecognizers.Add(panGesture);
            UpdateData();
        }
        public void schemastandard()
        {
            this.BackgroundColor = Color.Black;
            lblStatus.TextColor = lblSwipe.TextColor = star.TextColor = data.TextColor = schedule.TextColor = Color.White;
        }
        public void schemacontrast()
        {
            this.BackgroundColor = Color.Yellow;
            lblStatus.TextColor = lblSwipe.TextColor = star.TextColor = data.TextColor = schedule.TextColor = Color.Black;
        }
        public void langpol()
        {
            schedule.Text = "Plan zajęć";
            lblSwipe.Text = "--- Przesuń palcem w prawo lub lewo +++";
        }
        public void langeng()
        {
            schedule.Text = "Schedule";
            lblSwipe.Text = "-Swipe to the right or left+";
        }

        protected override void OnAppearing()//dzikei metodzie tej możliwe jest załadowanie okreslonych zdarzen wtrakcie ładowania aplikacji
        {
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                schemastandard();
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                schemacontrast();
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
            {
                langpol();
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
            {
                langeng();
            }
            base.OnAppearing();
            UpdateData();
        }

        private void PanGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if(e.StatusType== GestureStatus.Running)//sprawdzamy czy sie przesunelo
            {
                deltax = e.TotalX;//zczytujemy
            }
            if (e.StatusType != GestureStatus.Completed)//jesli sie gesturestatus zakonczy zmieniamy dzien
                return;

            if (deltax > 0)
                SelectedDay = SelectedDay.AddDays(-1);
            else
            {
                SelectedDay = SelectedDay.AddDays(1);
            }
            deltax = 0;
            data.Text = SelectedDay.ToString("dd-MM-yyyy");
            UpdateData();
        }

        void UpdateData()
        {
            //lsvPlan.Header = SelectedDay.TranslateToPolish();
            try
            {
                if (ScheduleRepository.Data.Values.All(x => x.Start.Date < DateTime.Now.Date))//sprawdzamy czy plan jest aktualny
                {
                    //trzeba zaktualizowac plan zajec
                    var schedule = Grabber.Grab(App.GetApp().AppSettings.Get("User"),
                                                App.GetApp().AppSettings.Get("Password"));
                    for (int i = 0; i < schedule.Count; i++)
                        ScheduleRepository.Add(i.ToString(), schedule[i]);
                    ScheduleRepository.Save();
                }
                var tmp = new ObservableCollection<Slot>();//Reprezentuje kolekcję danych dynamicznych, który dostarcza powiadomienia, gdy elementy zostaną dodawane, usunięte, lub gdy cała kolekcja jest odświeżana
                foreach (var val in ScheduleRepository.Data.Values)
                {
                    if (val.Start.Date == SelectedDay.Date)
                    {
                        tmp.Add(val);
                    }
                }
                if (tmp.Count > 0)
                {
                    lblStatus.Text = "";
                    ShowItems(tmp);
                }
                else
                {
                    //lsvPlan.IsVisible = false;
                    if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
                    {
                        lblStatus.Text = "Dzisiaj nie ma zajec :)";
                    }
                    if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                    {
                        lblStatus.Text = "Today you have free day :)";
                    }
                    ScheduleLayout.Children.Clear();

                }
            }



               
              catch
            {
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
                {
                    lblStatus.Text = "Brak Harmonogramu :)";
                }
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                {
                    lblStatus.Text = "no schedule :)";
                }

            }




            }

        private void ShowItems(ObservableCollection<Slot> tmp)
        {
            ScheduleLayout.Children.Clear();
            foreach (var slot in tmp)
            {
                StackLayout horizontal = new StackLayout();
                horizontal.GestureRecognizers.Add(new TapGestureRecognizer()
                { Command = new Command((x) => NavigateTo(x)),
                    CommandParameter = slot });
                horizontal.Orientation = StackOrientation.Horizontal;
                if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
                {
                    horizontal.Children.Add(new Label()
                    {

                        Text = slot.Start.ToString("HH:mm"),
                        FontAttributes = FontAttributes.Bold,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.White
                    });
                }
                if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast"))
                {
                    horizontal.Children.Add(new Label()
                    {

                        Text = slot.Start.ToString("HH:mm"),
                        FontAttributes = FontAttributes.Bold,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Black
                    });
                }
                StackLayout vertical = new StackLayout();
                if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
                {
                    vertical.Children.Add(new Label() { Text = slot.Name, TextColor = Color.White ,FontAttributes = FontAttributes.Bold, LineBreakMode = LineBreakMode.WordWrap });
                    vertical.Children.Add(new Label() { Text = slot.Location, TextColor = Color.White });
                }
                if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
                {
                    vertical.Children.Add(new Label() { Text = slot.Name, TextColor = Color.Black ,FontAttributes = FontAttributes.Bold, LineBreakMode = LineBreakMode.WordWrap });
                    vertical.Children.Add(new Label() { Text = slot.Location, TextColor = Color.Black });
                }
                
                horizontal.Children.Add(vertical);
                ScheduleLayout.Children.Add(horizontal);

            }
        }

        private void NavigateTo(object x)
        {
            Slot slot = x as Slot;

            Navigation.PushAsync(new Navigation(slot), true);
        }
    }
}
