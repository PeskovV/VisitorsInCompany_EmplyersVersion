
namespace VisitorsInCompany.Helpers
{
    using System.Runtime.InteropServices;
    using System.Text;

    public class INIManager
    {
        #region System import

        //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

        //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);

        #endregion

        //Максимальный размер (для чтения значения из файла)
        private const int _size = 1024; 

        public string Path { get; } = null;

        public INIManager(string path)
        {
            Path = path;
        }

        public string GetPrivateString(string section, string key)
        {
            StringBuilder buffer = new StringBuilder(_size);
            GetPrivateString(section, key, null, buffer, _size, Path);
            return buffer.ToString();
        }

        public void WritePrivateString(string section, string key, string value)=>
            WritePrivateString(section, key, value, Path);
    }
}
