using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class ReportsViewModel : ViewModelBase
    {
        private ObservableCollection<Prodact> purchaseCollection;
        private Visibility visibility1;
        private Visibility visibility2;

        public ObservableCollection<Prodact> PurchaseCollection
        {
            get => purchaseCollection;
            set
            {
                Set(ref purchaseCollection, value);
                
            }
        }
        public Visibility Visibility1 { get => visibility1; set => Set(ref visibility1, value); }
        public Visibility Visibility2 { get => visibility2; set => Set(ref visibility2, value); }
        public RelayCommand PurchaseList { get; set; }
        public RelayCommand TransActions { get; set; }
        public RelayCommand GoBack { get; set; }
        public ReportsViewModel()
        {
            PurchaseCollection = new ObservableCollection<Prodact>();
            Visibility1 = Visibility.Collapsed;
            Visibility2 = Visibility.Collapsed;
            PurchaseList = new RelayCommand(GetComboBox1);
            TransActions = new RelayCommand(GetComboBox2);
            GoBack = new RelayCommand(ReturnToMenegerPage);
        }

        private void ReturnToMenegerPage()
        {
            Visibility1 = Visibility.Collapsed;
            Visibility2 = Visibility.Collapsed;
            MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
        }

        private void GetComboBox2()
        {
            InsertPurchaseProdacts();
            Visibility2 = Visibility.Visible;
        }

        private void GetComboBox1()
        {
            InsertPurchaseProdacts();
            Visibility1 = Visibility.Visible;
        }

        public void InsertPurchaseProdacts()
        {
            PurchaseCollection.Clear();
            ProdactService.Instance.GetAllPurchaseProdact().ToList().ForEach(p => PurchaseCollection.Add(p));
        }
    }
}
