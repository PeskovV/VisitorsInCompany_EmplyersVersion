using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsInCompany.Helpers
{
   public class INIManager
   {
      private const int SIZE = 1024; //Максимальный размер (для чтения значения из файла)
      private string _path = null; //Для хранения пути к INI-файлу

      public INIManager(string path)
      {
         _path = path;
      }

      public string Path { get => _path; }

      public string GetPrivateString(string section, string key)
      {
         StringBuilder buffer = new StringBuilder(SIZE);
         GetPrivateString(section, key, null, buffer, SIZE, _path);
         return buffer.ToString();
      }

      public void WritePrivateString(string section, string key, string value)
      {
         WritePrivateString(section, key, value, _path);
      }

      //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
      [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
      private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

      //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
      [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
      private static extern int WritePrivateString(string section, string key, string str, string path);
   }
}
