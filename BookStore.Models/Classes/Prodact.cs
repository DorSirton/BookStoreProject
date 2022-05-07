using BookStore.Models.Interfaces;
using System;


namespace BookStore.Models
{
    
    public abstract class Prodact : IDataEntity
    {
        public Guid Id { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Version { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string Genere { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ImageSource { get; set; }


        protected Prodact(string description, decimal basePrice, DateTime publicationDate,string gener, int version = 1)
            : this(description,gener, basePrice, publicationDate, Guid.NewGuid(), version)
        {
        }
        internal Prodact(string description,string gener, decimal basePrice, DateTime publicationDate, Guid id, int version = 1)
        {
            Genere = gener;
            this.Id = Guid.NewGuid();
            this.Description = description;
            this.BasePrice = basePrice;
            this.PublicationDate = publicationDate;
            DiscountPrice = 0;

        }
    }

}
