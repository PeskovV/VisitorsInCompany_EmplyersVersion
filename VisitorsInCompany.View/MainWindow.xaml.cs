using MvvmCross.Platforms.Wpf.Views;
using System.Windows.Input;
using VisitorsInCompany.Helpers;

namespace VisitorsInCompany.View
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : MvxWindow
   {
      //private RoutingVM _root = new RoutingVM();
      //private MainScreenVM _mainControl;
      //private CreateUserVM _createUser;

      public MainWindow()
      {
         InitializeComponent();
         //_mainControl = new MainScreenVM(new MvxNavigationService(new MvxNavigationCache(), new MvxViewModelLoader(null)));
         //_mainControl.OnCreateUser += MainControl_OnCreateUser;
         //_root.CurrentContentVM = _mainControl;
         //this.DataContext = _root;
      }

      //private void MainControl_OnCreateUser()
      //{
      //   _createUser = new CreateUserVM();
      //   _createUser.BackToMainControl += CreateUser_BackToMainControl;
      //   _root.CurrentContentVM = _createUser;
      //}

      private void CreateUser_BackToMainControl()
      {
         //if (_mainControl == null)
         //{
         //   _mainControl = new MainScreenVM();
         //   _mainControl.OnCreateUser += MainControl_OnCreateUser;
         //}
         //_root.CurrentContentVM = _mainControl;
      }

      private void MvxWindow_Closed(object sender, System.EventArgs e)
      {
         Helper.KillOSKProcess();
      }
   }
}
