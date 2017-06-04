using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI2projekt.Plan;
using Xamarin.Forms;

namespace SI2projekt
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            var tapImage1 = new TapGestureRecognizer();
            //Binding events  
            tapImage1.Tapped += tapImage_Tapped1;
            //Associating tap events to the image buttons  
            img.GestureRecognizers.Add(tapImage1);

            this.BackgroundColor = Color.Black;
            btnLogin.FontAttributes = btnGosc.FontAttributes = txtUser.FontAttributes = txtPassword.FontAttributes = lblApp2.FontAttributes = FontAttributes.Bold;
           
        }

        void tapImage_Tapped1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://w.prz.edu.pl"));
        }
        public void langpol()
        {
            lblApp.Text = "Aplikacja Student";
            lblApp2.Text = "Logowanie";
            lblUser.Text = "Użytkownik:";
            lblPassword.Text = "Hasło:";
            txtUser.Placeholder = "Nazwa użytkownika";
            txtPassword.Placeholder = "Hasło";
            btnLogin.Text = "Zaloguj";
            btnGosc.Text = "Gość";
        }
        public void langeng()
        {
            lblApp.Text = "Student application";
            lblApp2.Text = "Login";
            lblUser.Text = "Username:";
            lblPassword.Text = "Password:";
            txtUser.Placeholder = "Username";
            txtPassword.Placeholder = "Password";
            btnLogin.Text = "Login";
            btnGosc.Text = "Guest";
        }
        public void schemastandard()
        {
            this.BackgroundColor = btnLogin.BackgroundColor = btnGosc.BackgroundColor = Color.Black;
            lblApp.TextColor = lblApp2.TextColor = lblUser.TextColor = lblPassword.TextColor = btnLogin.TextColor = btnGosc.TextColor = btnGosc.BorderColor = btnLogin.BorderColor = Color.White;
            txtUser.TextColor = txtPassword.TextColor = Color.White;
            btnLogin.FontSize = btnGosc.FontSize = 30;
            btnGosc.BorderWidth = btnLogin.BorderWidth = 2;
            lblApp.FontSize = lblApp2.FontSize =  30;
            lblUser.FontSize = lblPassword.FontSize = txtUser.FontSize = txtPassword.FontSize = 25;
        }
        public void schemacontrast()
        {
            this.BackgroundColor = btnLogin.BackgroundColor = btnGosc.BackgroundColor = Color.Yellow;
            lblApp.TextColor = lblApp2.TextColor = lblUser.TextColor = lblPassword.TextColor = txtUser.TextColor = txtPassword.TextColor = btnLogin.TextColor = btnGosc.TextColor = btnGosc.BorderColor = btnLogin.BorderColor = Color.Black;
            //txtPassword.BackgroundColor = txtUser.BackgroundColor = Color.Yellow;
            btnGosc.BorderWidth = btnLogin.BorderWidth = 4;
            btnLogin.FontSize = btnGosc.FontSize = 30;
            lblApp.FontSize = lblApp2.FontSize = 30;
            lblUser.FontSize = lblPassword.FontSize = 28;
            txtUser.PlaceholderColor = txtPassword.PlaceholderColor = Color.Gray;
            txtPassword.FontSize = txtUser.FontSize = 25;
        }

        protected override void OnAppearing()
        {
            if (App.GetApp().AppSettings.ContainsKey("language")==false || (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL"))
            {
                langpol();
            }
            if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
            {
                langeng();
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == false || (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "standard"))
            {
                schemastandard();
            }
            if (App.GetApp().AppSettings.ContainsKey("colorschema") == true && App.GetApp().AppSettings.Data["colorschema"] == "contrast")
            {
                schemacontrast();
            }
            base.OnAppearing();
            txtUser.Text = "";
            txtPassword.Text = "";
            txtStatus.Text = "";
            if (App.GetApp().AppSettings.ContainsKey("autologin") == true && App.GetApp().AppSettings.Data["autologin"] == "true")
            {
                App.GetApp().MainPage = new NavigationPage(new StartPage(false, false));
            }
        }
        private void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Text = string.Empty;
                if (Grabber.LoginSuccess(txtUser.Text, txtPassword.Text))
                {
                    if (App.GetApp().AppSettings.Data["User"] != txtUser.Text)
                    {
                        App.GetApp().ScheduleRepository.clear();
                    }
                    App.GetApp().AppSettings.Data["User"] = txtUser.Text;
                    App.GetApp().AppSettings.Data["Password"] = txtPassword.Text;
                    //App.GetApp().AppSettings.AddOrUpdate("zalogowany", "true");
                    //App.GetApp().AppSettings.Save();
                    Navigation.PushAsync(new StartPage(false, true), true);
                }
                else
                {
                    if (App.GetApp().AppSettings.ContainsKey("language") == false || (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL"))
                    {
                        DisplayAlert("INFORMACJA", "Błędny login lub hasło", "OK");
                    }
                    if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                    {
                        DisplayAlert("INFORMATION", "Wrong username or password", "OK");
                    }
                    //txtStatus.Text = "Bledny login lub haslo";
                }
            }
            catch
            {
                if (App.GetApp().AppSettings.ContainsKey("language") == false || (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "PL"))
                {
                    DisplayAlert("INFORMACJA", "Brak połączenia z siecią", "OK");
                }
                if (App.GetApp().AppSettings.ContainsKey("language") == true && App.GetApp().AppSettings.Data["language"] == "ENG")
                {
                    DisplayAlert("INFORMATION", "No network connection", "OK");
                }
                //txtStatus.Text = "brak polaczenia z siecia";
            }
        }
        private void BtnGosc_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StartPage(true, false), true);
            return;
        }
    }
}
