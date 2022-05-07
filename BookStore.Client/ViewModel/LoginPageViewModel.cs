using BookStore.Client.Services;
using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string passward;
        private string userName;

        public RelayCommand LoginComand { get; set; }
        public RoleService MyService { get; set; }
        public string Passward { get => passward; set => Set(ref passward, value); }
        public string UserName { get => userName; set => Set(ref userName, value); }


        public LoginPageViewModel(RoleService r)
        {
            LoginComand = new RelayCommand(Login);
            MyService = r;
        }

        private void Login()
        {
            if (Passward == "120798" && UserName == "DorSirton")
            {
                MyService.IsMeneger = true;
                MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
            }
            else
            {
                if (Passward == default || UserName == default)
                {
                    return;
                }
                if (Passward == default && UserName == default)
                {
                    return;
                }
                MyService.IsMeneger = false;
                MessengerInstance.Send<UserControl>(new CostumerOptionPage());
            }
        }
    }
}
