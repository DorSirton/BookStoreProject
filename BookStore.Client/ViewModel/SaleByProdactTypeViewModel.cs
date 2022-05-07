using BookStore.Client.Views;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class SaleByProdactTypeViewModel : ViewModelBase
    {
        private ObservableCollection<string> prodactTypeCollection;
        private ObservableCollection<decimal> precentDiscountCollection;
        private decimal selectedPrecent;
        private string selectedProdactType;
        private string returnAction;

        public string ReturnAction { get => returnAction; set => Set(ref returnAction, value); }
        public RelayCommand R { get; set; }
        public RelayCommand GoBack { get; set; }
        public string SelectedProdactType { get => selectedProdactType; set => Set(ref selectedProdactType, value); }
        public decimal SelectedPrecent { get => selectedPrecent; set => Set(ref selectedPrecent, value); }
        public ObservableCollection<string> ProdactTypeCollection { get => prodactTypeCollection; set => Set(ref prodactTypeCollection, value); }
        public ObservableCollection<decimal> PrecentDiscountCollection { get => precentDiscountCollection; set => Set(ref precentDiscountCollection, value); }
        public SaleByProdactTypeViewModel()
        {
            R = new RelayCommand(CreateSale);
            GoBack = new RelayCommand(Return);
            prodactTypeCollection = new ObservableCollection<string>();
            PrecentDiscountCollection = new ObservableCollection<decimal>();
            AddDefultPrecent();
            FillProdactTypeCollection();
        }

        private void Return()
        {
            SelectedProdactType = "";
            ReturnAction = "";
            SelectedPrecent = default;
            MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
        }

        private void CreateSale()
        {
            ReturnAction = "";
            if ( SelectedPrecent == default || SelectedProdactType == null)
            {
                ReturnAction = "One Or More Of The Fields Is Unsigned";
                return;
            }
            foreach (var item in ProdactService.Instance.GetAllProdact())
            {
                decimal price = item.BasePrice / 100 * SelectedPrecent;
                if (item.GetType().Name == SelectedProdactType)
                {
                    if (item.DiscountPrice != 0)
                    {
                        ProdactService.Instance.RemoveProdactFromSaleList(item);
                    }
                    item.DiscountPrice = price;
                    ProdactService.Instance.AddProdactToSaleList(item);
                }
            }
            SelectedPrecent = default;
            SelectedProdactType = default;
            ReturnAction = "New Sale Add Succesfully";
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
            ProdactTypeCollection.Remove("All");
        }
    }
}
