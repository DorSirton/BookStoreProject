using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class RemoveFromStockViewModel : ViewModelBase
    {
        private string selectedProdactType;
        private Prodact selectedProdact;
        private string deletedActionReturn;

        public RelayCommand Removefromstock { get; set; }
        public RelayCommand GoBack { get; set; }

        public ObservableCollection<string> ProdactTypeCollection { get; set; }
        public ObservableCollection<Prodact> ShowProdactCollection { get; set; }
        public string DeletedActionReturn { get => deletedActionReturn; set => Set(ref deletedActionReturn, value); }
        public string SelectedProdactType
        {
            get => selectedProdactType;
            set
            {
                if (value != selectedProdactType)
                    Set(ref selectedProdactType, value);
                GetProdacts();
            }
        }
        public Prodact SelectedProdact { get => selectedProdact; set => Set(ref selectedProdact, value); }
        public RemoveFromStockViewModel()
        {
            ProdactTypeCollection = new ObservableCollection<string>();
            ShowProdactCollection = new ObservableCollection<Prodact>();
            Removefromstock = new RelayCommand(RemoveProdactFromStock);
            GoBack = new RelayCommand(ReturnToMenegerMenu);
            FillProdactTypes();
        }

        private void ReturnToMenegerMenu()
        {
            MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
        }

        public void FillProdactTypes()
        {
            ProdactService.Instance.ProdactsTypes.Remove("All");
            ProdactService.Instance.ProdactsTypes.ForEach(p => ProdactTypeCollection.Add(p));
        }

        public void GetProdacts()
        {
            ShowProdactCollection.Clear();
            foreach (var item in ProdactService.Instance.GetAllProdact())
                if (item.GetType().Name == SelectedProdactType)
                    ShowProdactCollection.Add(item);
        }
        private void RemoveProdactFromStock()
        {
            if (selectedProdactType == null)
            {
                DeletedActionReturn = "Please Select Your Prodact To Remove";
            }
            if (selectedProdact == null)
            {
                DeletedActionReturn = "Please Select Your Prodact To Remove";
            }
            else
            {
                foreach (var item in ProdactService.Instance.GetAllProdact())
                    if (item.Id == selectedProdact.Id)
                    {
                        DeletedActionReturn = ProdactService.Instance.RemoveProdactFromCollection(item);
                        ShowProdactCollection.Clear();
                        return;
                    }
            }
        }
    }
}

