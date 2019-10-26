using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisitorsInCompany.Helpers;

namespace VisitorsInCompany.Views
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
