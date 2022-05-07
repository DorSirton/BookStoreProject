using BookStore.Client.Views;
using BookStore.Models;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class PaymentPageViewModel : ViewModelBase
    {
        private ObservableCollection<Prodact> prodactSelectedCollection;
        private decimal totalSelectedPrice;
        private string actionFidback;
        private string mailAdress;
        private int last3Digits;
        private int creditCardNumber;
        private DateTime validity;
        private Prodact regrateProdact;

        public bool MailContainKey { get; set; }
        public RelayCommand GoBack { get; set; }
        public RelayCommand Buy { get; set; }
        public RelayCommand Regrate { get; set; }
        public string ActionFidback { get => actionFidback; set => Set(ref actionFidback, value); }
        public Prodact RegrateProdact { get => regrateProdact; set => Set(ref regrateProdact, value); }
        public string MailAdress { get => mailAdress; set => Set(ref mailAdress, value); }
        public int Last3Digits { get => last3Digits; set => Set(ref last3Digits, value); }
        public int CreditCardNumber { get => creditCardNumber; set => Set(ref creditCardNumber, value); }
        public DateTime Validity { get => validity; set => Set(ref validity, value); }
        public decimal TotalSelectedPrice { get => totalSelectedPrice; set => Set(ref totalSelectedPrice, value); }

        public ObservableCollection<Prodact> ProdactSelectedCollection { get => prodactSelectedCollection; set => Set(ref prodactSelectedCollection, value); }
        public PaymentPageViewModel()
        {
            GoBack = new RelayCommand(ReturnToBuyingPage);
            Buy = new RelayCommand(FinalProdactBuying);
            Regrate = new RelayCommand(UndoSelectingProdact);
            ProdactSelectedCollection = new ObservableCollection<Prodact>();
            MessengerInstance.Register<ObservableCollection<Prodact>>(this, ConnectProdact);
            Validity = DateTime.Now;
        }

        private void UndoSelectingProdact()
        {
            if (RegrateProdact == default)
            {
                return;
            }
            TotalSelectedPrice -= regrateProdact.BasePrice;
            ProdactService.Instance.AddExistingProdactToStock(RegrateProdact);
            prodactSelectedCollection.Remove(RegrateProdact);
            if (prodactSelectedCollection.Count == 0)
            {
                MessengerInstance.Send<UserControl>(new BuyingPage());
            }
        }

        private void FinalProdactBuying()
        {

            if (CreditCardNumber > 999999 || CreditCardNumber < 100000)
            {
                ActionFidback = "Invalid credit card number Please Try Again";
                CreditCardNumber = default;
                return;
            }
            if (Last3Digits > 999 || Last3Digits < 100)
            {
                ActionFidback = "Invalid 3 last digits Please Try Again";
                Last3Digits = default;
                return;
            }
            if (MailAdress.Length < 10 || MailAdress.Length > 20)
            {
                ActionFidback = "Invalid Mail Adress Lengh, Please Try Again";
                MailAdress = default;
                return;
            }
            if (Validity.Date < DateTime.Now)
            {
                ActionFidback = "Invalid Validity, pleate try again";
                Validity = DateTime.Now;
                return;
            }

            foreach (var item in ProdactSelectedCollection)
            {
                item.PurchaseDate = DateTime.Now;
                ProdactService.Instance.AddProdactToPurchaseList(item);
                ProdactService.Instance.RemoveProdactFromCollection(item);
            }
            ActionFidback = "Thank You For Buying Hope To See You Again, Recipet Will Be Send To Your Mail";
            Last3Digits = default;
            MailAdress = default;
            CreditCardNumber = default;
            TotalSelectedPrice = default;
            Validity = DateTime.Now;
            ProdactSelectedCollection.Clear();
        }

        private void ReturnToBuyingPage()
        {
            MessengerInstance.Send<UserControl>(new BuyingPage());
            last3Digits = default;
            mailAdress = default;
            creditCardNumber = default;
            totalSelectedPrice = default;
            actionFidback = "";
            Validity = DateTime.Now;
        }

        private void ConnectProdact(ObservableCollection<Prodact> obj)
        {
            ProdactSelectedCollection = obj;
            GetTotalPrice();
        }
        public void GetTotalPrice()
        {
            foreach (var item in ProdactSelectedCollection)
            {
                TotalSelectedPrice = TotalSelectedPrice + (item.BasePrice - item.DiscountPrice);
            }
        }
    }
}
