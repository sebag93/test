using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI2projekt.ApplicationData;
using SI2projekt.Plan;
using Xamarin.Forms;

namespace SI2projekt
{
  public partial class App : Application
  {
    public static App GetApp()
    {
      return Application.Current as App;
    }
    public Repository<string> AppSettings { get; private set; }
    public Repository<Slot> ScheduleRepository { get; private set; }

    public App()
    {
      InitializeComponent();
      // The root page of your application
      MainPage = new NavigationPage(new LoginPage());

    }

    protected override void OnStart()
    {
            // Handle when your app starts
            AppSettings = Repository<string>.Load("AppSettings");
            if (AppSettings.ContainsKey("User") == false)
            {
                AppSettings.Add("User", "");
            }
            if (AppSettings.ContainsKey("Password") == false)
            {
                AppSettings.Add("Password", "");
            }

            if (AppSettings.ContainsKey("language") == false)
            {
                MainPage = new NavigationPage(new Ustawienia());
            }
            if (AppSettings.ContainsKey("autologin")==true && AppSettings.Data["autologin"]=="true")
            {
                MainPage = new NavigationPage(new StartPage(false,false));
            }
            ScheduleRepository = Repository<Slot>.Load("Schedule");//ladowanie z dysku planu

    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
      AppSettings.Save();
      ScheduleRepository.Save();//zapis na dysku z bazy 
    }

    protected override void OnResume()
    {
            // Handle when your app resumes
            if (MainPage.Navigation.NavigationStack.Last().GetType() == typeof(Navigation))
            {
                MainPage.Navigation.RemovePage(MainPage.Navigation.NavigationStack.Last());
            }
        }
  }
}
