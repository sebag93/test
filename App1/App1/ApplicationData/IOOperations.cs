using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2projekt.ApplicationData//implentacja metod zapisywanie i odczytywanie plików tekstowych 
{
  public class IOOperations
  {
    public static void PCLCreateFile(string fileName, string content)
    {
      IFolder localStorage = FileSystem.Current.LocalStorage;
      IFile file = localStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).Result;
      file.WriteAllTextAsync(content);
    }
    public static string PCLReadFile(string fileName)
    {
      IFolder localStorage = FileSystem.Current.LocalStorage;
      if (PCLCheckFileExists(fileName).Result)
      {
        IFile file = localStorage.GetFileAsync(fileName).Result;
        //return file.ReadAllTextAsync().Result; //This wouldn't work. You will hit deadlock. 
        return ReadFile(file, fileName).Result;
      }
      return string.Empty;
    }

    private static async Task<bool> PCLCheckFileExists(string fileName)
    {
      IFolder localStorage = FileSystem.Current.LocalStorage;
      return
        await
          Task.Run(() => localStorage.CheckExistsAsync(fileName).Result == ExistenceCheckResult.FileExists)
            .ConfigureAwait(false);
    }

    public static async Task<string> ReadFile(IFile f, string fileName)
    {
      return await Task.Run(() => f.ReadAllTextAsync()).ConfigureAwait(false);
    }
  }
}
