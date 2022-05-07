using System;

namespace BookStore.Models.Classes
{
    public class Journal : Prodact
    {
        public string EditorName { get; set; }
        public string Name
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public int IssueNumber { get; set; }
        public string Geners { get; set; }
        public string Frequency { get; set; }
        public Journal(string editorName,string gener, string name, int issueNumber, DateTime publicationDate, decimal basePrice,
           string frequency)
            : base(name, basePrice, publicationDate, gener)
        {
            this.EditorName = editorName;
            this.IssueNumber = issueNumber;
            this.Frequency = frequency;
            this.Geners = gener;
        }
    }

}
