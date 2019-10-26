using MvvmCross.Platforms.Wpf.Views;
using System.Windows.Input;
using VisitorsInCompany.ViewModels;

namespace VisitorsInCompany.View.Views
{
   /// <summary>
   /// Interaction logic for MainScreenControl.xaml
   /// </summary>
   public partial class MainScreenView : MvxWpfView
   {
      public MainScreenView()
      {
         InitializeComponent();
         //createVisitor.Focus();
      }
      private void MvxWpfView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
      {

         //if (Keyboard.IsKeyDown(Key.LeftCtrl) && (e.Key == Key.O))
         //{
         //   (DataContext as MainScreenViewModel).ChangeVisibilityButton();
         //   e.Handled = true;

         //}
      }
   }
}