using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.View.Views;

namespace VisitorsInCompany.View.ViewModels
{
    public class ReportsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private BackupProcess _backupWin = null;

        public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
        public IMvxAsyncCommand GoToCurrentVisitorReportCommand => new MvxAsyncCommand(GoToCurrentVisitorReportAsync);
        public IMvxAsyncCommand GoToBelatedVisitorReportCommand => new MvxAsyncCommand(GoToBelatedVisitorReportAsync);
        public IMvxAsyncCommand GoToVisitLogCommand => new MvxAsyncCommand(GoToVisitLogAsync);
        public IMvxAsyncCommand BackupCommand => new MvxAsyncCommand(BackupAsync);

        public ReportsViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
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

        private async Task BackupAsync()
        {
            try
            {
                foreach (var dir in DriveInfo.GetDrives())
                {
                    if (dir.DriveType == DriveType.Removable && dir.IsReady)
                    {
                        string nowDate = DateTime.Now.ToShortDateString().Replace(':', '_');
                        string targetPath = dir.Name + $"backup - {nowDate}";
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

        private async Task GoToMainScreenAsync() => await _navigationService.Navigate<MainScreenViewModel>();
        private async Task GoToCurrentVisitorReportAsync() => await _navigationService.Navigate<CurrentVisitorReportViewModel>();
        private async Task GoToBelatedVisitorReportAsync() => await _navigationService.Navigate<BelatedVisitorReportViewModel>();
        private async Task GoToVisitLogAsync() => await _navigationService.Navigate<VisitLogViewModel>();
    }
}
