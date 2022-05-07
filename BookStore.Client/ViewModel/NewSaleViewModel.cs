using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class NewSaleViewModel: ViewModelBase
    {
        private UserControl changingUserControl;
        public UserControl ChangingUserControl { get => changingUserControl; set => Set(ref changingUserControl, value); }
        public RelayCommand Button1 { get; set; }
        public RelayCommand Button2 { get; set; }
        public RelayCommand Button3 { get; set; }
        public NewSaleViewModel()
        {
            ChangingUserControl = null;
            Button1 = new RelayCommand(SendToSaleByprodactType);
            Button2 = new RelayCommand(SendToSaleByprodact);
            Button3= new RelayCommand(SendToSaleByGener);
        }

        private void SendToSaleByprodactType()
        {
            ChangingUserControl = new SaleByProdactTypePage();
        }

        private void SendToSaleByprodact()
        {
            ChangingUserControl = new SaleBySpaciphicProdactPage();
        }
        private void SendToSaleByGener()
        {
            ChangingUserControl = new SaleByGenerPage();
        }

    }
}
