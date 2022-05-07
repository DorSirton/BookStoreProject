using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class RemoveSaleViewModel : ViewModelBase
    {
        private ObservableCollection<Prodact> prodactOnSaleCollection;
        private Prodact selectedProdact;
        private string returnAction;

        public string ReturnAction { get => returnAction; set => returnAction = value; }
        public Prodact SelectedProdact { get => selectedProdact; set => selectedProdact = value; }
        public ObservableCollection<Prodact> ProdactOnSaleCollection
        {
            get => prodactOnSaleCollection;
            set
            {
                Set(ref prodactOnSaleCollection, value);
            }
        }
        public RelayCommand RemoveSale { get; set; }
        public RelayCommand GoBack { get; set; }
        public RelayCommand ShowProdactOnSale { get; set; }
        public RemoveSaleViewModel()
        {
            GoBack = new RelayCommand(ReturnBack);
            RemoveSale = new RelayCommand(RemoveFromSaleCollection);
            ProdactOnSaleCollection = new ObservableCollection<Prodact>();
            ShowProdactOnSale = new RelayCommand(ShoeSaleProdact);

        }

        private void ShoeSaleProdact()
        {
            ProdactOnSaleCollection.Clear();
            FillCollection();
        }

        private void ReturnBack()
        {
            ProdactOnSaleCollection.Clear();
            MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
        }

        private void RemoveFromSaleCollection()
        {
            if (SelectedProdact == null)
            {
                return;
            }
            ProdactService.Instance.RemoveProdactFromSaleList(SelectedProdact);
            ProdactOnSaleCollection.Remove(SelectedProdact);

        }

        public void FillCollection()
        {
            ProdactOnSaleCollection.Clear();
            foreach (var item in ProdactService.Instance.GetAllSaleProdact().ToList())
            {
               ProdactOnSaleCollection.Add(item);
            }
        }
    }
}
