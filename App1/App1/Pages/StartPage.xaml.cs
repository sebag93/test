using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SI2projekt
{
    public partial class StartPage : ContentPage
    {
        bool Gosc;
        public StartPage(bool gosc, bool aktywny)
        {
            InitializeComponent();
            btnPlan.FontAttributes = btnBuldings.FontAttributes = btnNav.FontAttributes = btnWyloguj.FontAttributes = btnZapisz.FontAttributes = btnUstawienia.FontAttributes = btnPOI.FontAttributes = FontAttributes.Bold;
            
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromMinutes(1), UpdateTimeData);//urochomienie metody która co minute bedzie uruchamiała metode updatetime
            
            if (gosc == true)
            {
                btnPlan.IsEnabled = false;
                btnNav.IsEnabled = false;
                btnZapisz.IsEnabled = false;
                Gosc = true;
            }
            if (gosc != true)
            {
                lblApp.Text = "Aplikacja Student";
            }
            if (aktywny == false)
            {
                btnZapisz.IsEnabled = false;
            }
        }
        public void langpol()
        {
            btnPlan.Text = "Plan zajęć";
            btnBuldings.Text = "Budynki";
            btnNav.Text = "Nawiguj";
            btnZapisz.Text = "Zapisz profil";
            btnUstawienia.Text = "Ustawienia";
            btnWyloguj.Text = "Wyloguj";
        }
        public void langeng()
        {
            btnPlan.Text = "Schedule";
            btnBuldings.Text = "Buildings";
            btnNav.Text = "Navigate";
            btnZapisz.Text = "Save profile";
            btnUstawienia.Text = "Settings";
            btnWyloguj.Text = "Logout";
        }
        public void schemastandard()
        {
            this.BackgroundColor = btnPlan.BackgroundColor = btnBuldings.BackgroundColor = btnNav.BackgroundColor = btnPOI.BackgroundColor = btnZapisz.BackgroundColor = btnUstawienia.BackgroundColor = btnWyloguj.BackgroundColor = Color.Black;
            lblApp.TextColor = lblDayOfWeek.TextColor = lblTimer.TextColor = btnPlan.TextColor = btnPlan.BorderColor = btnBuldings.TextColor = btnBuldings.BorderColor = btnNav.TextColor = btnNav.BorderColor = btnPOI.TextColor = btnPOI.BorderColor = btnZapisz.TextColor = btnZapisz.BorderColor = btnUstawienia.TextColor = btnUstawienia.BorderColor = btnWyloguj.TextColor = btnWyloguj.BorderColor = Color.White;
            btnPlan.BorderWidth = btnWyloguj.BorderWidth = btnBuldings.BorderWidth = btnNav.BorderWidth = btnZapisz.BorderWidth = btnUstawienia.BorderWidth = btnPOI.BorderWidth = 2;
            btnPlan.FontSize = btnBuldings.FontSize = btnNav.FontSize = btnWyloguj.FontSize = btnZapisz.FontSize = btnPOI.FontSize = btnUstawienia.FontSize = 22;
        }
        public void schemacontrast()
        {
            this.BackgroundColor = btnPlan.BackgroundColor = btnBuldings.BackgroundColor = btnNav.BackgroundColor = btnPOI.BackgroundColor = btnZapisz.BackgroundColor = btnUstawienia.BackgroundColor = btnWyloguj.BackgroundColor = Color.Yellow;
            lblApp.TextColor = lblDayOfWeek.TextColor = lblTimer.TextColor = btnPlan.TextColor = btnPlan.BorderColor = btnBuldings.TextColor = btnBuldings.BorderColor = btnNav.TextColor = btnNav.BorderColor = btnPOI.TextColor = btnPOI.BorderColor = btnZapisz.TextColor = btnZapisz.BorderColor = btnUstawienia.TextColor = btnUstawienia.BorderColor = btnWyloguj.TextColor = btnWyloguj.BorderColor = Color.Black;
            btnPlan.BorderWidth = btnWyloguj.BorderWidth = btnBuldings.BorderWidth = btnNav.BorderWidth = btnZapisz.BorderWidth = btnUstawienia.BorderWidth = btnPOI.BorderWidth = 4;
            btnPlan.FontSize = btnBuldings.FontSize = btnNav.FontSize = btnWyloguj.FontSize = btnZapisz.FontSize = btnPOI.FontSize = btnUstawienia.FontSize = 26;
        }

        private bool UpdateTimeData()
        {
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
            {
                lblDayOfWeek.Text = DateTime.Now.DayOfWeek.TranslateToPolish();
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
            {
                lblDayOfWeek.Text = DateTime.Now.DayOfWeek.TranslateToEnglish();
            }
            lblTimer.Text = DateTime.Now.ToString("H:mm");
            //zawsze zwraca true, zeby timer chodzil caly czas
            return true;
        }
        private async void BtnPlan_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlanPage(), true);
        }
        private async void BtnNav_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Navigation(), true);
        }
        private async void BtnUstawienia_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ustawienia(), true);
        }

        private async void BtnBuldings_OnClicked(object sender, EventArgs e)
        {
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                await Navigation.PushAsync(new Budynki(), true);
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                await Navigation.PushAsync(new Budynki2(), true);
            }
        }
        private async void BtnWyloguj_OnClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < Navigation.NavigationStack.Count; i++)//zdejmujemy ze stosu wszystkie otwarte strony by nie było do nich powrotu
            {
                await Navigation.PopAsync();//popasync zdjecie ze stosu
            }
            App.GetApp().AppSettings.AddOrUpdate("autologin", "false");//dodanie w pamieci 
            App.GetApp().AppSettings.Save();
            App.GetApp().ScheduleRepository.clear();
            App.GetApp().ScheduleRepository.Save();
            await Navigation.PushAsync(new LoginPage(), true);
        }
        private void BtnZapisz_OnClicked(object sender, EventArgs e)
        {
            //for (int i = 1; i == Navigation.NavigationStack.Count; i++)//zdejmujemy ze stosu wszystkie otwarte strony by nie było do nich powrotu
            //{
            //    Navigation.PopAsync();//popasync zdjecie ze stosu
            //}

            App.GetApp().AppSettings.AddOrUpdate("autologin", "true");//dodanie w pamieci 
            App.GetApp().AppSettings.Save();
            btnZapisz.IsEnabled = false;
        }
        private async void BtnExt_OnClicked(object sender, EventArgs e)
        {
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                await Navigation.PushAsync(new DaneZeStrony(), true);
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                await Navigation.PushAsync(new DaneZeStrony2(), true);
            }
            
        }
      
        protected override void OnAppearing()
        {
            UpdateTimeData();
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                schemastandard();
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                schemacontrast();
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == false || (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL"))
            {
                langpol();
                if (Gosc == true)
                {
                    lblApp.Text = "Konto Gościa";
                }
                else
                {
                    lblApp.Text = "Aplikacja Student";
                }
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
            {
                langeng();
                if (Gosc == true)
                {
                    lblApp.Text = "Guest account";
                }
                else
                {
                    lblApp.Text = "Student application";
                }
            }
        }
    }
}
