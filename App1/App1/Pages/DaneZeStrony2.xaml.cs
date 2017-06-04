using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using static SI2projekt.Budynki2;

namespace SI2projekt
{
    public partial class DaneZeStrony2 : ContentPage
    {
        private static ObservableCollection<ViewModel>
        buldings = new ObservableCollection<ViewModel>();
        public DaneZeStrony2()
        {
            buldings.Clear();
            InitializeComponent();
            this.BackgroundColor = Color.Yellow;
            UpdateData();
            lstBuldings.ItemsSource = buldings;
            BindingContext = this;
        }
        void UpdateData()
        {

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(new Uri("http://arturtest.cba.pl/dane.php")).Result;
                var pageContent = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(pageContent);
                    var tab = document.GetElementbyId("dane");
                    var dane = tab.InnerHtml.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in dane)
                    {
                        try
                        {
                            var d = item.Split(';');
                            buldings.Add(new ViewModel(d[0], string.Format("{0}, {1}", d[1], d[2])));
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }

        }
        private void LstBuldings_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new Navigation(e.Item as ViewModel));
        }
    }
}
