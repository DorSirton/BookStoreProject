using BookStore.Client.Services;
using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class ProdactListViewModel : ViewModelBase
    {
        private string selectedProdact;
        private string selectedGenere;
        private ObservableCollection<Prodact> showProdactCollection;

        public RoleService MyRoleService { get; set; }
        public ObservableCollection<string> ProdactTypesCollection { get; set; }
        public RelayCommand Back { get; set; }
        public ObservableCollection<string> GenereCollection { get; set; }
        public ObservableCollection<Prodact> ShowProdactCollection
        {
            get => showProdactCollection;
            set
            {
                Set(ref showProdactCollection, value);
            }
        }

        public string SelectedGenere
        {
            get => selectedGenere;
            set
            {
                Set(ref selectedGenere, value);
                GetProdacts();
            }
        }
        public string SelectedProdact
        {
            get => selectedProdact;
            set
            {
                if (value != selectedProdact)
                    GenereCollection.Clear();
                Set(ref selectedProdact, value);
                UpdateSelectedItem();
                GetProdacts();
            }
        }

        public ProdactListViewModel(RoleService role)
        {
            MyRoleService = role;
            ShowProdactCollection = new ObservableCollection<Prodact>();
            ProdactTypesCollection = new ObservableCollection<string>();
            GenereCollection = new ObservableCollection<string>();
            Back = new RelayCommand(GoBack);
            FillProdactTypes();

        }


        public void GetProdacts()
        {
            ShowProdactCollection.Clear();
            if (SelectedProdact == "All")
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

        private void GoBack()
        {
            ShowProdactCollection.Clear();
            selectedProdact = "";
            selectedGenere = "";
            if (MyRoleService.IsMeneger)
            {
                MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
            }
            else
            {
                MessengerInstance.Send<UserControl>(new CostumerOptionPage());
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
            if (SelectedProdact == "Book")
            {
                FillBookGenere();
            }
            if (SelectedProdact == "Journal")
            {
                FillJournalGenere();
            }
        }

    }
}
