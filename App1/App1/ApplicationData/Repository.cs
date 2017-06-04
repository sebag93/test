using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SI2projekt.ApplicationData//obiekt przechowuje zapisane dane z pliku IOOperations//wzoprzec projektowy repozytorium
{
  public class Repository<T>
  {
    public Dictionary<string, T> Data { get; private set; }//derinicja kluczy i wartości//reprezentuje kolekcje kluczy i wartości
    public string Name { get; private set; }

    internal T Get(string key)
    {
      return Data[key];
    }

    public Repository(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException("Nazwa nie moze byc pusta");
      Data = new Dictionary<string, T>();
      Name = name;
    }

    public bool ContainsKey(string name)
    {
      return Data.ContainsKey(name);
    }

    internal void Add(string key, T value)
    {
      Data.Add(key, value);
    }
        internal void clear()
        {
            Data.Clear();
        }
    internal void AddOrUpdate(string key, T value)
    {
      Data[key] = value;
    }

    public void Save()
    {
      string DataToSave = JsonConvert.SerializeObject(Data, Formatting.Indented);
      IOOperations.PCLCreateFile(Name + ".txt", DataToSave);
    }


    public static Repository<T> Load(string name)
    {
      Repository<T> tmp = new Repository<T>(name);
      string data = IOOperations.PCLReadFile(name + ".txt");
      tmp.Data = JsonConvert.DeserializeObject<Dictionary<string, T>>(data);
      if (tmp.Data == null)
        tmp.Data = new Dictionary<string, T>();
      return tmp;
    }


  }
}
