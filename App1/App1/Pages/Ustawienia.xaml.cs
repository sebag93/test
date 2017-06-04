using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SI2projekt
{
    public partial class Ustawienia : ContentPage
    {
        public Ustawienia()
        {
            InitializeComponent();
            btnBack.IsVisible = false;
            var tapImage1 = new TapGestureRecognizer();
            //Binding events  
            tapImage1.Tapped += tapImage_Tapped1;
            //Associating tap events to the image buttons  
            img.GestureRecognizers.Add(tapImage1);
            btnPolish.FontAttributes = btnEnglish.FontAttributes = btnContrast.FontAttributes = btnStandard.FontAttributes = btnSave.FontAttributes = btnBack.FontAttributes = lblLanguage.FontAttributes = lblColorVersion.FontAttributes = FontAttributes.Bold;
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                schemastandard();
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                schemacontrast();
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == false)
            {
                DisplayAlert("Informacje/Information", "Pierwsze uruchomienie aplikacji... Proszę wybrać język a następnie zapisać zmiany. \n\nFirst run application... Please select your language and saves the changes.", "OK");
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL")
            {
                langpol();
                btnSave.IsVisible = false;
                btnBack.IsVisible = true;
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
            {
                langeng();
                btnSave.IsVisible = false;
                btnBack.IsVisible = true;
            }
        }
        void tapImage_Tapped1(object sender, EventArgs e)
        {
            if ((App.GetApp().AppSettings.ContainsKey("language") == false) || (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL"))
            {
                DisplayAlert("Informacje", "Autorami aplikacji są Artur Buś oraz Sebastian Guzdek. Wszelkie uwagi dotyczące aplikacji proszę zgłaszać na adres e-mail: abuscitybus@gmail.com", "OK");
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
            {
                DisplayAlert("Information", "The authors of the application are Artur Buś and Sebastian Guzdek. Any comments regarding the application, please report to the email address: abuscitybus@gmail.com", "OK");
            }
        }

        public void langpol()
        {
            lblLanguage.Text = "Wybierz język:";
            lblColorVersion.Text = "Schemat kolorów:";
            btnContrast.Text = "Kontrast";
            btnStandard.Text = "Domyślny";
            btnSave.Text = "Zapisz";
            btnBack.Text = "Wróć";
        }
        public void langeng()
        {
            lblLanguage.Text = "Choose language:";
            lblColorVersion.Text = "Color scheme:";
            btnContrast.Text = "Contrast";
            btnStandard.Text = "Default";
            btnSave.Text = "Save";
            btnBack.Text = "Back";
        }
        public void schemastandard()
        {
            this.BackgroundColor = Color.Black;
            lblLanguage.TextColor = lblColorVersion.TextColor = btnEnglish.TextColor = btnPolish.TextColor = btnStandard.TextColor = btnContrast.TextColor = btnSave.TextColor = btnBack.TextColor = Color.White;
            btnEnglish.BorderColor = btnPolish.BorderColor = btnContrast.BorderColor = btnStandard.BorderColor = btnSave.BorderColor = btnBack.BorderColor = Color.White;
            btnPolish.FontSize = btnEnglish.FontSize = btnContrast.FontSize = btnStandard.FontSize = btnSave.FontSize = btnBack.FontSize = lblLanguage.FontSize = lblColorVersion.FontSize = 20;
            btnPolish.BorderWidth = btnEnglish.BorderWidth = btnContrast.BorderWidth = btnStandard.BorderWidth = btnSave.BorderWidth = btnBack.BorderWidth = 2;
            btnPolish.BackgroundColor = btnEnglish.BackgroundColor = btnSave.BackgroundColor = btnBack.BackgroundColor = btnContrast.BackgroundColor = btnStandard.BackgroundColor = Color.Black;
        }
        public void schemacontrast()
        {
            this.BackgroundColor = Color.Yellow;
            lblLanguage.TextColor = lblColorVersion.TextColor = btnEnglish.TextColor = btnPolish.TextColor = btnStandard.TextColor = btnContrast.TextColor = btnSave.TextColor = btnBack.TextColor = Color.Black;
            btnEnglish.BorderColor = btnPolish.BorderColor = btnContrast.BorderColor = btnStandard.BorderColor = btnSave.BorderColor = btnSave.BorderColor = btnBack.BorderColor = Color.Black;
            btnPolish.FontSize = btnEnglish.FontSize = btnContrast.FontSize = btnStandard.FontSize = btnSave.FontSize = btnBack.FontSize = lblLanguage.FontSize = lblColorVersion.FontSize = 22;
            btnPolish.BorderWidth = btnEnglish.BorderWidth = btnContrast.BorderWidth = btnStandard.BorderWidth = btnSave.BorderWidth = btnBack.BorderWidth = 4;
            btnPolish.BackgroundColor = btnEnglish.BackgroundColor = btnSave.BackgroundColor = btnBack.BackgroundColor = btnStandard.BackgroundColor = btnContrast.BackgroundColor = Color.Yellow;
        }
        public void btnPolish_OnClicked(object sender, EventArgs e)
        {
            langpol();
            App.GetApp().AppSettings.AddOrUpdate("language", "PL");
            App.GetApp().AppSettings.Save();
            DisplayAlert("", "Język aplikacji został zmieniony na polski.", "OK");
        }
        public void btnEnglish_OnClicked(object sender, EventArgs e)
        {
            langeng();
            App.GetApp().AppSettings.AddOrUpdate("language", "ENG"); 
            App.GetApp().AppSettings.Save();
            DisplayAlert("", "Application language has been changed to English.", "OK");
        }
        public void btnStandard_OnClicked(object sender, EventArgs e)
        {
            schemastandard();
            App.GetApp().AppSettings.AddOrUpdate("colorschema", "standard");
            App.GetApp().AppSettings.Save();
        }
        public void btnContrast_OnClicked(object sender, EventArgs e)
        {
            schemacontrast();
            App.GetApp().AppSettings.AddOrUpdate("colorschema", "contrast");
            App.GetApp().AppSettings.Save();
        }
        public async void btnSave_OnClicked(object sender, EventArgs e)
        {
            if (App.GetApp().AppSettings.ContainsKey("language") == false)
            {
                App.GetApp().AppSettings.AddOrUpdate("language", "PL");
                App.GetApp().AppSettings.Save();
            }
            await Navigation.PushAsync(new LoginPage());
        }
        public async void btnBack_OnClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < Navigation.NavigationStack.Count; i++)//zdejmujemy ze stosu wszystkie otwarte strony by nie było do nich powrotu
            {
                await Navigation.PopAsync();//popasync zdjecie ze stosu
            }
            await Navigation.PushAsync(new StartPage(false, false));
            //if (App.GetApp().AppSettings.ContainsKey("zalogowany") == true && App.GetApp().AppSettings.Data["zalogowany"] == "true")
            //{
            //    await Navigation.PushAsync(new StartPage(false,false));
            //}
            //if (App.GetApp().AppSettings.ContainsKey("autologin") == false || (App.GetApp().AppSettings.ContainsKey("autologin") == true && App.GetApp().AppSettings.Data["autologin"] == "false"))
            //{
            //    await Navigation.PushAsync(new StartPage(true, false));
            //}
        }
    }    
}
