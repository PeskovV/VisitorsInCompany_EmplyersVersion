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

namespace VisitorsInCompany.Views
{
   /// <summary>
   /// Interaction logic for VisitorShortInfo.xaml
   /// </summary>
   public partial class VisitorShortInfo : MvxWpfView
   {
      public VisitorShortInfo()
      {
         InitializeComponent();
      }

      private void MvxWpfView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
      {

      }
   }
}
