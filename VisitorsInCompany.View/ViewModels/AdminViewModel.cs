using MvvmCross.ViewModels;

namespace VisitorsInCompany.View.ViewModels
{
    public class AdminViewModel : MvxViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
            }
        }
        
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
    }
}