using System.Windows;
using MvvmCross.Platforms.Wpf.Views;
using VisitorsInCompany.Helpers;

namespace VisitorsInCompany.View.Views
{
   /// <summary>
   /// Interaction logic for VisitorsListView.xaml
   /// </summary>
   public partial class VisitorsListView : MvxWpfView
   {
      public VisitorsListView()
      {
         InitializeComponent();
      }

      private void TextBox_GotFocus(object sender, RoutedEventArgs e)
      {
         OperationSystemInteractiveHelper.StartVirtualKeyboard();
      }

      private void TextBox_LostFocus(object sender, RoutedEventArgs e)
      {
         Helper.KillOSKProcess();
      }
   }
}
