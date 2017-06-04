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
using static SI2projekt.Budynki;

namespace SI2projekt
{
    public partial class DaneZeStrony : ContentPage
    {

       
        private static ObservableCollection<ViewModel>
    buldings = new ObservableCollection<ViewModel>();
        public DaneZeStrony()
        {
            buldings.Clear();

            UpdateData();
            InitializeComponent();
            this.BackgroundColor = Color.Black;
            lstBuldings.ItemsSource = buldings;
            BindingContext = this;
            
        }
        void UpdateData()
        {
            try
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
                          
                            {
                            }
                        }
                    }
                }

            }
            catch
            {
              
            }
            }
        private void LstBuldings_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new Navigation(e.Item as ViewModel));
        }
    }
}
