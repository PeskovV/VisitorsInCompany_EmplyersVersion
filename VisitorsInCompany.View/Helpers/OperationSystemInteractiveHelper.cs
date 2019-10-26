using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsInCompany.Helpers
{
   public static class OperationSystemInteractiveHelper
   {
      #region System import

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
      [DllImport("user32.dll", SetLastError = true)]
      public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
      [DllImport("user32.dll")]
      public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);



      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(IntPtr hWnd,
          UInt32 Msg,
          IntPtr wParam,
          IntPtr lParam);
      private const UInt32 WM_SYSCOMMAND = 0x112;
      private const UInt32 SC_RESTORE = 0xf120;
      public const int WM_SETTEXT = 0X000C;

      #endregion

      //private const string OnScreenKeyboardExe = @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe";
      private const string OnScreenKeyboardExe = "osk.exe";

      /// <summary>
      /// Запустить виртуальную клавиатуру
      /// </summary>
      public static void StartVirtualKeyboard()
      {
         Process[] p = Process.GetProcessesByName(
             Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));

         if (p.Length == 0)
         {
            StartOsk();
         }
         else
         {
            // there might be a race condition if the process terminated 
            // meanwhile -> proper exception handling should be added
            //
            SendMessage(p[0].MainWindowHandle,
                WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0));
         }
      }

      public static void TryFindWindows()
      {
         IntPtr ptr = FindWindow(null, "Экранная клавиатура");
         if (ptr == IntPtr.Zero)
         {
            Console.WriteLine("Окно не найдено");
         }
         else
         {
            SendMessage(ptr, WM_SETTEXT, new IntPtr(SC_RESTORE), new IntPtr(0)); // сама функция отправки текста в заголовок
         }
      }

      static void StartOsk()
      {
         IntPtr ptr = new IntPtr(); ;
         bool sucessfullyDisabledWow64Redirect = false;

         // Disable x64 directory virtualization if we're on x64,
         // otherwise keyboard launch will fail.
         if (System.Environment.Is64BitOperatingSystem)
         {
            sucessfullyDisabledWow64Redirect =
                Wow64DisableWow64FsRedirection(ref ptr);
         }


         ProcessStartInfo psi = new ProcessStartInfo();
         psi.FileName = OnScreenKeyboardExe;
         // We must use ShellExecute to start osk from the current thread
         // with psi.UseShellExecute = false the CreateProcessWithLogon API 
         // would be used which handles process creation on a separate thread 
         // where the above call to Wow64DisableWow64FsRedirection would not 
         // have any effect.
         //
         psi.UseShellExecute = true;
         Process.Start(psi);

         // Re-enable directory virtualisation if it was disabled.
         if (System.Environment.Is64BitOperatingSystem)
            if (sucessfullyDisabledWow64Redirect)
               Wow64RevertWow64FsRedirection(ptr);
      }
   }
}
