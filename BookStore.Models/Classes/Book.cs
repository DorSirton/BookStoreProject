using System;


namespace BookStore.Models.Classes
{
    public class Book : Prodact
    {
        public string AuthorName { get; set; }
        public string Title
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public string Genres { get; set; }
        public int Edition { get; set; }
        public string Synopsis { get; set; } // תקציר
        public Book(string authorName, string title, DateTime publicationDate, decimal basePrice,
            string genere, int edition = 1)
            : base(title, basePrice, publicationDate, genere)
        {
            this.AuthorName = authorName;
            this.Edition = edition;
            this.Genres = genere; // params - makes the value inserted optional
        }
       
    }

}
