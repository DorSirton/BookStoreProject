using BookStore.Data;
using BookStore.Models;
using BookStore.Models.Classes;
using BookStore.Models.Enums;
using System;
using System.Collections.Generic;

namespace BookStore.Server
{
    public class ProdactService
    {
        public IRepositery<Prodact> _repository;
        public List<string> ProdactsTypes { get; set; }
        public List<string> BookGener { get; set; }
        public List<string> JournalGener { get; set; }
        public List<string> JournalFrequency { get; set; }

        private static ProdactService _instance;
        public static ProdactService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProdactService();
                }
                return _instance;
            }
        }

        public ProdactService()
        {
            _repository = RepositoryFactory.GetProductRespository();
            ProdactsTypes = new List<string>();
            BookGener = new List<string>();
            JournalGener = new List<string>();
            JournalFrequency = new List<string>();
            FillProdactTypes();
            FillBookGener();
            FillJournalGenere();
            FillJournalFrequency();
        }
        public IEnumerable<Prodact> GetAllProdact()
        {
            return FileSystemData.Instance.GetAllProdact();
        }
        public IEnumerable<Prodact> GetAllSaleProdact()
        {
            return FileSystemData.Instance.GetAllSalesProdact();
        }
        public IEnumerable<Prodact> GetAllPurchaseProdact()
        {
            return FileSystemData.Instance.GetAllPurchaseProdact();
        }
        public void FillProdactTypes()
        {
            ProdactsTypes.Add("Book");
            ProdactsTypes.Add("Journal");
            ProdactsTypes.Add("All");
        }
        public void FillBookGener()
        {
            BookGener.Add($"{BookGenere.Action}");
            BookGener.Add($"{BookGenere.Adventure}");
            BookGener.Add($"{BookGenere.Drama}");
            BookGener.Add($"{BookGenere.Fiction}");
            BookGener.Add($"{BookGenere.Noval}");
            BookGener.Add($"{BookGenere.Romance}");
        }

        public void FillJournalGenere()
        {
            JournalGener.Add($"{JournalGenere.Law}");
            JournalGener.Add($"{JournalGenere.Medicine}");
            JournalGener.Add($"{JournalGenere.Nature}");
            JournalGener.Add($"{JournalGenere.Science}");
        }
        public void FillJournalFrequency()
        {
            JournalFrequency.Add($"{Journalfrequency.Annually}");
            JournalFrequency.Add($"{Journalfrequency.Daily}");
            JournalFrequency.Add($"{Journalfrequency.Monthly}");
            JournalFrequency.Add($"{Journalfrequency.Daily}");
            JournalFrequency.Add($"{Journalfrequency.Quarterly}");
            JournalFrequency.Add($"{Journalfrequency.Weekly}");
        }
        public Guid AddBookToStock(string name, string author, DateTime publication, decimal price, string genere, string synop, string imagesource)
        {
            return _repository.Insert(new Book(author, name, publication, price, genere, 1) { Synopsis = synop, ImageSource = imagesource });
        }
        public Guid AddJournalToStock(string name, string author, DateTime publication, decimal price, int issuewnumber, string freq, string gener, string imagesource)
        {
            return _repository.Insert(new Journal(author, gener, name, issuewnumber, publication, price, freq) { ImageSource = imagesource});
        }
        public void AddExistingProdactToStock(Prodact p)
        {
             _repository.InsertExistProdact(p);
        }
        public void AddProdactToSaleList(Prodact p)
        {
            _repository.InsertSale(p);
        }
        public void RemoveProdactFromSaleList(Prodact p)
        {
            _repository.RemoveSale(p);
        }
        public void AddProdactToPurchaseList(Prodact p)
        {
            _repository.InsertPurchase(p);
        }
        public string RemoveProdactFromCollection(Prodact p)
        {
            if (p.Id != null)
            {
                if (_repository.Delete(p.Id))
                {
                    return "Delet Action Succses !";
                }
                else
                {
                    return "Delet Action Failed..";
                }
            }
            else
            {
                return "Delet Action Failed..";
            }
        }
       
    }
}
