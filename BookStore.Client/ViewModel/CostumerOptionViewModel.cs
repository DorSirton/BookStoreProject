using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class CostumerOptionViewModel : ViewModelBase
    {
        public RelayCommand BuyProdact{ get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand SalesOptions { get; set; }
        public RelayCommand SeeProdacts { get; set; }
        public CostumerOptionViewModel()
        {
            BuyProdact = new RelayCommand(BuyProdacts);
            Exit = new RelayCommand(ReturnToLogin);
            SalesOptions = new RelayCommand(SeeAllSales);
            SeeProdacts = new RelayCommand(SeeCoolections);
        }

        private void SeeCoolections()
        {
            MessengerInstance.Send<UserControl>(new ProdactListPage());
        }

        private void SeeAllSales()
        {
            MessengerInstance.Send<UserControl>(new NewSalePage());
        }

        private void ReturnToLogin()
        {
            MessengerInstance.Send<UserControl>(new LoginPage());
        }

        private void BuyProdacts()
        {
            MessengerInstance.Send<UserControl>(new BuyingPage());
        }
    }
}
