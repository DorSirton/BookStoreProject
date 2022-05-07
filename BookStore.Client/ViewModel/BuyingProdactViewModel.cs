using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class BuyingProdactViewModel : ViewModelBase
    {
        private string selectedProdactType;
        private Prodact selectedProdact;
        private string selectedGenere;
        private ObservableCollection<Prodact> showProdactCollection;
        public ObservableCollection<Prodact> MyCartList { get; set; }
        public ObservableCollection<string> ProdactTypesCollection { get; set; }
        public RelayCommand Back { get; set; }
        public RelayCommand Pay { get; set; }
        public RelayCommand AddToCart { get; set; }

        public ObservableCollection<string> GenereCollection { get; set; }
        public ObservableCollection<Prodact> ShowProdactCollection
        {
            get => showProdactCollection;
            set
            {
                Set(ref showProdactCollection, value);
            }
        }
        public Prodact SelectedProdact { get => selectedProdact; set => Set(ref selectedProdact, value); }

        public string SelectedGenere
        {
            get => selectedGenere;
            set
            {
                Set(ref selectedGenere, value);
                GetProdacts();
            }
        }
        public string SelectedProdactType
        {
            get => selectedProdactType;
            set
            {
                if (value != selectedProdactType)
                    GenereCollection.Clear();
                Set(ref selectedProdactType, value);
                UpdateSelectedItem();
                GetProdacts();
            }
        }

        public BuyingProdactViewModel()
        {

            ShowProdactCollection = new ObservableCollection<Prodact>();
            ProdactTypesCollection = new ObservableCollection<string>();
            GenereCollection = new ObservableCollection<string>();
            MyCartList = new ObservableCollection<Prodact>();
            Back = new RelayCommand(GoBack);
            Pay = new RelayCommand(GoToPay);
            AddToCart = new RelayCommand(AddToCartList);
            FillProdactTypes();
        }

        private void AddToCartList()
        {
            MyCartList.Add(SelectedProdact);
            ProdactService.Instance.RemoveProdactFromCollection(SelectedProdact);
            showProdactCollection.Clear();
        }

        private void GoToPay()
        {
            MessengerInstance.Send<UserControl>(new PaymentPage());
            MessengerInstance.Send(MyCartList);
        }

        private void GoBack()
        {
            MessengerInstance.Send<UserControl>(new CostumerOptionPage());
        }
        public void GetProdacts()
        {
            ShowProdactCollection.Clear();

            if (SelectedProdactType == "All")
            {
                foreach (var item in ProdactService.Instance.GetAllProdact())
                    ShowProdactCollection.Add(item);
                return;
            }
            else
            {
                foreach (var item in ProdactService.Instance.GetAllProdact())
                {
                    if (item.Genere == selectedGenere)
                    {
                        ShowProdactCollection.Add(item);
                    }
                }
            }
        }
        

        public void FillProdactTypes()
        {
            ProdactService.Instance.ProdactsTypes.ForEach(p => ProdactTypesCollection.Add(p));
        }
        public void FillBookGenere()
        {
            ProdactService.Instance.BookGener.ForEach(p => GenereCollection.Add(p));
        }
        public void FillJournalGenere()
        {
            ProdactService.Instance.JournalGener.ForEach(p => GenereCollection.Add(p));
        }
        public void UpdateSelectedItem()
        {
            if (SelectedProdactType == "Book")
            {
                FillBookGenere();
            }
            if (SelectedProdactType == "Journal")
            {
                FillJournalGenere();
            }
        }
    }
}
