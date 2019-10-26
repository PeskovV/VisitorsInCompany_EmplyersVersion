using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.Interfaces;
using VisitorsInCompany.Views;

namespace VisitorsInCompany.ViewModels
{
   class ReportsViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      private BackupProcess _backupWin = null;
      public ReportsViewModel(IMvxNavigationService navigationService, IRepository repo)
      {
         _navigationService = navigationService;
         _repo = repo;
      }

      public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
      private async Task GoToMainScreenAsync()
      {
         await _navigationService.Navigate<MainScreenViewModel>();
      }

      public IMvxAsyncCommand GoToCurrentVisitorReportCommand => new MvxAsyncCommand(GoToCurrentVisitorReportAsync);
      private async Task GoToCurrentVisitorReportAsync()
      {
         await _navigationService.Navigate<CurrentVisitorReportViewModel>();
      }

      public IMvxAsyncCommand GoToBelatedVisitorReportCommand => new MvxAsyncCommand(GoToBelatedVisitorReportAsync);
      private async Task GoToBelatedVisitorReportAsync()
      {
         await _navigationService.Navigate<BelatedVisitorReportViewModel>();
      }

      public IMvxAsyncCommand GoToVisitLogCommand => new MvxAsyncCommand(GoToVisitLogAsync);
      private async Task GoToVisitLogAsync()
      {
         await _navigationService.Navigate<VisitLogViewModel>();
      }

      public IMvxAsyncCommand BackupCommand => new MvxAsyncCommand(BackupAsync);
      private async Task BackupAsync()
      {
         string targetPath = string.Empty;
         try
         {
            foreach (var dir in DriveInfo.GetDrives())
            {
               if (dir.DriveType == DriveType.Removable && dir.IsReady)
               {
                  string nowDate = DateTime.Now.ToShortDateString().Replace(':', '_');
                  targetPath = dir.Name + $"backup - {nowDate}";
                  //+ Path.DirectorySeparatorChar; 
                  //+ Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar).Last();

                  Directory.CreateDirectory(targetPath);
                  _backupWin = new BackupProcess();
                  
                  using (WaitCursor cursor = new WaitCursor())
                  {
                     await CopyData(Environment.CurrentDirectory, targetPath);
                     _backupWin.Close();
                  }
                  MessageBox.Show($"Данные сохранены по пути: {targetPath}", "Резервное копирование произведено");
                  return;
               }
            }
            MessageBox.Show($"Внешние носители не найдены");
         }
         catch (Exception ex)
         {
            MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
         }
         finally
         {
            _backupWin?.Close();
         }
         
      }

      //private void CopyData(string backupDirectory)
      //{
      //   //CopyDirectories(Environment.CurrentDirectory, backupDirectory);
      //   Copy(Environment.CurrentDirectory, backupDirectory);
      //}

      //private void CopyDirectories(string currentDirectory, string backupDirectory)
      //{
      //   foreach (var dir in Directory.GetDirectories(currentDirectory))
      //   {
      //      if (dir == backupDirectory)
      //         continue;

      //      CopyDirectories(dir, backupDirectory);
      //      CopyFiles(dir, backupDirectory);
      //   }
      //}

      //private void CopyFiles(string currentDirectory, string backupDirectory)
      //{
      //   foreach (var f in Directory.GetFiles(currentDirectory))
      //   {
      //      File.Copy(f, backupDirectory + Path.DirectorySeparatorChar + Path.GetFileName(f));
      //   }
      //}

      private async Task CopyData(string currentDirectory, string backupDirectory)
      {

         await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => _backupWin?.Show()), DispatcherPriority.Normal);

         DirectoryInfo dirSource;
         DirectoryInfo dirTarget;
         List<string> pathList = new List<string>();
         foreach (var dir in Directory.GetDirectories(currentDirectory))
         {
            if (dir == backupDirectory)
               continue;

            dirSource = new DirectoryInfo(dir);
            string targetPath = backupDirectory +
                            Path.DirectorySeparatorChar +
                            dir.Split(Path.DirectorySeparatorChar).Last();

            Directory.CreateDirectory(targetPath);

            dirTarget = new DirectoryInfo(targetPath);
            CopyAll(dirSource, dirTarget);
         }
         CopyAll(new DirectoryInfo(currentDirectory), new DirectoryInfo(backupDirectory));
      }

      public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
      {
         try
         {
            if (source.FullName.ToLower() == target.FullName.ToLower())
               return;

            // Check if the target directory exists, if not, create it. 
            if (Directory.Exists(target.FullName) == false)
               Directory.CreateDirectory(target.FullName);

            // Copy each file into it's new directory. 
            foreach (FileInfo fi in source.GetFiles())
               fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);

            // Copy each subdirectory using recursion. 
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
               DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
               CopyAll(diSourceSubDir, nextTargetSubDir);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show($"Ошибка при создании резервной копии: {ex.Message} \n Операция прервана!");
         }
         
      }
   }
}
