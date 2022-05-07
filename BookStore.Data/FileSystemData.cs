using BookStore.Models;
using BookStore.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookStore.Data
{
    public class FileSystemData : IRepositery<Prodact>
    {
        public List<Prodact> ProdactList { get; set; }
        public List<Prodact> ProdactsOnSale { get; set; }
        public List<Prodact> Purchases { get; set; }

        //private XmlSerializer Serializer;

        private static FileSystemData _instance;
        public static FileSystemData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FileSystemData();
                }
                return _instance;
            }
        }
        //private void Init()
        //{
        //    string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //    try
        //    {
        //        using (FileStream filestream = new FileStream($"{FolderPath}//XmlSerializerData.txt", FileMode.OpenOrCreate))
        //        {
        //            using (var reader = XmlReader.Create(filestream))
        //            {

        //                Prodact[] data = (Prodact[])Serializer.Deserialize(reader);
        //                ProdactsList = data.ToList();

        //            }
        //        }
        //    }
        //    catch (SecurityException ex)
        //    {
        //        throw new SecurityException(ex.Message);
        //    }
        //    catch (DirectoryNotFoundException ex)
        //    {
        //        throw new SecurityException(ex.Message);
        //    }
        //    catch (ArgumentOutOfRangeException ex)
        //    {
        //        throw new SecurityException(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new SecurityException(ex.Message);
        //    }

        //}
        public FileSystemData()
        {
            ProdactList = new List<Prodact>();
            ProdactsOnSale = new List<Prodact>();
            Purchases = new List<Prodact>();
            FillDefaultProdactCollection();
        }
        public void FillDefaultProdactCollection()
        {
            ProdactList.Add(new Book("Antoine de Saint-Exupéry", "The Little Prince", new DateTime(1943, 04, 1), (decimal)99.90, "Noval", 1)
            {
                Synopsis = "young prince who visits various planets in space",
                ImageSource = "/Assets/Littleprince.JPG"
            }) ;
           ProdactList.Add(new Book("Alexander Duma", " The Tree Mosketers", new DateTime(1884, 07, 01), (decimal)99.9, "Adventure", 1)
           {
               Synopsis = "primarily a historical and adventure novel",
               ImageSource = "/Assets/Dartagnan-musketeers.jpg"
           });
           ProdactList.Add(new Journal("News Corporation", "Science", "Times", 30, new DateTime(1778, 01, 01), (decimal)29.90, "Weekly")
           {
               ImageSource = "/Assets/Time_Magazine_logo.png"
           });
           ProdactList.Add(new Journal("Berty Charls Forbs", "Law", "Forbs", 1200, new DateTime(1917), (decimal)29.90, "Monthly")
           {
               ImageSource = "/Assets/Forbes_(magazine)_cover.jpg"

           });
        }
        public bool Delete(Guid id)
        {
            return ProdactList.Remove(ProdactList.FirstOrDefault(p => p.Id == id));
        }

        public Prodact Edit(Prodact item)
        {
            Delete(item.Id);
            Insert(item);
            return item;
        }

        public Prodact Get(Guid id)
        {
            return ProdactList.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Prodact> GetAllProdact(Predicate<Prodact> filter = null)
        {
            if (filter == null)
                return ProdactList;
            return ProdactList.Where(p => filter.Invoke(p));
        }
        public IEnumerable<Prodact> GetAllSalesProdact(Predicate<Prodact> filter = null)
        {
            if (filter == null)
                return ProdactsOnSale;
            return ProdactsOnSale.Where(p => filter.Invoke(p));
        }
        public IEnumerable<Prodact> GetAllPurchaseProdact(Predicate<Prodact> filter = null)
        {
            if (filter == null)
                return Purchases;
            return Purchases.Where(p => filter.Invoke(p));
        }
        public Guid Insert(Prodact item)
        {
            ProdactList.Add(item);
            return item.Id;
        }
        public void InsertExistProdact(Prodact item)
        {
            ProdactList.Add(item);
        }
        public Guid InsertSale(Prodact item)
        {
            ProdactsOnSale.Add(item);
            return item.Id;
        }
        public Guid RemoveSale(Prodact item)
        {
            ProdactsOnSale.Remove(item);
            return item.Id;
        }
        public Guid InsertPurchase(Prodact item)
        {
            Purchases.Add(item);
            return item.Id;
        }


    }
}
