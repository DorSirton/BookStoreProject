using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class SaleBySpaciphicProdactViewModel : ViewModelBase
    {
        private ObservableCollection<string> prodactTypeCollection;
        private ObservableCollection<decimal> precentDiscountCollection;
        private ObservableCollection<Prodact> prodactCollection;
        private decimal selectedPrecent;
        private string selectedProdactType;
        private string returnAction;
        private Prodact selectedProdact;

        public Prodact SelectedProdact { get => selectedProdact; set => Set(ref selectedProdact, value); }
        public string ReturnAction { get => returnAction; set => Set(ref returnAction, value); }
        public string SelectedProdactType
        {
            get => selectedProdactType;
            set
            {
                Set(ref selectedProdactType, value);
                FillProdactCollection();
            }
        }
        public decimal SelectedPrecent { get => selectedPrecent; set => Set(ref selectedPrecent, value); }
        public RelayCommand R { get; set; }
        public RelayCommand GoBack { get; set; }
        public ObservableCollection<Prodact> ProdactCollection { get => prodactCollection; set => prodactCollection = value; }
        public ObservableCollection<string> ProdactTypeCollection { get => prodactTypeCollection; set => Set(ref prodactTypeCollection, value); }
        public ObservableCollection<decimal> PrecentDiscountCollection { get => precentDiscountCollection; set => Set(ref precentDiscountCollection, value); }
        public SaleBySpaciphicProdactViewModel()
        {
            R = new RelayCommand(CreateSale);
            GoBack = new RelayCommand(Return);
            ProdactCollection = new ObservableCollection<Prodact>();
            prodactTypeCollection = new ObservableCollection<string>();
            PrecentDiscountCollection = new ObservableCollection<decimal>();
            AddDefultPrecent();
            FillProdactTypeCollection();
        }
        private void Return()
        {
            ReturnAction = default;
            SelectedPrecent = default;
            SelectedProdact = default;
            SelectedProdactType = default;
            MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
        }
        private void CreateSale()
        {
            ReturnAction = "";
            if (SelectedPrecent == default || selectedProdactType == null || SelectedProdact == null)
            {
                ReturnAction = "One Or More Of The Fields Is Unsigned";
                return;
            }
            foreach (var item in ProdactService.Instance.GetAllProdact())
            {
                decimal price = item.BasePrice / 100 * SelectedPrecent;
                if (SelectedProdact == item)
                {
                    if (item.DiscountPrice != 0)
                    {
                        ProdactService.Instance.RemoveProdactFromSaleList(item);
                    }
                    item.DiscountPrice = price;
                    ProdactService.Instance.AddProdactToSaleList(item);
                }
            }
            ReturnAction = "New Sale Add Succesfully";
            SelectedPrecent = default;
            SelectedProdactType = default;
            SelectedProdact = default;

        }

        private void AddDefultPrecent()
        {
            PrecentDiscountCollection.Add(10);
            PrecentDiscountCollection.Add(15);
            PrecentDiscountCollection.Add(20);
            PrecentDiscountCollection.Add(25);
            PrecentDiscountCollection.Add(40);
            PrecentDiscountCollection.Add(70);
            PrecentDiscountCollection.Add(90);
        }
        public void FillProdactTypeCollection()
        {
            ProdactService.Instance.ProdactsTypes.ForEach(p => ProdactTypeCollection.Add(p));
        }
        public void FillProdactCollection()
        {
            ProdactCollection.Clear();
            if (SelectedProdactType == "All")
            {
                foreach (var item in ProdactService.Instance.GetAllProdact())
                    ProdactCollection.Add(item);
                return;
            }
            else
            {
                foreach (var item in ProdactService.Instance.GetAllProdact())
                    if (item.GetType().Name == SelectedProdactType)
                        ProdactCollection.Add(item);
            }
        }
    }
}
