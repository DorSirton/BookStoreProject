using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class MenegerOptionViewModel : ViewModelBase
    {
        public RelayCommand ShowProdacts { get; set; }
        public RelayCommand AddToStock { get; set; }
        public RelayCommand RemoveFromStock { get; set; }
        public RelayCommand NewSale { get; set; }
        public RelayCommand RemoveSale { get; set; }
        public RelayCommand Reports { get; set; }
        public RelayCommand Exit { get; set; }
        public MenegerOptionViewModel()
        {
            ShowProdacts = new RelayCommand(ShowProdactsListPage);
            AddToStock = new RelayCommand(AddToStockPage);
            NewSale = new RelayCommand(NewSalePage);
            RemoveSale = new RelayCommand(RemoveSalePage);
            Reports = new RelayCommand(ReportsPage);
            Exit = new RelayCommand(ReturnToLoginPage);
            RemoveFromStock = new RelayCommand(RemoveFromStockPage);
        }

        private void RemoveFromStockPage()
        {
            MessengerInstance.Send<UserControl>(new RemoveFromStockPage());

        }

        private void ReturnToLoginPage()
        {
            MessengerInstance.Send<UserControl>(new LoginPage());
        }

        private void ReportsPage()
        {
            MessengerInstance.Send<UserControl>(new ReportsPage());

        }

        private void RemoveSalePage()
        {
            MessengerInstance.Send<UserControl>(new RemoveSalePage());

        }

        private void NewSalePage()
        {
            MessengerInstance.Send<UserControl>(new NewSalePage());

        }

        private void AddToStockPage()
        {
            MessengerInstance.Send<UserControl>(new AddToStockPage());
        }

        private void ShowProdactsListPage()
        {
            MessengerInstance.Send<UserControl>(new ProdactListPage());
        }
    }
}
