using MvvmCross.Platforms.Wpf.Views;
using System.Windows.Input;
using VisitorsInCompany.View.ViewModels;

namespace VisitorsInCompany.View.Views
{
   /// <summary>
   /// Interaction logic for LoginView.xaml
   /// </summary>
   public partial class LoginView : MvxWpfView
   {
      public LoginView()
      {
         InitializeComponent();
         login.Focus();
      }

      private void MvxWpfView_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Enter)
         {
            (DataContext as LoginViewModel).LoginCommand.Execute(password);
            e.Handled = true;
         }
      }
   }
}
