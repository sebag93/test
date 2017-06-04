using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
  public partial class Settings : ContentPage
  {
    public Settings()
    {
      InitializeComponent();
      txtUser.Text = App.GetApp().AppSettings.Get("User");
      txtPassword.Text = App.GetApp().AppSettings.Get("Password");
    }

    private void BtnSave_OnClicked(object sender, EventArgs e)
    {
      if (txtPassword.Text != txtPassword2.Text)
      {
        lblError.Text = "Hasla musza byc takie same";
        return;

      }
      App.GetApp().AppSettings.AddOrUpdate("User", txtUser.Text);
      App.GetApp().AppSettings.AddOrUpdate("Password", txtPassword.Text);
      try
      {
        App.GetApp().AppSettings.Save();
      }
      catch (Exception ex)
      {
        
      }
      Navigation.PopAsync(true);
    }
  }
}
