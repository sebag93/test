using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace SI2projekt.Plan
{
    public class Grabber
    {
        const string page = "http://wu.pwste.edu.pl/WU/logon.jsp";

        private static string GetPageContent(string user, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string format =
                    string.Format("{0}?login={1}&password={2}&logon=Zaloguj&return=%2FWU%2Fschedule.jsp%3FfromRedirect%3Dtrue",
                    page, WebUtility.UrlEncode(user), WebUtility.UrlEncode(password));//sprawdza url zeby stwierdzic poprawnosc logowania
                var response = client.GetAsync(new Uri(format)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var pageContent = response.Content.ReadAsStringAsync().Result;
                    pageContent = pageContent.Replace("&#322;", "ł");
                    pageContent = pageContent.Replace("&#324;", "ń");
                    pageContent = pageContent.Replace("&#261;", "ą");
                    pageContent = pageContent.Replace("&oacute;", "ó");

                    pageContent = pageContent.Replace("&#263;", "ć");
                    pageContent = pageContent.Replace("&#281;", "ę");
                    pageContent = pageContent.Replace("&#347;", "ś");
                    pageContent = pageContent.Replace("&#378;", "ź");
                    pageContent = pageContent.Replace("&#380;", "ż");


                    return pageContent;
                }
            }
            return null;
        }

        public static bool LoginSuccess(string user, string password)
        {
            string pageContent = GetPageContent(user, password);
            if (pageContent.Contains(@"class=""error"""))
                return false;

            return true;

        }
        public static List<Slot> Grab(string user, string password)
        {
           
            List<Slot> tmp = new List<Slot>();//Slot pojedyncze zajecia

            string pageContent = GetPageContent(user, password);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);
            var tab = document.DocumentNode
              .Descendants("table")   //Zwraca przefiltrowane kolekcję elementów podrzędnych dla tego dokumentu lub elementu, w kolejności dokumentu. Tylko elementy, które ma pasującego XName znajdują się w kolekcji.
              .Where(d => d.Attributes.Contains("class") && 
              d.Attributes["class"].Value.Contains("dataTable")).
              FirstOrDefault();

            if (tab != null)
            {
                foreach (var row in tab.ChildNodes)
                {
                    string name = string.Empty;
                    string dates = string.Empty;
                    string startTime = string.Empty;
                    string endTime = string.Empty;
                    string room = string.Empty;
                    int cell = 0;

                    if (row.Name == "tr")
                    {
                        bool header = false;
                        foreach (var column in row.ChildNodes)
                        {
                            if (column.Name == "th")
                            {
                                header = true;
                                break;
                            }
                            if (column.Name == "td")
                            {
                                switch (cell)
                                {
                                    case 1:
                                        name = column.InnerText;
                                        break;
                                    case 2:
                                        dates = column.InnerText;
                                        break;
                                    case 4:
                                        startTime = column.InnerText;
                                        break;
                                    case 5:
                                        endTime = column.InnerText;
                                        break;
                                    case 6:
                                        room = column.InnerText;
                                        break;
                                }
                                cell++;
                            }



                        }
                        if (header == true)
                            continue;
                        foreach (var date in dates.Split(' '))
                        {
                            string adres = "50.012190, 22.673114";
                            if (room.Contains("Budynek IIT"))
                                adres = "50.012190, 22.673114";
                            if (room.Contains("Budynek J1"))
                                adres = "50.012720, 22.673061";
                            if (room.Contains("Budynek J2"))
                                adres = "50.012551, 22.672189";

                            if (room.Contains("Budynek J3"))
                                adres = "50.012379, 22.671331";
                            if (room.Contains("Budynek J4"))
                                adres = "50.012203, 22.670478";
                            if (room.Contains("Hala sportowa"))
                                adres = "50.013283, 22.673329";

                            if (room.Contains("Budynek P"))
                                adres = "50.012032, 22.671494";
                            if (room.Contains("Budynek IEiZ"))
                                adres = "50.012270, 22.672562";
                            if (room.Contains("Budynek A"))
                                adres = "50.010273, 22.673660";

                            if (room.Contains("Budynek B"))
                                adres = "50.009959, 22.673478";
                            if (room.Contains("Budynek C"))
                                adres = "50.010065, 22.672634";
                            if (room.Contains("Budynek D"))
                                adres = "50.010459, 22.672850";
                            //reszta budynkwo 
                            tmp.Add(new Slot() { Name = name.Remove(name.IndexOf('\n')).Trim(), Location = room, LocationAdress = adres, Start = DateTime.Parse(date + " " + startTime), Stop = DateTime.Parse(date + " " + endTime) });
                        }
                    }
                }
            }
            return tmp;
        }

    }


}
