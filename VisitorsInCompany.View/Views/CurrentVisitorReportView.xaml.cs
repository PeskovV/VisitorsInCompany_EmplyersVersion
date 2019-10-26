using MvvmCross.Platforms.Wpf.Views;
using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.ViewModels;

namespace VisitorsInCompany.Views
{
   /// <summary>
   /// Interaction logic for CurrentVisitorReportView.xaml
   /// </summary>
   public partial class CurrentVisitorReportView : MvxWpfView
   {
      private Timer _timer = new Timer(Helper.GetTimeBeforeSearch());

      public CurrentVisitorReportView()
      {
         InitializeComponent();
         _timer.Elapsed += _timer_Elapsed;
      }

      private void _timer_Elapsed(object sender, ElapsedEventArgs e)
      {
         _timer.Stop();
         Dispatcher.BeginInvoke(new Action(() => (DataContext as CurrentVisitorReportViewModel).SortCollection(search.Text)), System.Windows.Threading.DispatcherPriority.Normal);
         
      }

      private void lstView_Loaded(object sender, RoutedEventArgs e)
      {
         CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstView.ItemsSource);
         view.SortDescriptions.Add(new SortDescription("FullName", ListSortDirection.Ascending));
         view.SortDescriptions.Add(new SortDescription("Organization", ListSortDirection.Ascending));
      }

      GridViewColumnHeader _lastHeaderClicked = null;
      ListSortDirection _lastDirection = ListSortDirection.Ascending;
      

      void GridViewColumnHeaderClickedHandler(object sender,
                                              RoutedEventArgs e)
      {
         var headerClicked = e.OriginalSource as GridViewColumnHeader;
         ListSortDirection direction = ListSortDirection.Ascending;

         if (headerClicked != null)
         {
            if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
            {
               if (headerClicked != _lastHeaderClicked)
               {
                  if (direction == ListSortDirection.Ascending)
                     direction = ListSortDirection.Descending;
                  else
                     direction = ListSortDirection.Ascending;
               }
               else
               {
                  if (_lastDirection == ListSortDirection.Ascending)
                  {
                     direction = ListSortDirection.Descending;
                  }
                  else
                  {
                     direction = ListSortDirection.Ascending;
                  }
               }

               var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
               var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

               Sort(sortBy, direction);

               if (direction == ListSortDirection.Ascending)
               {
                  headerClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateArrowUp"] as DataTemplate;
               }
               else
               {
                  headerClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateArrowDown"] as DataTemplate;
               }

               // Remove arrow from previously sorted header
               if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
               {
                  _lastHeaderClicked.Column.HeaderTemplate = null;
               }

               _lastHeaderClicked = headerClicked;
               _lastDirection = direction;
            }
         }
      }

      private void Sort(string sortBy, ListSortDirection direction)
      {
         ICollectionView dataView =
           CollectionViewSource.GetDefaultView(lstView.ItemsSource);

         dataView.SortDescriptions.Clear();
         SortDescription sd = new SortDescription(sortBy, direction);
         dataView.SortDescriptions.Add(sd);
         dataView.Refresh();
      }

      private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
         _timer.Stop();
         _timer.Start();
      }

      private void search_GotFocus(object sender, RoutedEventArgs e)
      {
         OperationSystemInteractiveHelper.StartVirtualKeyboard();
      }

      private void search_LostFocus(object sender, RoutedEventArgs e)
      {
         Helper.KillOSKProcess();
      }
   }
}
