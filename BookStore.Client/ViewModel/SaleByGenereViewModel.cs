using BookStore.Client.Views;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class SaleByGenereViewModel : ViewModelBase
    {
        private ObservableCollection<string> prodactTypeCollection;
        private ObservableCollection<decimal> precentDiscountCollection;
        private ObservableCollection<string> generCollection;
        private decimal selectedPrecent;
        private string selectedProdactType;
        private string returnAction;
        private string selectedGener;

        public string ReturnAction { get => returnAction; set => Set(ref returnAction, value); }
        public string SelectedProdactType
        {
            get => selectedProdactType;
            set
            {
                Set(ref selectedProdactType, value);
                UpdateGener();
            }
        }
        public string SelectedGener { get => selectedGener; set => selectedGener = value; }
        public decimal SelectedPrecent { get => selectedPrecent; set => Set(ref selectedPrecent, value); }
        public RelayCommand R { get; set; }
        public RelayCommand GoBack { get; set; }
        public ObservableCollection<string> ProdactTypeCollection { get => prodactTypeCollection; set => Set(ref prodactTypeCollection, value); }
        public ObservableCollection<string> GenerCollection { get => generCollection; set => Set(ref generCollection, value); }
        public ObservableCollection<decimal> PrecentDiscountCollection { get => precentDiscountCollection; set => Set(ref precentDiscountCollection, value); }
        public SaleByGenereViewModel()
        {
            R = new RelayCommand(CreateSale);
            GoBack = new RelayCommand(Return);
            prodactTypeCollection = new ObservableCollection<string>();
            PrecentDiscountCollection = new ObservableCollection<decimal>();
            GenerCollection = new ObservableCollection<string>();
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
            if (SelectedProdactType == null || SelectedPrecent == default || SelectedGener == null)
            {
                ReturnAction = "One Or More Of The Fields Is Unsigned";
                return;
            }
            ReturnAction = "";
            foreach (var item in ProdactService.Instance.GetAllProdact())
            {
                decimal price = item.BasePrice / 100 * SelectedPrecent;
                if (item.GetType().Name == SelectedProdactType)
                {
                    if (item.Genere == SelectedGener)
                    {
                        if (item.DiscountPrice != 0)
                        {
                            ProdactService.Instance.RemoveProdactFromSaleList(item);
                        }
                        item.DiscountPrice = price;
                        ProdactService.Instance.AddProdactToSaleList(item);
                    }
                }
            }
            SelectedProdactType = default;
            SelectedGener = default;
            SelectedPrecent = default;
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
        public void FillBookGenere()
        {
            ProdactService.Instance.BookGener.ForEach(p => GenerCollection.Add(p));
        }
        public void FillJournalGenere()
        {
            ProdactService.Instance.JournalGener.ForEach(p => GenerCollection.Add(p));
        }
        public void UpdateGener()
        {
            GenerCollection.Clear();
            if (SelectedProdactType == "Book")
            {
                FillBookGenere();
            }
            if (SelectedProdactType == "Journal")
            {
                FillJournalGenere();
            }
        }
        public void FillProdactTypeCollection()
        {
            ProdactService.Instance.ProdactsTypes.ForEach(p => ProdactTypeCollection.Add(p));
            ProdactTypeCollection.Remove("All");
        }
    }
}

