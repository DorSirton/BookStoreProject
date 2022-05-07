using BookStore.Client.Views;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class AddToStockViewModel : ViewModelBase
    {
        public RelayCommand GoBack { get; set; }
        public RelayCommand AddToStock { get; set; }
        public ObservableCollection<string> ProdactTypesCollection { get; set; }
        public ObservableCollection<string> GenereCollection { get; set; }
        public ObservableCollection<string> FrequansyCollection { get; set; }

        public string Name { get => name; set => Set(ref name, value); }
        public string Synopsis { get => synopsis; set => Set(ref synopsis, value); }
        public string AuthorName { get => authorName; set => Set(ref authorName, value); }
        public decimal Price { get => price; set => Set(ref price, value); }
        public int JournalIssueNumber { get => journalIssueNumber; set => Set(ref journalIssueNumber, value); }
        public Guid ReturnedId { get => returnedId; set => Set(ref returnedId, value); }
        public DateTime SelectedDate { get => selectedDate; set => Set(ref selectedDate, value); }
        public string UpdateSuccses { get => updateSuccses; set => Set(ref updateSuccses, value); }
        public Visibility Vis { get => vis; set => Set(ref vis, value); }
        public Visibility SynopsisVis { get => synopsisVis; set => Set(ref synopsisVis, value); }
        public RelayCommand SelectImage { get; set; }
        public string ImageSource { get => imageSource; set =>Set(ref imageSource, value); }

        private string selectedProdact;
        private string selectedGenere;
        private string name;
        private string authorName;
        private decimal price;
        private int journalIssueNumber;
        private string selectedFrequansy;
        private Guid returnedId;
        private DateTime selectedDate;
        private string updateSuccses;
        private string synopsis;
        private Visibility vis;
        private Visibility synopsisVis;
        private string imageSource;

        public string SelectedFrequansy
        {
            get => selectedFrequansy;
            set
            {
                Set(ref selectedFrequansy, value);
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
            }
        }
        public string SelectedGenere
        {
            get => selectedGenere;
            set
            {
                Set(ref selectedGenere, value);
            }
        }

        public AddToStockViewModel()
        {
            GoBack = new RelayCommand(ReturnBack);
            AddToStock = new RelayCommand(UpdateStock);
            SelectImage = new RelayCommand(GetImageSource);
            GenereCollection = new ObservableCollection<string>();
            ProdactTypesCollection = new ObservableCollection<string>();
            FrequansyCollection = new ObservableCollection<string>();
            FillProdactTypes();
            FillJournalFrequansy();
            SelectedDate = DateTime.Now;
            Vis = Visibility.Collapsed;
            SynopsisVis = Visibility.Visible;
        }

        private void GetImageSource()
        {
            OpenFileDialog openImage = new OpenFileDialog();
            if (openImage.ShowDialog() == true)
            {
                ImageSource = openImage.FileName;
            }
        }

        private void UpdateStock()
        {
            if (SelectedProdact == "Book")
            {
                if (SelectedGenere == null || SelectedDate == null || AuthorName == null || Name == null || Price <= 0 || Synopsis == null)
                {
                    UpdateSuccses = "One Or More Of The Required Field Is Unsigned";
                    return;
                }
                ReturnedId = ProdactService.Instance.AddBookToStock(Name, AuthorName, SelectedDate, Price, SelectedGenere, Synopsis, ImageSource);
                UpdateSuccses = "Update Succses !!! ";
            }
            if (SelectedProdact == "Journal")
            {
                if (SelectedGenere == null || SelectedDate == null || AuthorName == null || Name == null || Price <= 0 || SelectedFrequansy == null)
                {
                    UpdateSuccses = "One Or More Of The Required Field Is Unsigned";
                    return;
                }
                ReturnedId = ProdactService.Instance.AddJournalToStock(Name, AuthorName, SelectedDate, Price, JournalIssueNumber, SelectedFrequansy, SelectedGenere,ImageSource);
                UpdateSuccses = "Update Succses !!! ";
            }
            Name = default;
            AuthorName = default;
            SelectedDate = DateTime.Now;
            Price = default;
            SelectedGenere = default;
            JournalIssueNumber = default;
            SelectedFrequansy = default;
            SelectedGenere = default;
            UpdateSuccses = "";
            ImageSource = null;
        }

        private void ReturnBack()
        {
            MessengerInstance.Send<UserControl>(new MenegerOptionsPage());
            Name = default;
            AuthorName = default;
            SelectedDate = DateTime.Now;
            Price = default;
            SelectedGenere = default;
            JournalIssueNumber = default;
            SelectedFrequansy = default;
            SelectedGenere = default;
            UpdateSuccses = "";
            ImageSource = null;

        }
        public void FillProdactTypes()
        {
            ProdactService.Instance.ProdactsTypes.ForEach(p => ProdactTypesCollection.Add(p));
            ProdactTypesCollection.Remove("All");
        }
        public void FillBookGenere()
        {
            ProdactService.Instance.BookGener.ForEach(p => GenereCollection.Add(p));
        }
        public void FillJournalGenere()
        {
            ProdactService.Instance.JournalGener.ForEach(p => GenereCollection.Add(p));
        }
        public void FillJournalFrequansy()
        {
            ProdactService.Instance.JournalFrequency.ForEach(p => FrequansyCollection.Add(p));
        }
        public void UpdateSelectedItem()
        {
            if (SelectedProdact == "Book")
            {
                SynopsisVis = Visibility.Visible;
                Vis = Visibility.Collapsed;
                FillBookGenere();
            }
            if (SelectedProdact == "Journal")
            {
                Vis = Visibility.Visible;
                SynopsisVis = Visibility.Collapsed;
                FillJournalGenere();
            }
        }
    }
}
