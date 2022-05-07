using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{

    public class MainViewModel : ViewModelBase
    {


        private UserControl myContent;
        public UserControl MyContent { get => myContent; set => Set(ref myContent, value); }

        public MainViewModel()
        {
            MyContent = new LoginPage();
            MessengerInstance.Register<UserControl>(this, UpdateUserControl);
        }


        private void UpdateUserControl(UserControl obj)
        {
            MyContent = obj;
        }
    }
}
